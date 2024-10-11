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
        public int? LivingConditionID { get; set; }//We will make a table for this lib_gis_living_condition
        [Display(Name ="Date Accomplished")]
        public DateTime? ValidationDate { get; set; }
        public string EntryBy { get; set; }
        public DateTime? EntryDateTime { get; set; }//Index here for generating GIS date from and to

        public LibrarylivCondition LibrarylivCondition { get; set; }
        public LibraryAssessment LibraryAssessment { get; set; }
        public MasterListModel MasterListModel { get; set; } = null;
        public LibraryValidator LibraryValidator { get; set; }
        //GIS model reference code has many TableAuthorizeRepresentatives
        public ICollection<TableAuthRepresentative> TableAuthRepresentatives { get; set; } = new List<TableAuthRepresentative>();
    }
}
