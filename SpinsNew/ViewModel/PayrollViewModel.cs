using SpinsNew.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace SpinsNew.ViewModel
{
    public class PayrollViewModel : PayrollModel //Abstraction from payroll model
    {
        //View model for joining
        public bool Verified { get; set; }// From tbl_masterlist
        public string Province { get; set; }//from tbl_payroll_socpen
        public string Municipality { get; set; }//from tbl_payroll_socpen
        public int District { get; set; }//from lib_city_mun
        public string FullName { get; set; }// From tbl_masterlist
        public string Barangay { get; set; }// From tbl_masterlist
        public DateTime? BirthDate { get; set; }// From tbl_masterlist
        public string Sex { get; set; } // from tbl_masterlist
        public string HealthStatus { get; set; }//from libraryHealthstatus
        public string HealthStatusRemarks { get; set; }//from libraryHealthstatus
        [Display(Name ="ID Type")]
        public string IdType { get; set; }//from library idtype
        public string Period { get; set; }//From libraryPeriod
        [Display(Name = "Payroll Status")]
        public string PayrollStatus { get; set; }//from libraryPayrollStatus and LibraryClaimType
        public string Type { get; set; }//from our libraryPayrollType
        public string Tag { get; set; }//from libraryPayrollTag
        [Display(Name = "Current Status")]
        public string Status { get; set; }//from masterlist
        [Display(Name ="Payment Mode")]
        public string PaymentMode { get; set; }//from libraryPaymentMode.
        public string Modified { get; set; }
        [Display(Name = "Created by")]
        public string Created { get; set; }

        public int TotalTarget { get; set; }//Count from our statistics
        public int WaitListed { get; set; }//Count of Waitlisted
        public int Male { get; set; }//Count of males
        public int Female { get; set; }//Count of females
        public double Amount { get; set; }
        public int TotalAmount3000 { get; set; }

        //public int LatestPayroll { get; set; }
    }
}
