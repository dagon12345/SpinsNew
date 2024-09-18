using SpinsNew.Models;
using System.Collections.Generic;

namespace SpinsNew.Libraries
{
    public class LibraryClaimType
    {
        
        public int Id { get; set; }
        public int ClaimTypeID { get; set; }
        public string ClaimType { get; set; }

        public ICollection<PayrollModel> PayrollModels { get; } = new List<PayrollModel>();
    }
}
