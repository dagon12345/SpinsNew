using System;

namespace SpinsNew.Models
{
    public class RegisterModel
    {
        public int Id { get; set; }
        public string Lastname { get; set; }
        public string Firstname { get; set; }
        public string Middlename { get; set; }
        public DateTime Birthdate { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public int UserRole { get; set; }
        public DateTime DateRegistered { get; set; }
        public bool IsActive { get; set; }
    }
}
