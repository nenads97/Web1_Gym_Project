using FitnesCentar.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Windows;

namespace FitnesCentar.Controllers
{
    public class TrainerController : Controller
    {
        // GET: Trainer
        public ActionResult ProfilTrenera(string Username,string Sacuvaj,User user)
        {
            Users users = (Users)Session["users"];
            if (!users.UserNameCheck(Username))
            {
                return RedirectToAction("PocetnaNeprijavljeni","Neprijavljeni");
            }
            if (Sacuvaj != null)
            {
                User oldUser = new User();
                oldUser = users.UserData(Username);
                user.Role = oldUser.Role;
                users.ChangeUser(user, oldUser);
                Session["users"] = users;
                MessageBox.Show("Uspesno izmenjen user");               
            }
            ViewBag.user_name = Username;
            ViewBag.FitnessCenter = users.TrainerFitnessCenter(Username);
            ViewBag.TrenerPodaci = users.UserData(Username);         
            return View();
        }
       
        public ActionResult ZavrseniTreninziTrener(string Username,string Option,string typeOfSorting,string sortBy,string Name,string TypeOfTraining,string LowerLimit,string UpperLimit)
        {
            Users users = (Users)Session["users"];
            if (!users.UserNameCheck(Username))
            {
                return RedirectToAction("PocetnaNeprijavljeni", "Neprijavljeni");
            }
            if(Option == "Sort")
            {
                ViewBag.GroupTrainings = users.GroupTrainingSorting(typeOfSorting, sortBy, Username);
            }
            else if(Option == "Find")
            {
                DateTime date = new DateTime();
                if (LowerLimit == "" && UpperLimit == "")
                {
                    if (Name == "" && TypeOfTraining == "")
                    {
                        ViewBag.GroupTrainings = users.GroupTrainingSorting("", "",Username);
                    }
                    else
                    {
                        ViewBag.GroupTrainings = users.GroupTrainingSearch(Name, TypeOfTraining, LowerLimit, UpperLimit,Username);
                    }

                }
                else if (LowerLimit != "" && UpperLimit == "")
                {
                    if (DateTime.TryParse(LowerLimit, out date))
                    {
                        ViewBag.GroupTrainings = users.GroupTrainingSearch(Name, TypeOfTraining, LowerLimit, UpperLimit,Username);
                    }
                    else
                    {
                        ViewBag.GroupTrainings = users.GroupTrainingSorting("", "",Username);
                        MessageBox.Show("Morate uneti datum za DonjuGranicu");
                    }
                }
                else if (LowerLimit == "" && UpperLimit != "")
                {
                    if (DateTime.TryParse(UpperLimit, out date))
                    {
                        ViewBag.GroupTrainings = users.GroupTrainingSearch(Name, TypeOfTraining, LowerLimit, UpperLimit, Username);
                    }
                    else
                    {
                        ViewBag.GroupTrainings = users.GroupTrainingSorting("", "",Username);
                        MessageBox.Show("Morate uneti datum za GornjuGranicu");
                    }
                }
                else if (LowerLimit != "" && UpperLimit != "")
                {
                    if (DateTime.TryParse(LowerLimit, out date) && DateTime.TryParse(UpperLimit, out date))
                    {
                        ViewBag.GroupTrainings = users.GroupTrainingSearch(Name, TypeOfTraining, LowerLimit, UpperLimit,Username);
                    }
                    else
                    {
                        ViewBag.GroupTrainings = users.GroupTrainingSorting("", "",Username);
                        MessageBox.Show("You must enter a number");
                    }
                }
            }
            else
            {
                ViewBag.GroupTrainings = users.GroupTrainingsOfTrainer(Username);
            }           

            ViewBag.Posetioci = users.visitorsGroupTraining;
            ViewBag.user_name = Username;           
            return View();
        }
        public ActionResult PredstojeciTreninziTrener(string Username,GroupTraining GrupniTrening,string FitnessCenterName,string Name,string Option,string Date,string Vreme)
        {
            Users users = (Users)Session["users"];
            if (!users.UserNameCheck(Username))
            {
                return RedirectToAction("PocetnaNeprijavljeni", "Neprijavljeni");
            }
            if (Option == "Kreiraj")
            {               
                GrupniTrening.TrainingDate = Date +" "+Vreme;
                GrupniTrening.FitnessCenterName = users.TrainerFitnessCenter(Username);               
                if (DateTime.Parse(GrupniTrening.TrainingDate) > DateTime.Parse("2022/07/10 00:00"))
                {
                    users.SaveGroupTrainingIntoDatabase(GrupniTrening, Username);
                    MessageBox.Show($"Uspesno dodat {GrupniTrening.Name}");
                    Session["users"] = users;
                }
                else
                {
                    MessageBox.Show($"{GrupniTrening.Name} nedovoljno daleko u buducnost");
                }
                
            }
            else if (Option == "Obrisi")
            {
                if (users.visitorsGroupTraining.ContainsKey(GrupniTrening.Name))
                {
                    MessageBox.Show($"{GrupniTrening.Name} ima prijavljene posetioce");
                }
                else
                {
                    users.DeleteGroupTraining(GrupniTrening.Name, Username);
                    MessageBox.Show($"Uspesno obrisan {GrupniTrening.Name}");
                    Session["users"] = users;
                }
               
            }
            else if (Option == "Izmeni")
            {
                users.ChangeGroupTraining(GrupniTrening);
                MessageBox.Show($"Uspesno izmenjen {GrupniTrening.Name}");
                Session["users"] = users;
            }

            ViewBag.GroupTrainings = users.GroupTrainingsOfTrainer(Username);
            ViewBag.Posetioci = users.visitorsGroupTraining;            
            ViewBag.user_name = Username;
            return View();
        }
        public ActionResult DetaljanPrikazTrener(string Name,string Username)
        {
            Users users = (Users)Session["users"];
            if (!users.UserNameCheck(Username))
            {
                return RedirectToAction("PocetnaNeprijavljeni", "Neprijavljeni");
            }
            FitnessCenter fitnessCenter = new FitnessCenter();
            ViewBag.user_name = Username;
            users.fitnessCenters.TryGetValue(Name, out fitnessCenter);
            ViewBag.FitnessCenter = fitnessCenter;
            ViewBag.GroupTrainings = users.GroupTrainingsOfFitnessCenter(Name);
            ViewBag.Komentari = users.ListOfComments(Name);
            return View();
        }
    }
}