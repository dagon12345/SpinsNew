using SpinsNew.Models;
using System;
using System.Collections.Generic;

namespace SpinsNew.Libraries
{
    public class LibraryPayrollTag
    {
        public int Id { get; set; }
        public int PayrollTagID { get; set; }
        public string PayrollTag { get; set; }
        public DateTime? DateTimeEntry { get; set; }

        public ICollection<PayrollModel> PayrollModels { get; } = new List<PayrollModel>();
    }
}
