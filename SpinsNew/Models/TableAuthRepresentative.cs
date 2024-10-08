using SpinsNew.Libraries;
using System;
using System.Collections.Generic;

namespace SpinsNew.Models
{
    public class TableAuthRepresentative
    {
        public int Id { get; set; }
        public int MasterListId { get; set; }
        public int ReferenceCode { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string ExtName { get; set; }
        public int RelationshipId { get; set; }
        public DateTime? DateTimeEntry { get; set; }
        public int ValidationTypeId { get; set; }

        //Has one gis model reference code
        public GisModel GisModel { get; set; }
        //Has many LibraryRelationships
        public ICollection<LibraryRelationship> LibraryRelationships { get; set; } = new List<LibraryRelationship>();
    }
}
