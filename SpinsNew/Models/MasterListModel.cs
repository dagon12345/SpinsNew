
using SpinsNew.Libraries;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SpinsNew.Models
{
    public class MasterListModel
    {
        //Parent model
        public int Id { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string ExtName { get; set; }
        public DateTime? BirthDate { get; set; }
        public int? SexID { get; set; }
        public int? HealthStatusID { get; set; }
        [Display(Name = "Remarks")]
        public string HealthStatusRemarks { get; set; }
        public int IDtypeID { get; set; } //OSCA id related Library ID Type
        public string IDNumber { get; set; }//OSCA id related Library ID Type
        public bool IsVerified { get; set; }

        public ICollection<PayrollModel> PayrollModels { get; } = new List<PayrollModel>();
        public LibrarySex LibrarySex { get; set; }
        public LibraryHealthStatus LibraryHealthStatus { get; set; }
        public LibraryIDType LibraryIDType { get; set; }
        // public PayrollModel PayrollModel { get; set; }
    }
}
