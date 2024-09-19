using SpinsNew.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SpinsNew.ViewModel
{
    public class MasterListViewModel : MasterListModel
    {
        public string Municipality { get; set; }
        public string Barangay { get; set; }
    
        public int? Age { get; set; }//Add age property
        public string Sex { get; set; }//Sex property
        [Display(Name = "Marital Status")]
        public string MaritalStatus { get; set; }//marital Status
        [Display(Name = "ID Type")]
        public string IdType { get; set; }
        [Display(Name = "Health Status")]
        public string HealthStatus { get; set; }
        [Display(Name ="Data Source")]
        public string DataSource { get; set; }
        public string Status { get; set; }
        public string Registration { get; set; }
        public string Assessment { get; set; }//From Table GIS
        [Display(Name ="GIS")]
        public int? ReferenceCode { get; set; }
        [Display(Name ="SPIS Batch")]
        public int? SpisBatch { get; set; }
        [Display(Name = "SPBUF")]
        public Int64? Spbuf { get; set; }//Int64 is a bigint can handle large amount of integers
    }
}
