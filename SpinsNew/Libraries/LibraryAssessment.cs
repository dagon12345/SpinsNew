﻿using SpinsNew.Models;
using System.Collections.Generic;

namespace SpinsNew.Libraries
{
    public class LibraryAssessment
    {
        public int Id { get; set; }
        public string Assessment { get; set; }

        public override string ToString()
        {
            return Assessment;
        }
        public ICollection<GisModel> GisModels { get; } = new List<GisModel>();
    }
}
