using FitnesCentar.Models.Enumeracija;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FitnesCentar.Models
{
    public class User
    {
        public string Username { get; set; } //Treba da je jedinstveno
        public string Password { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Gender { get; set; }
        public string Email { get; set; }
        public string DateOfBirth { get; set; } //format dd/MM/yyyy 
        public UserRole Role { get; set; }
        public bool Deleted { get; set; }
        public User()
        {

        }
      
        public User(string userName, string lozinka, string ime, string prezime, string pol, string email, string datumRodjenja, UserRole uloga, bool deleted)
        {
            Username = userName;
            Password = lozinka;
            Name = ime;
            Surname = prezime;
            Gender = pol;
            Email = email;
            DateOfBirth = datumRodjenja;
            Role = uloga;
            Deleted = deleted;
        }
    }
}