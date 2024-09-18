using SpinsNew.DataAccess.Models;
using System;
using System.Collections.Generic;

namespace SpinsNew.DataAccess.Libraries
{
    public class LibraryPayrollType
    {
        public int Id { get; set; }
        public int PayrollTypeID { get; set; }
        public string PayrollType { get; set; }
        public DateTime? DateTimeEntry { get; set; }

        public ICollection<PayrollModel> PayrollModels { get; } = new List<PayrollModel>();//Reference from our payroll model
    }
}
