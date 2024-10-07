using SpinsNew.Models;
using System;
using System.Collections.Generic;

namespace SpinsNew.Libraries
{
    public class LibraryPaymentMode
    {
        public int Id { get; set; }
        public int PaymentModeID { get; set; }
        public string PaymentMode { get; set; }
        public DateTime? DateTimeEntry { get; set; }

        public override string ToString()
        {
            return PaymentMode;
        }

        public ICollection<PayrollModel> PayrollModels { get; } = new List<PayrollModel>();
    }
}
