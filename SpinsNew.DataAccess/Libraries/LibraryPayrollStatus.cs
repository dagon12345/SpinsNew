using SpinsNew.DataAccess.Models;
using System;
using System.Collections.Generic;

namespace SpinsNew.DataAccess.Libraries
{
    public class LibraryPayrollStatus
    {
        public int Id { get; set; }
        public int PayrollStatusID { get; set; }
        public string PayrollStatus { get; set; }
        public DateTime? DateTimeEntry { get; set; }

        public ICollection<PayrollModel> PayrollModels { get; } = new List<PayrollModel>();
    }
}
