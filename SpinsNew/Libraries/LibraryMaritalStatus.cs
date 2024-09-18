using SpinsNew.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpinsNew.Libraries
{
    public class LibraryMaritalStatus
    {
        public int Id { get; set; }
        public string MaritalStatus { get; set; }

        public ICollection<MasterListModel> masterListModels { get; } = new List<MasterListModel>();
    }
}
