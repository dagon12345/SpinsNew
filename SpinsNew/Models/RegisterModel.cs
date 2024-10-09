using System;
using System.ComponentModel.DataAnnotations;

namespace SpinsNew.Models
{
    public class RegisterModel
    {
        public int Id { get; set; }
        [Display (Name = "Last Name")]
        public string Lastname { get; set; }
        [Display(Name = "First Name")]
        public string Firstname { get; set; }
        [Display(Name = "Middle Name")]
        public string Middlename { get; set; }
        [Display(Name = "Birth Date")]
        public DateTime Birthdate { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        [Display(Name = "Role")] // 1 is Admin, 2 Payroll Incharge, 3 Encoders.
        public int UserRole { get; set; }
        [Display(Name = "Date Registered")]
        public DateTime DateRegistered { get; set; }
        [Display(Name = "Activation")]
        public bool IsActive { get; set; }
    }
}
