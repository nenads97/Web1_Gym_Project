using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FitensCentar.Models
{
    public class Trener : Korisnik
    {
        public Trener(string username, string password, string imeKorisnika, string prezimeKorisnika, Pol pol, string emailKorisnika, string datumRodjenja)
            : base(username, password, imeKorisnika, prezimeKorisnika, pol, emailKorisnika, datumRodjenja)
        {
            _Uloga = Uloga.TRENER;
            grTreninzi = new List<GrupniTrening>();
            ftCentri = new List<FitnesCentar>();
        }

        public Trener()
        {

        }

        public List<GrupniTrening> grTreninzi { get; set; }
        public List<FitnesCentar> ftCentri { get; set; }
    }
}