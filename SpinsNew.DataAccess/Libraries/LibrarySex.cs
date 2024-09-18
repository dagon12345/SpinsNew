using SpinsNew.DataAccess.Models;
using System.Collections.Generic;

namespace SpinsNew.DataAccess.Libraries
{
    public class LibrarySex
    {
        
        public int Id { get; set; }
        public string Sex { get; set; }

        //public ICollection<PayrollModel> PayrollModels { get; } = new List<PayrollModel>();
        public ICollection<MasterListModel> MasterListModels { get; } = new List<MasterListModel>();
    }
}
