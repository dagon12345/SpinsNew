using SpinsNew.Models;
using System.Collections.Generic;

namespace SpinsNew.Libraries
{
    public class LibraryHealthStatus
    {
        public int Id { get; set; }
        public string HealthStatus { get; set; }

        public ICollection<MasterListModel> MasterListModels { get; } = new List<MasterListModel>();
    }
}
