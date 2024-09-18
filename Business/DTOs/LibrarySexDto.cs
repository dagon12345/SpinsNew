using SpinsNew.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.DTOs
{
    public class LibrarySexDto
    {
        public int Id { get; set; }
        public string Sex { get; set; }

        //public ICollection<PayrollModel> PayrollModels { get; } = new List<PayrollModel>();
        public ICollection<MasterListModelDto> MasterLists { get; } = new List<MasterListModelDto>();

    }
}
