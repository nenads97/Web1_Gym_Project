using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FitensCentar.Models
{
    public class FitnesCentar
    {
        public FitnesCentar(string naziv, string adresa, int godinaOtvaranja, Korisnik vlasnik, double mesecnaClanarina, double godisnjaClanarina, double cenaJednogTreninga, double cenaGrupnogTreninga, double cenaPersonalniTrener)
        {
            Naziv = naziv;
            Adresa = adresa;
            GodinaOtvaranja = godinaOtvaranja;
            Vlasnik = vlasnik;
            MesecnaClanarina = mesecnaClanarina;
            GodisnjaClanarina = godisnjaClanarina;
            CenaJednogTreninga = cenaJednogTreninga;
            CenaGrupnogTreninga = cenaGrupnogTreninga;
            CenaPersonalniTrener = cenaPersonalniTrener;
        }

        public string Naziv { get; set; }
        public string Adresa { get; set; }
        public int GodinaOtvaranja { get; set; }
        public Korisnik Vlasnik { get; set; }
        public double MesecnaClanarina { get; set; }
        public double GodisnjaClanarina { get; set; }
        public double CenaJednogTreninga { get; set; }
        public double CenaGrupnogTreninga { get; set; }
        public double CenaPersonalniTrener { get; set; }

    }

    

    
}