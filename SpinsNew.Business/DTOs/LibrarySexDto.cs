using System.Collections.Generic;

namespace SpinsNew.Business.DTOs
{
    public class LibrarySexDto
    {
        public int Id { get; set; }
        public string Sex { get; set; }

        //public ICollection<PayrollModel> PayrollModels { get; } = new List<PayrollModel>();
        public ICollection<MasterListModelDto> MasterLists { get; } = new List<MasterListModelDto>();

    }
}
