using SpinsNew.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SpinsNew.Libraries
{
    public class LibrarySex
    {
        
        public int Id { get; set; }
        public string Sex { get; set; }

        public override string ToString()
        {
            return Sex;
        }

        //public ICollection<PayrollModel> PayrollModels { get; } = new List<PayrollModel>();
        public ICollection<MasterListModel> MasterListModels { get; } = new List<MasterListModel>();
    }
}
