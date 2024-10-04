using SpinsNew.Models;
using System.Collections.Generic;

namespace SpinsNew.Libraries
{
    public class LibraryClaimType
    {
        
        public int Id { get; set; }
        public int ClaimTypeID { get; set; }
        public string ClaimType { get; set; }

        public override string ToString()
        {
            return ClaimType;
        }

        public ICollection<PayrollModel> PayrollModels { get; } = new List<PayrollModel>();
    }
}
