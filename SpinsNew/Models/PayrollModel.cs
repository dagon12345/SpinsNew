using SpinsNew.Libraries;
using SpinsNew.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SpinsNew.Models
{
    public class PayrollModel 
    {
        //Child model of masterlist/Barangay
        public int ID { get; set; }
        public int MasterListID { get; set; }
        public int PSGCRegion { get; set; }
        public int PSGCProvince { get; set; }//Province
        public int PSGCCityMun { get; set; }//Municipality
        public int PSGCBrgy { get; set; }
        [Display(Name = "Full Address")]
        public string Address { get; set; }
        public double Amount { get; set; }
        public int Year { get; set; }
        public int PeriodID { get; set; }

        public int PayrollStatusID { get; set; }
        public int? ClaimTypeID { get; set; }

        [Display(Name = "Date Claimed")]
        public DateTime? DateClaimedFrom { get; set; }
        public DateTime? DateClaimedTo { get; set; }

        public string Remarks { get; set; }
        public int PayrollTypeID { get; set; }
        public int PayrollTagID { get; set; }
        public int PaymentModeID { get; set; }
        [DisplayFormat(DataFormatString = "MMM-dd-yyyy")]
        public DateTime? DateTimeEntry { get; set; }
        public string EntryBy { get; set; }



        [DisplayFormat(DataFormatString = "MMM-dd-yyyy")]
        public DateTime? DateTimeModified { get; set; }
        public string ModifiedBy { get; set; }


        //public int LatestPayroll { get; set; }//experiment
        /* Required reference navigation to principal*/
        public MasterListModel MasterListModel { get; set; } = null;
        public LibraryBarangay LibraryBarangay { get; set; } = null;
        public LibraryPeriod LibraryPeriod { get; set; }
        public LibraryPayrollStatus LibraryPayrollStatus  { get; set; }
        public LibraryClaimType LibraryClaimType { get; set; }
        public LibraryPayrollType LibraryPayrollType { get; set; }
        public LibraryPayrollTag LibraryPayrollTag { get; set; }
        public LibraryPaymentMode LibraryPaymentMode { get; set; }
        public LibraryProvince LibraryProvince { get; set; }
        public LibraryMunicipality LibraryMunicipality { get; set; }
        //public ICollection<LibraryProvince> LibraryProvinces { get; } = new List<LibraryProvince>();
        //public ICollection<GisModel> GisModels { get; } = new List<GisModel>();
        public LibraryYear LibraryYear { get; set; }
        //public GisModel GisModel { get; set; }
        //public ICollection<PayrollandGisManyToMany> PayrollandGisManyToManys { get; } = new List<PayrollandGisManyToMany>(); //Linking Entitiy many to many relationship


    }
}
