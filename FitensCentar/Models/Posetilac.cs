using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FitensCentar.Models
{
    public class Posetilac : Korisnik
    {
        public Posetilac(string username, string password, string imeKorisnika, string prezimeKorisnika, Pol pol, string emailKorisnika, string datumRodjenja) 
            : base(username, password, imeKorisnika, prezimeKorisnika, pol, emailKorisnika, datumRodjenja)
        {
            _Uloga = Uloga.POSETILAC;
            listaGrTreninga = new List<GrupniTrening>();
        }

        public Posetilac()
        {

        }
    
        public List<GrupniTrening> listaGrTreninga { get; set; }
    }
}