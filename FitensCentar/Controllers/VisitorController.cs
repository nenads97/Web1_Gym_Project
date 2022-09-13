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
        
        public ActionResult GrupniTreninziPosetilac(string Username,string Option,string typeOfSorting,string sortBy, string Name, string TypeOfTraining, string LowerLimit, string UpperLimit)
        {
            Users users = (Users)Session["users"];
            if (Option == "Sort")
            {
                ViewBag.GroupTrainings = users.GroupTrainingSorting(typeOfSorting, sortBy,Username);
            }
            else if (Option == "Find")
            {
                DateTime date = new DateTime();
                if (LowerLimit == "" && UpperLimit == "")
                {
                    if (Name == "" && TypeOfTraining == "")
                    {
                        ViewBag.GroupTrainings = users.GroupTrainingSorting("", "", Username);
                    }
                    else
                    {
                        ViewBag.GroupTrainings = users.GroupTrainingSearch(Name, TypeOfTraining, LowerLimit, UpperLimit, Username);
                    }

                }
                else if (LowerLimit != "" && UpperLimit == "")
                {
                    if (DateTime.TryParse(LowerLimit, out date))
                    {
                        ViewBag.GroupTrainings = users.GroupTrainingSearch(Name, TypeOfTraining, LowerLimit, UpperLimit, Username);
                    }
                    else
                    {
                        ViewBag.GroupTrainings = users.GroupTrainingSorting("", "", Username);
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
                        ViewBag.GroupTrainings = users.GroupTrainingSorting("", "", Username);
                        MessageBox.Show("Morate uneti datum za GornjuGranicu");
                    }
                }
                else if (LowerLimit != "" && UpperLimit != "")
                {
                    if (DateTime.TryParse(LowerLimit, out date) && DateTime.TryParse(UpperLimit, out date))
                    {
                        ViewBag.GroupTrainings = users.GroupTrainingSearch(Name, TypeOfTraining, LowerLimit, UpperLimit, Username);
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
        public ActionResult DetaljanPrikazPosetilac(string Name,string NameOfGroupTraining,string Username,Comment Komentar,string Option)
        {
            Users users = (Users)Session["users"];        
            if (Option == "Komentarisi")
            {
                if (users.CheckVisitorComment(Username, Name))
                {
                    Komentar.Visibility = "CekaOdobrenje";
                    users.SaveCommentIntoDatabase(Komentar);
                    Session["users"] = users;
                    MessageBox.Show($"Komentar ceka odobrenje");
                }
                else
                {
                    MessageBox.Show($"Nisam bio u {Name}");
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
            users.fitnessCenters.TryGetValue(Name, out fitnessCenter);
            ViewBag.FitnessCenter = fitnessCenter;
            ViewBag.GroupTrainings = users.GroupTrainingsOfFitnessCenter(Name);          
            ViewBag.Komentari = users.ListOfComments(Name);            
           
            return View();
        }
    }
}