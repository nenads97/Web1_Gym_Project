using FitnesCentar.Models;
using FitnesCentar.Models.Enumeracija;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Windows;

namespace FitnesCentar.Controllers
{
    public class VlasnikController : Controller
    {
        // GET: Vlasnik
        public ActionResult ProfilVlasnika(string Username,string Sacuvaj,User user)
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
                user.Role = oldUser.Role;
                users.ChangeUser(user, oldUser);
                Session["users"] = users;
                MessageBox.Show("Uspesno izmenjen user");
            }
            ViewBag.user_name = Username;
            ViewBag.VlasnikPodaci = users.UserData(Username);
            ViewBag.FitnessCentersOfAdmin = users.FitnessCentersOfAdmin(Username);
            return View();
        }       
        public ActionResult Komentari(string Username,string Option,Comment k)
        {
            Users users = (Users)Session["users"];
            if (Option == "Odobri")
            {
                users.ChangeComment(k, Username, "Odobreno");
                Session["users"] = users;
            }
            else if(Option == "Odbij")
            {
                users.ChangeComment(k, Username, "Odbijeno");
                Session["users"] = users;
            }
            ViewBag.ListOfComments = users.ListOfCommentsAdmin(Username);
            ViewBag.user_name = Username;
            return View();
        }
        [HttpPost]
        public ActionResult FitnesCentriVlasnik(string Username,string Option,FitnessCenter fitnessCenter)
        {
            Users users = (Users)Session["users"];           
            if (Option == "Kreiraj")
            {
                fitnessCenter.AdminUsername = Username;              
                users.SaveFitnessCenterIntoDatabase(fitnessCenter);              
                MessageBox.Show($"Uspesno dodat {fitnessCenter.Naziv}");
                Session["users"] = users;
            }
            else if(Option == "Obrisi")
            {
                if (!users.CheckFutureTrainingsFitnessCenter(fitnessCenter.Naziv))
                {
                    users.DeleteFitnessCenter(fitnessCenter.Naziv, fitnessCenter.AdminUsername);
                    MessageBox.Show($"Uspesno obrisan {fitnessCenter.Naziv}");
                    Session["users"] = users;
                }
                else
                {
                    MessageBox.Show($"{fitnessCenter.Naziv} ima treninge u buducnosti");
                }
                
            }
            else if(Option == "Izmeni")
            {
                users.ChangeFitnessCenter(fitnessCenter);
                MessageBox.Show($"Uspesno izmenjen {fitnessCenter.Naziv}");
                Session["users"] = users;
            }
            ViewBag.FitnessCenters = users.FitnessCentersOfAdmin2(Username);
            ViewBag.user_name = Username;
            return View();
        }
        public ActionResult DetaljanPrikazVlasnik(string Naziv,string Username)
        {
            Users users = (Users)Session["users"];
            FitnessCenter fitnessCenter = new FitnessCenter();
            ViewBag.user_name = Username;
            users.fitnessCenters.TryGetValue(Naziv, out fitnessCenter);
            ViewBag.FitnessCenter = fitnessCenter;
            ViewBag.GroupTrainings = users.GroupTrainingsOfFitnessCenter(Naziv);
            ViewBag.Komentari = users.ListOfComments(Naziv);
            return View();
        }
        
        [HttpPost]
        public ActionResult PregledTrenera(string Username,string AdminUsername,User user,string Kreiraj,string FitnessCenter,string Blokiraj)
        {
            Users users = (Users)Session["users"];
            if(Kreiraj == "Kreiraj")
            {
                user.Role = UserRole.TRAINER;
                if (!users.UserNameCheck(user.Username))
                {
                    users.SaveUserIntoDatabase(user);
                    users.SaveTrainerFitnessCenterIntoDatabase(Username, FitnessCenter);                  
                    MessageBox.Show($"Uspesno dodat {Username}");
                    Session["users"] = users;
                }
                else {
                    MessageBox.Show("Postoji user sa tim korisnickim imenom!");
                }              
            }
            if(Blokiraj == "Blokiraj")
            {
                users.BlockTrainer(Username);
                Session["users"] = users;
                MessageBox.Show($"Uspesno blokiran {Username}");
            }
            ViewBag.FitnessCenters = users.FitnessCentersOfAdmin(AdminUsername);
            ViewBag.user_name = AdminUsername;
            ViewBag.Treneri = users.TrainersOfAdmins(AdminUsername);
            return View();
        }      
    }
}