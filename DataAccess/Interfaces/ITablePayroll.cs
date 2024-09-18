using SpinsNew.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SpinsNew.Interfaces
{
    public interface ITablePayroll
    {
        //We create an method that we can write a logic inside our TablePayroll services
        Task<List<PayrollViewModel>> GetPayrollAsync(int year, int periodID);
        //Task<List<Payroll>>
    }
}
