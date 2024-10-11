using Microsoft.EntityFrameworkCore;
using SpinsNew.Data;
using SpinsNew.Interfaces;
using SpinsNew.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpinsNew.Services
{
    public class TableGisService : ITableGIS
    {
        public async Task<List<GisViewModel>> DisplayGisAsync(DateTime startDate, DateTime endDate)
        {
            using (var context = new ApplicationDbContext())
            {
                var displayGis = await context.tbl_gis
                     .Include(x => x.MasterListModel)
                     .ThenInclude(m => m.LibraryRegion)
                     .Include(x => x.MasterListModel.LibraryProvince)
                     .Include(x => x.MasterListModel.LibraryMunicipality)
                     .Include(x => x.MasterListModel.LibraryBarangay)
                     .Include(x => x.MasterListModel.LibrarySex)
                     .Include(x => x.MasterListModel.LibraryMaritalStatus)
                     .Include(x => x.MasterListModel.LibraryIDType)
                     .Include(x => x.LibrarylivCondition)
                     .Include(x => x.LibraryValidator)
                     .Include(x => x.LibraryAssessment)
                     .Include(x => x.TableAuthRepresentatives)
                     .Where(x => x.EntryDateTime >= startDate && x.EntryDateTime <= endDate)
                     .OrderBy(x => x.MasterListModel.LibraryProvince.ProvinceName)
                     .AsNoTracking()
                     .ToListAsync(); // Fetch data into memory

                // Group by ReferenceCode and select the latest entry
                var groupedDisplayGis = displayGis
                    .GroupBy(x => (x.MasterListModel.LastName, x.MasterListModel.FirstName, x.MasterListModel.MiddleName, x.MasterListModel.ExtName))
                    .Select(g => g.OrderByDescending(x => x.EntryDateTime).FirstOrDefault()) // Get the latest entry per group
                    .Select(x => new GisViewModel
                    {
                        Id = x.Id,
                        FullName = $"{x.MasterListModel.LastName}, {x.MasterListModel.FirstName} {x.MasterListModel.MiddleName} {x.MasterListModel.ExtName}",
                        EntryDateTime = x.EntryDateTime,
                        IdOsca = x.MasterListModel.IDNumber,
                        ReferenceCode = x.ReferenceCode,
                        LastName = x.MasterListModel.LastName,
                        FirstName = x.MasterListModel.FirstName,
                        MiddleName = x.MasterListModel.MiddleName,
                        ExtName = x.MasterListModel.ExtName,
                        IdType = x.MasterListModel.LibraryIDType.Type,
                        Grantee = "YES",
                        Respondent = $"{x.MasterListModel.LastName}, {x.MasterListModel.FirstName} {x.MasterListModel.MiddleName} {x.MasterListModel.ExtName}",

                        Region = $"{x.MasterListModel.LibraryRegion.Region}/{x.MasterListModel.PSGCRegion}",
                        Province = $"{x.MasterListModel.LibraryProvince.ProvinceName}/{x.MasterListModel.PSGCProvince}",
                        Municipality = $"{x.MasterListModel.LibraryMunicipality.CityMunName}/{x.MasterListModel.PSGCCityMun}",
                        Barangay = $"{x.MasterListModel.LibraryBarangay.BrgyName}/{x.MasterListModel.PSGCBrgy}",
                        Address = x.MasterListModel.Address,
                        permanentStreet = "",

                        PresentRegion = $"{x.MasterListModel.LibraryRegion.Region}/{x.MasterListModel.PSGCRegion}",
                        PresentProvince = $"{x.MasterListModel.LibraryProvince.ProvinceName}/{x.MasterListModel.PSGCProvince}",
                        PresentMunicipality = $"{x.MasterListModel.LibraryMunicipality.CityMunName}/{x.MasterListModel.PSGCCityMun}",
                        PresentBarangay = $"{x.MasterListModel.LibraryBarangay.BrgyName}/{x.MasterListModel.PSGCBrgy}",
                        PresentAddress = x.MasterListModel.Address,
                        PresentpermanentStreet = "",

                        Sex = x.MasterListModel.LibrarySex.Sex,
                        BirthDate = x.MasterListModel.BirthDate,
                        Age = DateTime.Now.Year - x.MasterListModel.BirthDate.Value.Year
                             - (DateTime.Now.DayOfYear < x.MasterListModel.BirthDate.Value.DayOfYear ? 1 : 0),
                        BirthPlace = x.MasterListModel.BirthPlace,
                        CareGiver = "",
                        Relationship = "",
                        MaritalStatus = x.MasterListModel.LibraryMaritalStatus.MaritalStatus,
                        HouseholdSize = x.HouseholdSize,
                        Tin = "",
                        ContactNumber = x.MasterListModel.ContactNum,
                        Nationality = "FILIPINO",
                        Profession = "",
                        SourceOfFunds = "",
                        GrossSalary = "",
                        Email = "",
                        MothersMaiden = x.MasterListModel.MothersMaiden,
                        EmbossName = "",
                        LbpBank = "",
                        ReceivePension = "NO",
                        DswdSocialPension = "NO",
                        Gsis = "NO",
                        Sss = "NO",
                        Afpslai = "NO",
                        Otherpension = "NO",
                        WagesSalaries = "N/A",
                        WagesSalariesRegular = "N/A",
                        WagesSalariesAmount = 0,
                        Profit = "N/A",
                        ProfitRegular = "N/A",
                        ProfitAmount = 0,
                        Household = "N/A",
                        HouseholdRegular = "N/A",
                        HouseholdAmount = 0,
                        Domestic = "N/A",
                        DomesticRegular = "N/A",
                        DomesticAmount = 0,
                        International = "N/A",
                        InternationalRegular = "N/A",
                        InternationalAmount = 0,
                        Friends = "N/A",
                        FriendsRegular = "N/A",
                        FriendsAmount = 0,
                        Government = "N/A",
                        GovernmentRegular = "N/A",
                        GovernmentAmount = 0,
                        Others = "N/A",
                        OthersRegular = "N/A",
                        OthersAmount = 0,
                        TotalAmount = 0,

                        LivingConditions = x.LibrarylivCondition.LivingConditions,

                        // LivingConditionID = x.LivingConditionID,
                        Older85 = (DateTime.Now.Year - x.MasterListModel.BirthDate.Value.Year
                             - (DateTime.Now.DayOfYear < x.MasterListModel.BirthDate.Value.DayOfYear ? 1 : 0)) > 85 ? "YES" : "NO",
                        HealthLimitActivities = "N/A",
                        NeedSomeoneHelp = "N/A",
                        NeedStayHome = "N/A",
                        CanCountOnSomeone = "N/A",
                        NeedMobilityAssistance = "N/A",
                        Disability = "N/A",
                        IllnessDisease = "N/A",
                        Food = "N/A",
                        MedicineVitamins = "N/A",
                        HealthCheckUp = "N/A",
                        Clothes = "N/A",
                        Utilities = "N/A",
                        DebtPayment = "N/A",
                        Livelihood = "N/A",
                        UtilizationOthers = "N/A",
                        ValidatorName = x.LibraryValidator.Validator,
                        ValidationDate = x.ValidationDate,
                        Assessment = x.LibraryAssessment.Assessment,
                        SpisRemarks = "",
                        AuthorizeRepresentative = string.Join("; ", x.TableAuthRepresentatives
                                         .Select(t => $"{t.LastName}, {t.FirstName} {t.MiddleName}"))
                    })
                    .ToList();

                return groupedDisplayGis;

            }
        }
    }
}
