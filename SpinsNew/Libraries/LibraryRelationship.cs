using SpinsNew.Models;

namespace SpinsNew.Libraries
{
    public class LibraryRelationship
    {
        public int Id { get; set; }
        public string English { get; set; }

        public override string ToString()
        {
            return English; 
        }

        //Has one Authorize representative
        public TableAuthRepresentative TableAuthRepresentative { get; set; }
    }
}
