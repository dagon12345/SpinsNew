using SpinsNew.Models;
using System.Collections.Generic;

namespace SpinsNew.Libraries
{
    public class LibraryRegistrationType
    {
        public int Id { get; set; }
        public string RegType { get; set; }

        public ICollection<MasterListModel> MasterListModels { get; } = new List<MasterListModel>();
    }
}
