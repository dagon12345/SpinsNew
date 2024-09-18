using SpinsNew.DataAccess.Libraries;
using SpinsNew.Libraries;
using SpinsNew.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SpinsNew.DataAccess.Models
{
    public class MasterListModel
    {
        //Parent model
        public int Id { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string ExtName { get; set; }
        public int PSGCProvince { get; set; }
        public int PSGCCityMun { get; set; }
        public DateTime? BirthDate { get; set; }
        public int? SexID { get; set; }
        public int? HealthStatusID { get; set; }
        [Display(Name = "Remarks")]
        public string HealthStatusRemarks { get; set; }
        public int IDtypeID { get; set; } //OSCA id related Library ID Type
        public string IDNumber { get; set; }//OSCA id related Library ID Type
        public int StatusID { get; set; }//from libraryStatus

        public string Remarks { get; set; }
        public string DateDeceased { get; set; }

        public bool IsVerified { get; set; }

        /*Below our tables and libraries that is referenced for joining*/
        public ICollection<PayrollModel> PayrollModels { get; } = new List<PayrollModel>();
        public LibrarySex LibrarySex { get; set; }
        public LibraryHealthStatus LibraryHealthStatus { get; set; }
        public LibraryIDType LibraryIDType { get; set; }
        public LibraryStatus LibraryStatus { get; set; }
        public GisModel GisModel { get; set; }
        public LibraryMunicipality LibraryMunicipality { get; set; }
        //public ICollection<GisModel> GisModels { get; } = new List<GisModel>();

    }
}
