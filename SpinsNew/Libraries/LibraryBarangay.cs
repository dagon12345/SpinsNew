using SpinsNew.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SpinsNew.Libraries
{
    public class LibraryBarangay
    {
        [Key]
        public int PSGCBrgy { get; set; }
        public string BrgyName { get; set; }
        public int PSGCCityMun { get; set; }

        public ICollection<PayrollModel> PayrollModels { get; } = new List<PayrollModel>();
        public ICollection<MasterListModel> masterListModels { get; } = new List<MasterListModel>();
 
    }
}
