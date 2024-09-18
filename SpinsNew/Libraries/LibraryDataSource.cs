using SpinsNew.Models;
using System.Collections.Generic;

namespace SpinsNew.Libraries
{
    public class LibraryDataSource
    {
        public int Id { get; set; }
        public string DataSource { get; set; }

        public ICollection<MasterListModel> MasterListModels { get; } = new List<MasterListModel>();
    }
}
