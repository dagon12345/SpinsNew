using SpinsNew.DataAccess.Models;
using System.Collections.Generic;

namespace SpinsNew.DataAccess.Libraries
{
    public class LibraryIDType
    {
        public int Id { get; set; }
        public string Type { get; set; }

        public ICollection<MasterListModel> MasterListModels { get; } = new List<MasterListModel>();
    }
}
