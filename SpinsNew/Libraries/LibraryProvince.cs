using SpinsNew.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SpinsNew.Libraries
{
    public class LibraryProvince
    {
        [Key]//Declare this as a key for mapping because our lib_province library has no Id property.
        public int PSGCProvince { get; set; }
        public string ProvinceName { get; set; }
        public int PSGCRegion { get; set; }

        public ICollection<PayrollModel> PayrollModels { get; } = new List<PayrollModel>();// The principal entity is the payroll model
    }
}
