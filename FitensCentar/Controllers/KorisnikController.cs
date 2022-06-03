using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FitensCentar.Models;

namespace FitensCentar.Controllers
{
    public class KorisnikController : Controller
    {
        // GET: Korisnik
        public ActionResult Korisnik()
        {
            var korisnik = new Korisnik()
            {
                username = "Nenad", datumRodjenja = DateTime.Now, password = "aezakmi",
                emailKorisnika = "nenad.suknovic@gmail.com", imeKorisnika = "Nenad", pol = 0,
                prezimeKorisnika = "Suknovic", uloga = 0

            };
                return View(korisnik);
        }
    }
}