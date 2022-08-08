using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FitensCentar.Models
{
    public class Komentar
    {
        public Komentar(Korisnik posetilac, FitnesCentar fitnesCentar, short ocena, string tekstKomentara)
        {
            Posetilac = posetilac;
            FitnesCentar = fitnesCentar;
            Ocena = ocena;
            TekstKomentara = tekstKomentara;
        }

        public Korisnik Posetilac { get; set; }
        public FitnesCentar FitnesCentar { get; set; } 
        public short Ocena { get; set; }
        public string TekstKomentara { get; set; }

    }
}