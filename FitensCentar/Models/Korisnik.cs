using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FitensCentar.Models
{
    public class Korisnik
    {
        public string username { get; set; }
        public string password { get; set; }
        public string imeKorisnika { get; set; }
        public string prezimeKorisnika { get; set; }
        public Pol pol { get; set; }
        public string emailKorisnika { get; set; }
        public DateTime datumRodjenja  { get; set; }
        public Uloga uloga { get; set; }
    }
}