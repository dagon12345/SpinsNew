using SpinsNew.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SpinsNew.Libraries
{
    public class LibraryMunicipality
    {
        [Key]
        public int PSGCCityMun { get; set; }
        public string CityMunName { get; set; }
        public int PSGCProvince { get; set; }
        public int District { get; set; }

        public override string ToString()
        {
            return CityMunName;
        }
        public virtual ICollection<PayrollModel> PayrollModels { get; } = new List<PayrollModel>();//Our parent is our tbl_payroll_socpen
        public virtual ICollection<MasterListModel> MasterListModels { get; } = new List<MasterListModel>();

        public LibraryProvince LibraryProvince { get; set; } // Foreign key to Library Province
        public ICollection<LibraryBarangay> LibraryBarangays { get; set; } = new List<LibraryBarangay>();


    }
}
