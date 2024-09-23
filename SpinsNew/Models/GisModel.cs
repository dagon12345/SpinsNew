using SpinsNew.Libraries;
using SpinsNew.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SpinsNew.Models
{
    public class GisModel
    {
        //Also a parent but not in the masterlist
        [Key]
        public int? Id { get; set; }
        public int? ReferenceCode { get; set; }
        public int? MasterListID { get; set; }
        public int? PSGCProvince { get; set; }
        public int? PSGCCityMun { get; set; }
        public int? PSGCBrgy { get; set; }
        public int? AssessmentID { get; set; }
        public int? SpisBatch { get; set; }
        public int? HouseholdSize { get; set; }
        public int? ValidatedByID { get; set; }
        public DateTime? ValidationDate { get; set; }

        //  public ICollection<PayrollModel> PayrollModels { get; } = new List<PayrollModel>();

        // public ICollection<PayrollandGisManyToMany> PayrollandGisManyToManys { get; } = new List<PayrollandGisManyToMany>(); //Linking Entitiy many to many relationship
        // public ICollection<MasterListModel> MasterListModels { get; } = new List<MasterListModel>();
        public LibraryAssessment LibraryAssessment { get; set; }
        public MasterListModel MasterListModel { get; set; } = null;
        public LibraryValidator LibraryValidator { get; set; }
    }
}
