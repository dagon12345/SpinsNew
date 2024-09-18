using SpinsNew.DataAccess.Models;
using System.Collections.Generic;

namespace SpinsNew.DataAccess.Libraries
{
    public class LibraryStatus
    {
        public int Id { get; set; }
        public string Status { get; set; }

        public ICollection<MasterListModel> MasterListModels { get; } = new List<MasterListModel>(); // From tbl_masterlist
    }
}
