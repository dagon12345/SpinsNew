using SpinsNew.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpinsNew.ViewModel
{
    public class AuthorizeRepViewModel : TableAuthRepresentative
    {
        [Display (Name = "Relationship")]
        public string English { get; set; }

    }
}
