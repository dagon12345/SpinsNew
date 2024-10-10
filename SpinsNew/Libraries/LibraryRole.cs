using SpinsNew.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SpinsNew.Libraries
{
    public class LibraryRole
    {
        [Key]
        public int UserRoleId { get; set; }
        public string Role { get; set; }

        public override string ToString()
        {
            return Role; 
        }

        public ICollection<RegisterModel> RegisterModels { get; set; } = new List<RegisterModel>();
    }
}
