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
                     .Select(x => new
                     {
                         Province = x.PSGCProvince,
                         Municipality = x.PSGCCityMun,
                         ClaimTypeID = x.ClaimTypeID,
                         PeriodID = x.PeriodID,
                         Year = x.Year,
                         x.LibraryProvince,
                         x.LibraryMunicipality
                     })
                    .Where(x => x.Year == year && x.ClaimTypeID.HasValue && x.PeriodID == periodID)

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
                    .Select(x => new PayrollViewModel
                    {
                        Province = x.Key.Province,
                        Municipality = x.Key.Municipality,
                        District = x.Key.District,
                        TotalTarget = x.Count()

                    })

                    .OrderBy(x => x.Province)
                    .ToList();


                return payrollGroup;

            }
        }
    }
}
