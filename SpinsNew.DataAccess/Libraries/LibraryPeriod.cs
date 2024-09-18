using SpinsNew.DataAccess.Models;
using System.Collections.Generic;

namespace SpinsNew.DataAccess.Libraries
{
    public class LibraryPeriod
    {
        public int Id { get; set; }
        public int PeriodID { get; set; }
        public string Period { get; set; }
        public string Abbreviation { get; set; }
        public string Months { get; set; }
        public string YearsUsed { get; set; }
        public int StipendMultiplier { get; set; }


        
        public override string ToString()
        {
            return $"{Period} ({Abbreviation}) {Months}"; // override to  Display Period and Abbreviation, month in the ComboBox
        }



        public ICollection<PayrollModel> PayrollModels { get; } = new List<PayrollModel>();//Get reference from our principal model PayrollModel

    }
}
