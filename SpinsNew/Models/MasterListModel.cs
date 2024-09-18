
using SpinsNew.Libraries;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SpinsNew.Models
{
    public class MasterListModel
    {
        /*NOTE: If there are no question mark in Foreign key property
         it is automatic INNERJOIN instead of LEFTJoin*/
        //Parent model
        public int Id { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string ExtName { get; set; }
        public int PSGCProvince { get; set; }
        public int PSGCCityMun { get; set; }
        public int PSGCBrgy { get; set; }
        public string Address { get; set; }
        public DateTime? BirthDate { get; set; }
        public int? SexID { get; set; }
        public int? MaritalStatusID { get; set; }
        public string Religion { get; set; }
        public string BirthPlace { get; set; }
        public string EducAttain { get; set; }
     

        public int? IDtypeID { get; set; } //OSCA id related Library ID Type
        [Display(Name = "ID Number")]
        public string IDNumber { get; set; }//OSCA id related Library ID Type
        [Display(Name = "Date Issued")]
        public DateTime? IDDateIssued { get; set; }
        public bool Pantawid { get; set; }
        public bool Indigenous { get; set; }
        [Display(Name ="SocPen ID")]
        public string SocialPensionId { get; set; }
        [Display(Name = "Household ID")]
        public string HouseholdId { get; set; }
        [Display(Name = "Indigenous ID")]
        public string IndigenousId { get; set; }
        [Display(Name = "Contact #")]
        public string ContactNum { get; set; }

        public int? HealthStatusID { get; set; }
        [Display(Name = "Remarks")]
        public string HealthStatusRemarks { get; set; }
        public DateTime? DateTimeEntry { get; set; }
        [Display(Name ="Entered By")]
        public string EntryBy { get; set; }

        public int? DataSourceId { get; set; }
        public int StatusID { get; set; }//from libraryStatus
        public string Remarks { get; set; }
        public int? RegTypeId { get; set; }
        public DateTime? InclusionDate { get; set; }
        public string ExclusionBatch { get; set; }
        public DateTime? ExclusionDate { get; set; }
        public DateTime? DateTimeModified { get; set; }
        public string ModifiedBy { get; set; }
        public string DateDeceased { get; set; }
        public DateTime? DateTimeDeleted { get; set; }
        public string DeletedBy { get; set; }
        [Display(Name = "Verification")]
        public bool IsVerified { get; set; }

        /*Below our tables and libraries that is referenced for joining*/
        public ICollection<PayrollModel> PayrollModels { get; } = new List<PayrollModel>();
        public LibrarySex LibrarySex { get; set; }
        public LibraryHealthStatus LibraryHealthStatus { get; set; }
        public LibraryIDType LibraryIDType { get; set; }
        public LibraryStatus LibraryStatus { get; set; }
        //public GisModel GisModel { get; set; } = null;
        public LibraryMunicipality LibraryMunicipality { get; set; }
        public LibraryBarangay LibraryBarangay { get; set; }
        public LibraryMaritalStatus LibraryMaritalStatus { get; set; }
        public LibraryDataSource LibraryDataSource { get; set; }
        public LibraryRegistrationType LibraryRegistrationType { get; set; }
        public ICollection<GisModel> GisModels { get; } = new List<GisModel>();
        public ICollection<SpbufModel> SpbufModels { get; } = new List<SpbufModel>();

    }
}
