using FitnesCentar.Models.Enumeracija;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FitnesCentar.Models
{
    public class GroupTraining
    {
        public string Name { get; set; }
        public TrainingType Trening { get; set; }
        public string FitnessCenterName { get; set; }
        public string TrainingDuration { get; set; } //combobox za ovo,15m 30m,45m,60m
        public string TrainingDate { get; set; }  //(čuvati u formatu dd/MM/yyyy HH:mm)       
        public int MaximumNumberOfVisitors { get; set; }
        public bool Deleted { get; set; }

        public GroupTraining()
        {

        }

        public GroupTraining(string name, TrainingType training, string fitnessCenterName, string trajanjeTreninga, string datumTreninga, int maxBrojPosetilaca,bool deleted)
        {
            Name = name;
            Trening = training;
            FitnessCenterName = fitnessCenterName;
            TrainingDuration = trajanjeTreninga;
            TrainingDate = datumTreninga;
            MaximumNumberOfVisitors = maxBrojPosetilaca;          
            Deleted = deleted;
        }
    }
}