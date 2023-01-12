using FitnesCentar.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Windows;
using System.Windows.Forms;

namespace FitnesCentar.Controllers
{
    public class NeprijavljenController : Controller
    {
        // GET: Unregistred
        public ActionResult PocetnaNeprijavljeni(string Option, string typeOfSorting, string sortBy, string Naziv, string Adresa, string DonjaGranica, string GornjaGranica)
        {
            if(Session["users"] == null)
            {
                Session["users"] = HttpContext.Application["users"];
            }
            Users users = (Users)Session["users"];
            if (Option == "Sort")
            {
                ViewBag.FitnessCenters = users.FitnesCenterSorting(typeOfSorting, sortBy);
            }
            else if (Option == "Find")
            {
                int number = 0;
                if (DonjaGranica == "" && GornjaGranica == "")
                {
                    if (Naziv == "" && Adresa == "")
                    {
                        ViewBag.FitnessCenters = users.FitnesCenterSorting("", "");
                    }
                    else
                    {
                        ViewBag.FitnessCenters = users.FitnessCenterSearch(Naziv, Adresa, DonjaGranica, GornjaGranica);
                    }

                }
                else if (DonjaGranica != "" && GornjaGranica == "")
                {
                    if (Int32.TryParse(DonjaGranica, out number))
                    {
                        ViewBag.FitnessCenters = users.FitnessCenterSearch(Naziv, Adresa, DonjaGranica, GornjaGranica);
                    }
                    else
                    {
                        ViewBag.FitnessCenters = users.FitnesCenterSorting("", "");
                        System.Windows.MessageBox.Show("You must enter a number za DonjuGranicu");
                    }
                }
                else if (DonjaGranica == "" && GornjaGranica != "")
                {
                    if (Int32.TryParse(GornjaGranica, out number))
                    {
                        ViewBag.FitnessCenters = users.FitnessCenterSearch(Naziv, Adresa, DonjaGranica, GornjaGranica);
                    }
                    else
                    {
                        ViewBag.FitnessCenters = users.FitnesCenterSorting("", "");
                        System.Windows.MessageBox.Show("You must enter a number za GornjuGranicu");
                    }
                }
                else if (DonjaGranica != "" && GornjaGranica != "")
                {
                    if (Int32.TryParse(DonjaGranica, out number) && Int32.TryParse(GornjaGranica, out number) && DonjaGranica != "")
                    {
                        ViewBag.FitnessCenters = users.FitnessCenterSearch(Naziv, Adresa, DonjaGranica, GornjaGranica);
                    }
                    else
                    {
                        ViewBag.FitnessCenters = users.FitnesCenterSorting("", "");
                        System.Windows.MessageBox.Show("You must enter a number");
                    }
                }
            }
            else
            {
                ViewBag.FitnessCenters = users.FitnesCenterSorting("", "");
            }
      
            //ViewBag.FitnessCenters = users.FitnesCenterSorting("", "");
            return View();
        }
        [HttpPost]
        public ActionResult DetaljanPrikaz(string Naziv)
        {
            Users users = (Users)Session["users"];
            FitnessCenter fitnessCenter = new FitnessCenter();
            users.fitnessCenters.TryGetValue(Naziv, out fitnessCenter);
            ViewBag.FitnessCenter = fitnessCenter;
            ViewBag.GroupTrainings = users.GroupTrainingsOfFitnessCenter(Naziv);
            ViewBag.Komentari = users.ListOfComments(Naziv);
            return View();
        }        
        public ActionResult Registracija()
        {
            return View();
        }
        public ActionResult RegistracijaKorisnika(User user,string DateOfBirth)
        {
            Users users = (Users)Session["users"];
            if (users == null)
            {
                users = new Users();
                Session["users"] = users;
            }

            var temporary = DateOfBirth;
            if (users.UserNameCheck(user.Username))
            {
                System.Windows.MessageBox.Show("Username vec postoji!");
                
            }
            else if (!users.ValidationOfDocumment(user.DateOfBirth))
            {
                System.Windows.MessageBox.Show("Date pogresan!");
            }
            else
            {
                users.SaveUserIntoDatabase(user);
                Session["users"] = users;
            }

            return View("Registracija");
        }      
    }
}