using SpinsNew.Models;
using System.Collections.Generic;

namespace SpinsNew.Libraries
{
    public class LibraryRegion
    {
        public int Id { get; set; }
        public int PSGCRegion { get; set; }
        public string Region { get; set; }

        public override string ToString()
        {
            return Region;
        }

        public ICollection<MasterListModel> masterListModels { get; set; } = new List<MasterListModel>();
        public ICollection<LibraryProvince> LibraryProvinces { get; set; } = new List<LibraryProvince>();//One region has many province
    
    }
}
