using SpinsNew.DataAccess.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SpinsNew.DataAccess.Libraries
{
    public class LibraryBarangay
    {
        [Key]
        public int PSGCBrgy { get; set; }
        public string BrgyName { get; set; }
        public int PSGCCityMun { get; set; }

        public ICollection<PayrollModel> PayrollModels { get; } = new List<PayrollModel>();
 
    }
}
