using SpinsNew.Models;
using System.Collections.Generic;

namespace SpinsNew.Libraries
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

        public ICollection<PayrollModel> PayrollModels { get; } = new List<PayrollModel>();//Get reference from our principal model PayrollModel

    }
}
