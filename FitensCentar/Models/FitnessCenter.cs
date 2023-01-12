using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FitnesCentar.Models
{
    public class FitnessCenter
    {
        public string Naziv { get; set; }
        public string Adresa { get; set; } // mozda da bude string[], format ulica x broj,mesto/grad,postanski broj
        public int DatumOtvaranja { get; set; }
        public string AdminUsername { get; set; }
        public int MonthlySubscription { get; set; }
        public int YearlySubscription { get; set; }
        public int PriceOfOneTraining { get; set; }
        public int GroupTrainingPrice { get; set; }
        public int PersonalTrainerPrice { get; set; }
        public bool Deleted { get; set; }

        public FitnessCenter()
        {

        }

        public FitnessCenter(string name, string adress, int openingDate, string adminUsername, int monthlySubscription, int yearlySubscription, int priceOfOneTraining, int groupTrainingPrice, int personalTrainerPrice, bool deleted)
        {
            Naziv = name;
            Adresa = adress;
            DatumOtvaranja = openingDate;
            AdminUsername = adminUsername;
            MonthlySubscription = monthlySubscription;
            YearlySubscription = yearlySubscription;
            PriceOfOneTraining = priceOfOneTraining;
            GroupTrainingPrice = groupTrainingPrice;
            PersonalTrainerPrice = personalTrainerPrice;
            Deleted = deleted;
        }

        
    }
}