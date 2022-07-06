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
                Username = "Nenad", DatumRodjenja = "10.01.1997.", Password = "aezakmi",
                EmailKorisnika = "nenad.suknovic@gmail.com", ImeKorisnika = "Nenad", _Pol = 0,
                PrezimeKorisnika = "Suknovic", _Uloga = 0

            };
                return View(korisnik);
        }
    }
}