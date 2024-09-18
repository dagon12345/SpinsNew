using SpinsNew.DataAccess.Models;
using System;
using System.Collections.Generic;

namespace SpinsNew.DataAccess.Libraries
{
    public class LibraryPaymentMode
    {
        public int Id { get; set; }
        public int PaymentModeID { get; set; }
        public string PaymentMode { get; set; }
        public DateTime? DateTimeEntry { get; set; }

        public ICollection<PayrollModel> PayrollModels { get; } = new List<PayrollModel>();
    }
}
