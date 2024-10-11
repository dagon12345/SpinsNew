using SpinsNew.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SpinsNew.Libraries
{
    public class LibrarylivCondition
    {
        [Key]
        public int Id { get; set; }
        public string LivingConditions { get; set; }

        public override string ToString()
        {
            return LivingConditions; 
        }
        public ICollection<GisModel> gisModels { get; set; } = new List<GisModel>();
    }
}
