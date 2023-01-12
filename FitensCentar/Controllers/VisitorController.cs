using FitnesCentar.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Windows;

namespace FitnesCentar.Controllers
{
    public class VisitorController : Controller
    {
        // GET: Visitor
        public ActionResult ProfilPosetioca(string Username,string Sacuvaj,User user)
        {
            Users users = (Users)Session["users"];

            if (users == null)
            {
                users = new Users();
                Session["users"] = users;
            }
            if(Sacuvaj != null)
            {
                User oldUser = new User();
                oldUser = users.UserData(Username);
                users.ChangeUser(user, oldUser);
                Session["users"] = users;
                MessageBox.Show("Uspesno izmenjen user");               
            }                 
            ViewBag.user_name = Username;
            ViewBag.PosetilacPodaci = users.UserData(Username);
            return View();
        }
        
        public ActionResult GrupniTreninziPosetilac(string Username,string Option,string typeOfSorting,string sortBy, string Naziv, string TypeOfTraining, string DonjaGranica, string GornjaGranica)
        {
            Users users = (Users)Session["users"];
            if (Option == "Sort")
            {
                ViewBag.GroupTrainings = users.GroupTrainingSorting(typeOfSorting, sortBy,Username);
            }
            else if (Option == "Find")
            {
                DateTime date = new DateTime();
                if (DonjaGranica == "" && GornjaGranica == "")
                {
                    if (Naziv == "" && TypeOfTraining == "")
                    {
                        ViewBag.GroupTrainings = users.GroupTrainingSorting("", "", Username);
                    }
                    else
                    {
                        ViewBag.GroupTrainings = users.GroupTrainingSearch(Naziv, TypeOfTraining, DonjaGranica, GornjaGranica, Username);
                    }

                }
                else if (DonjaGranica != "" && GornjaGranica == "")
                {
                    if (DateTime.TryParse(DonjaGranica, out date))
                    {
                        ViewBag.GroupTrainings = users.GroupTrainingSearch(Naziv, TypeOfTraining, DonjaGranica, GornjaGranica, Username);
                    }
                    else
                    {
                        ViewBag.GroupTrainings = users.GroupTrainingSorting("", "", Username);
                        MessageBox.Show("Morate uneti datum za DonjuGranicu");
                    }
                }
                else if (DonjaGranica == "" && GornjaGranica != "")
                {
                    if (DateTime.TryParse(GornjaGranica, out date))
                    {
                        ViewBag.GroupTrainings = users.GroupTrainingSearch(Naziv, TypeOfTraining, DonjaGranica, GornjaGranica, Username);
                    }
                    else
                    {
                        ViewBag.GroupTrainings = users.GroupTrainingSorting("", "", Username);
                        MessageBox.Show("Morate uneti datum za GornjuGranicu");
                    }
                }
                else if (DonjaGranica != "" && GornjaGranica != "")
                {
                    if (DateTime.TryParse(DonjaGranica, out date) && DateTime.TryParse(GornjaGranica, out date))
                    {
                        ViewBag.GroupTrainings = users.GroupTrainingSearch(Naziv, TypeOfTraining, DonjaGranica, GornjaGranica, Username);
                    }
                    else
                    {
                        ViewBag.GroupTrainings = users.GroupTrainingSorting("", "", Username);
                        MessageBox.Show("You must enter a number");
                    }
                }
            }
            else
            {
               ViewBag.GroupTrainings = users.GroupTrainingsOfVisitors(Username);
            }
            ViewBag.user_name = Username;            
            return View();
        }
        public ActionResult DetaljanPrikazPosetilac(string Naziv,string NameOfGroupTraining,string Username,Comment Komentar,string Option)
        {
            Users users = (Users)Session["users"];        
            if (Option == "Komentarisi")
            {
                if (users.CheckVisitorComment(Username, Naziv))
                {
                    Komentar.Visibility = "CekaOdobrenje";
                    users.SaveCommentIntoDatabase(Komentar);
                    Session["users"] = users;
                    MessageBox.Show($"Komentar ceka odobrenje");
                }
                else
                {
                    MessageBox.Show($"Nisam bio u {Naziv}");
                }
               
            }
            else if(Option == "Prijavi")
            {
                if (!users.TrainerLoginCheck(Username, NameOfGroupTraining))
                {
                    users.AddGroupTrainingVisitor(Username,NameOfGroupTraining);
                    Session["users"] = users;
                    MessageBox.Show($"Uspesno prijavljen training");
                }
                else
                {
                    MessageBox.Show($"Popunjena mesta {NameOfGroupTraining} ili sam se vec prijavio");
                }
            }

            FitnessCenter fitnessCenter = new FitnessCenter();
            ViewBag.user_name = Username;
            users.fitnessCenters.TryGetValue(Naziv, out fitnessCenter);
            ViewBag.FitnessCenter = fitnessCenter;
            ViewBag.GroupTrainings = users.GroupTrainingsOfFitnessCenter(Naziv);          
            ViewBag.Komentari = users.ListOfComments(Naziv);            
           
            return View();
        }
    }
}