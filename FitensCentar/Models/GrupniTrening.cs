using System;
using System.Collections.Generic;

namespace FitensCentar.Models
{
    public class GrupniTrening
    {
        public GrupniTrening(string naziv, TipTreninga trening, FitnesCentar fitCentar, DateTime vremeTreninga, int maxBrojPosetilaca, List<Korisnik> spisakPosetilaca)
        {
            Naziv = naziv;
            Trening = trening;
            FitCentar = fitCentar;
            VremeTreninga = vremeTreninga;
            MaxBrojPosetilaca = maxBrojPosetilaca;
            SpisakPosetilaca = spisakPosetilaca;
        }

        public string Naziv { get; set; }
        public TipTreninga Trening { get; set; }
        public FitnesCentar FitCentar { get; set; }
        public DateTime VremeTreninga { get; set; }
        public int MaxBrojPosetilaca { get; set; }
        public List<Korisnik> SpisakPosetilaca { get; set; }

    }
}