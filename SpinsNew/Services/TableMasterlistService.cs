using Microsoft.EntityFrameworkCore;
using SpinsNew.Data;
using SpinsNew.Interfaces;
using SpinsNew.Models;
using SpinsNew.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpinsNew.Services
{
    public class TableMasterlistService : ITableMasterlist
    {
        public async Task<List<MasterListViewModel>> GetMasterListModelsAsync(List<int> municipalities, List<int> status)
        {
            using (var context = new ApplicationDbContext())
            {
                var masterList = await context.tbl_masterlist
                    .Include(m => m.LibraryMunicipality)
                    .Include(b => b.LibraryBarangay)
                    .Include(s => s.LibrarySex)
                    .Include(ms => ms.LibraryMaritalStatus)
                    .Include(id => id.LibraryIDType)
                    .Include(hs => hs.LibraryHealthStatus)
                    .Include(ld => ld.LibraryDataSource)
                    .Include(ls => ls.LibraryStatus)
                    .Include(rt => rt.LibraryRegistrationType)
                    .Include(g => g.GisModels)
                        .ThenInclude(la => la.LibraryAssessment)
                    .Include(sp => sp.SpbufModels)
                    .Where(m => municipalities.Contains(m.PSGCCityMun) && m.DateTimeDeleted == null && status.Contains(m.StatusID))
                    .Select(n => new MasterListViewModel
                    {
                        Id = n.Id,
                        LastName = n.LastName,
                        FirstName = n.FirstName,
                        MiddleName = n.MiddleName,
                        ExtName = n.ExtName,

                        Municipality = n.LibraryMunicipality.CityMunName,
                        Barangay = n.LibraryBarangay.BrgyName,


                        Address = n.Address,
                        BirthDate = n.BirthDate,

                        //Calculate age based on the Birthdate if has value.
                        Age = DateTime.Now.Year - n.BirthDate.Value.Year
                         - (DateTime.Now.DayOfYear < n.BirthDate.Value.DayOfYear ? 1 : 0),
                        Sex = n.LibrarySex.Sex,
                        MaritalStatus = n.LibraryMaritalStatus.MaritalStatus,
                        Religion = n.Religion,
                        BirthPlace = n.BirthPlace,
                        EducAttain = n.EducAttain,
                        IdType = n.LibraryIDType.Type,
                        IDNumber = n.IDNumber,
                        IDDateIssued = n.IDDateIssued,
                        Pantawid = n.Pantawid,
                        Indigenous = n.Indigenous,
                        SocialPensionId = n.SocialPensionId,
                        HouseholdId = n.HouseholdId,
                        IndigenousId = n.IndigenousId,
                        ContactNum = n.ContactNum,
                        HealthStatus = n.LibraryHealthStatus.HealthStatus,
                        DateTimeEntry = n.DateTimeEntry,
                        EntryBy = n.EntryBy,
                        DataSource = n.LibraryDataSource.DataSource,
                        Status = n.LibraryStatus.Status,
                        Remarks = n.Remarks,
                        Registration = n.LibraryRegistrationType.RegType,
                        InclusionDate = n.InclusionDate,
                        ExclusionBatch = n.ExclusionBatch,
                        DateTimeModified = n.DateTimeModified,
                        ModifiedBy = n.ModifiedBy,
                        DateDeceased = n.DateDeceased,
                        IsVerified = n.IsVerified,
                        Assessment = n.GisModels.Select(l => l.LibraryAssessment.Assessment).FirstOrDefault(),
                        ReferenceCode = n.GisModels.Select(r => r.ReferenceCode).FirstOrDefault(),
                        SpisBatch = n.GisModels.Select(s => s.SpisBatch).FirstOrDefault(),
                        Spbuf = n.SpbufModels.Select(r => r.ReferenceCode).FirstOrDefault()

                    })
                    .ToListAsync();

                return masterList;
            }
        }

        public async Task SoftDeleteAsync(int Id, DateTime? dateDeleted, string _username)
        {
            using (var context = new ApplicationDbContext())
            {
               var updateDelete = context.tbl_masterlist.FirstOrDefault(x => x.Id == Id);
               updateDelete.DateTimeDeleted = dateDeleted;
               updateDelete.DeletedBy = _username;
               await context.SaveChangesAsync();
            }
        }

        public async Task UpdateAsync(int id, int statusId, string dateDeceased,
            string remarks, string exclusionBatch, DateTime? exclusionDate,
            DateTime? inclusionDate)
        {
            using (var context = new ApplicationDbContext())
            {


                //Update status below
                var masterlistActivate = context.tbl_masterlist.FirstOrDefault(m => m.Id == id);
                masterlistActivate.StatusID = statusId;
                masterlistActivate.DateDeceased = dateDeceased;
                masterlistActivate.Remarks = remarks;
                masterlistActivate.ExclusionBatch = exclusionBatch;
                masterlistActivate.ExclusionDate = exclusionDate;
                masterlistActivate.InclusionDate = inclusionDate;
                await context.SaveChangesAsync();

               
            }

        }

        public async Task VerificationUpdateAsync(int Id,bool verify)
        {
            using(var context = new ApplicationDbContext())
            {
                var verifyMaster = context.tbl_masterlist.FirstOrDefault(i => i.Id == Id);
                verifyMaster.IsVerified = verify;
                await context.SaveChangesAsync();
            }
        }
    }
}
