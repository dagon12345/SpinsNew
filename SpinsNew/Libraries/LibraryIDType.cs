using SpinsNew.Models;
using System.Collections.Generic;

namespace SpinsNew.Libraries
{
    public class LibraryIDType
    {
        public int Id { get; set; }
        public string Type { get; set; }

        public ICollection<MasterListModel> MasterListModels { get; } = new List<MasterListModel>();
    }
}
