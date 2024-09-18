using SpinsNew.DataAccess.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SpinsNew.Models
{
    public class GisModel
    {
        [Key]
        public int Id { get; set; }
        public int ReferenceCode { get; set; }
        public int MasterListID { get; set; }
        public int PSGCProvince { get; set; }
        public int PSGCCityMun { get; set; }
        public int PSGCBrgy { get; set; }
        public int AssessmentID { get; set; }
        public int? SpisBatch { get; set; }

       //  public ICollection<PayrollModel> PayrollModels { get; } = new List<PayrollModel>();

       // public ICollection<PayrollandGisManyToMany> PayrollandGisManyToManys { get; } = new List<PayrollandGisManyToMany>(); //Linking Entitiy many to many relationship
         public ICollection<MasterListModel> MasterListModels { get; } = new List<MasterListModel>();
        //public MasterListModel MasterListModel { get; set; }
    }
}
