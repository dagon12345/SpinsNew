using SpinsNew.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace SpinsNew.ViewModel
{
    public class PayrollViewModel : PayrollModel //Abstraction from payroll model
    {
        //View model for joining
        public bool Verified { get; set; }// From tbl_masterlist
        public string FullName { get; set; }// From tbl_masterlist
        public string Barangay { get; set; }// From tbl_masterlist
        public DateTime? BirthDate { get; set; }// From tbl_masterlist
        public string Sex { get; set; } // from tbl_masterlist
        public string HealthStatus { get; set; }
        public string HealthStatusRemarks { get; set; }
        [Display(Name ="ID Type")]
        public string IdType { get; set; }

    }
}
