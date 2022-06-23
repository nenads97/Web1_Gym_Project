using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FitensCentar.Models
{
    public class Korisnik
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string ImeKorisnika { get; set; }
        public string PrezimeKorisnika { get; set; }
        public Pol Pol { get; set; }
        public string EmailKorisnika { get; set; }
        public DateTime DatumRodjenja  { get; set; }
        public Uloga Uloga { get; set; }
    }
}