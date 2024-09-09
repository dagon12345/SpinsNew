using Microsoft.EntityFrameworkCore;
using SpinsNew.Data;
using SpinsNew.Interfaces;
using SpinsNew.ViewModel;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpinsNew.Services
{
    public class TablePayroll : ITablePayroll //Abstraction from interfaces
    {
        // Example usage of GetLocalOrAttach extension method
        //var cachedYear = _payrollContext.tbl_payroll_socpen
        //    .GetLocalOrAttach( x => x.Year == year,
        //    () => new PayrollModel {
        //        Year = year
        //    }
        //);

        //// Example usage of GetLocalOrAttach extension method
        //var cachedPeriod = _payrollContext.tbl_payroll_socpen
        //    .GetLocalOrAttach(x => x.PeriodID == periodID,
        //    () => new PayrollModel
        //    {
        //        PeriodID = periodID
        //    }
        //);

        public async Task<List<PayrollViewModel>> GetPayrollAsync(int year, int periodID)
        {

            using (var _dbContext = new ApplicationDbContext())
            {

                var payrollData = await _dbContext.tbl_payroll_socpen
                     //.Include(x => x.MasterListModel)
                     .Select(x => new
                     {
                         Province = x.PSGCProvince,
                         Municipality = x.PSGCCityMun,
                         ClaimTypeID = x.ClaimTypeID,
                         PeriodID = x.PeriodID,
                         Year = x.Year,
                         Amount = x.Amount, // Ensure the Amount field is included.
                    
                         x.LibraryProvince,
                         x.LibraryMunicipality,
                         x.MasterListModel
                     })
                    .Where(x => x.Year == year && x.ClaimTypeID.HasValue && x.PeriodID == periodID )//Filter by amount of 3000
                    .AsNoTracking()
                    .ToListAsync();

                var payrollGroup = payrollData

                    .GroupBy(x => new
                    {
                        Province = x.LibraryProvince.ProvinceName,
                        Municipality = x.LibraryMunicipality.CityMunName,
                        District = x.LibraryMunicipality.District,
                        Year = x.Year,

                    })
                    .Select(group => new PayrollViewModel
                    {
                        Province = group.Key.Province,
                        Municipality = group.Key.Municipality,
                        District = group.Key.District,
                        TotalTarget = group.Count(),
                        Male = group.Count(x => x.MasterListModel.SexID == 1 && x.Amount == 3000),  // Count of males
                        Female = group.Count(x => x.MasterListModel.SexID == 2 && x.Amount == 3000),  // Count of females
                        TotalAmount3000 = group.Count(x => (x.MasterListModel.SexID == 1 || x.MasterListModel.SexID == 2) && x.Amount == 3000)
                    })

                    .OrderBy(x => x.Province)
                      .ThenBy(x => x.Municipality) // Optional: Order by Municipality if needed
                    .ToList();


                return payrollGroup;

            }
        }
    }
}
