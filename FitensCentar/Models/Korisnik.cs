using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FitensCentar.Models
{
    public enum Pol
    {
        MUSKI = 0,
        ZENSKI,
        DRUGO
    }

    public enum Uloga
    {
        VLASNIK = 0,
        TRENER,
        POSETILAC
    }


    public class Korisnik
    {
        public Korisnik()
        {

        }

        public Korisnik(string username, string password, string imeKorisnika, string prezimeKorisnika, Pol pol, string emailKorisnika, string datumRodjenja)
        {
            Username = username;
            Password = password;
            ImeKorisnika = imeKorisnika;
            PrezimeKorisnika = prezimeKorisnika;
            _Pol = pol;
            EmailKorisnika = emailKorisnika;
            DatumRodjenja = datumRodjenja;
        }

        public string Username { get; set; }
        public string Password { get; set; }
        public string ImeKorisnika { get; set; }
        public string PrezimeKorisnika { get; set; }
        public Pol _Pol { get; set; }
        public string EmailKorisnika { get; set; }
        public string DatumRodjenja  { get; set; }
        public Uloga _Uloga { get; set; }

        public void SettujPol(string pol)
        {
            if(pol.Equals(Pol.MUSKI.ToString()))
            {
                _Pol = Pol.MUSKI;
            }
            else if(pol.Equals(Pol.ZENSKI.ToString()))
            {
                _Pol = Pol.ZENSKI;
            }
            else if(pol.Equals(Pol.DRUGO.ToString()))
            {
                _Pol = Pol.DRUGO;
            }
        }
    }

    
}