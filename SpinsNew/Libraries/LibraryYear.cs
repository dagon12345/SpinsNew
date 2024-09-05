using SpinsNew.Models;
using System.Collections.Generic;

namespace SpinsNew.Libraries
{
    public class LibraryYear
    {
        //Id don't need a key since it was an Id it is automatic recognized by EF
        public int Id { get; set; }
        public int Year { get; set; }
        public int MonthlyStipend { get; set; }
        public int Active { get; set; }

        public override string ToString()
        {
            return Year.ToString(); // THis override to return string into our combobox.
        }


        public ICollection<PayrollModel> PayrollModels { get; } = new List<PayrollModel>();//tbl_payroll_socpen is our principal entity
    }
}
