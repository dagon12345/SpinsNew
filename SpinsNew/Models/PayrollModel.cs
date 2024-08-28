﻿using System;

namespace SpinsNew.Models
{
    public class PayrollModel
    {
        public int ID { get; set; }
        public int MasterListID { get; set; }
        public int PSGCRegion { get; set; }
        public int PSGCProvince { get; set; }
        public int PSGCCityMun { get; set; }
        public int PSGCBrgy { get; set; }
        public string Address { get; set; }
        public double Amount { get; set; }
        public int Year { get; set; }
        public int PeriodID { get; set; }
        public int PayrollStatusID { get; set; }
        public int? ClaimTypeID { get; set; }
        public int PayrollTypeID { get; set; }
        public int PayrollTagID { get; set; }
        public int PaymentModeID { get; set; }
        public DateTime DateTimeEntry { get; set; }
        public string EntryBy { get; set; }


        public DateTime? DateClaimedFrom { get; set; }
        public DateTime? DateClaimedTo { get; set; }
        public string Remarks { get; set; }
        public DateTime? DateTimeModified { get; set; }
        public string ModifiedBy { get; set; }


    }
}
