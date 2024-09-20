using SpinsNew.Models;
using System.Collections.Generic;

namespace SpinsNew.Libraries
{
    public class LibraryValidator
    {
        public int Id { get; set; }
        public string Validator { get; set; }

        public override string ToString()
        {
            return Validator;
        }

        public ICollection<GisModel> gisModels { get; set; } = new List<GisModel>();
    }
}
