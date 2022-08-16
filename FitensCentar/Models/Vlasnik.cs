using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FitensCentar.Models
{
    public class Vlasnik : Korisnik
    {
        public Vlasnik(string username, string password, string imeKorisnika, string prezimeKorisnika, Pol pol, string emailKorisnika, string datumRodjenja) : base(username,password,imeKorisnika,prezimeKorisnika,pol,emailKorisnika,datumRodjenja)
        {
            _Uloga = Uloga.VLASNIK;
            ftCentri = new List<FitnesCentar>();
        }

        public Vlasnik()
        {

        }

        public List<FitnesCentar> ftCentri { get; set; }
    }
}