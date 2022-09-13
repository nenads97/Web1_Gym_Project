using PR122_2016_Web_projekat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Windows;

namespace PR122_2016_Web_projekat.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult IndexLogin()
        {
            Session["users"] = HttpContext.Application["users"];
            return View();
        }
        [HttpPost]
        public ActionResult Login(string Username, string Password)
        {
            Users users = (Users)Session["users"];
      
            if (users.LoggedUserChrck(Username, Password))
            {
                if (users.RoleCheck(Username) == "VISITOR")
                {
                    ViewBag.user_name = Username;
                    //ViewBag.FitnessCenters = users.fitnessCenters.Values.ToList();
                    ViewBag.FitnessCenters = users.FitnesCenterSorting("", "");
                    return View("UserLogin");
                }
                else if (users.RoleCheck(Username) == "TRAINER")
                {
                    ViewBag.user_name = Username; ;
                    //ViewBag.FitnessCenters = users.fitnessCenters.Values.ToList();
                    ViewBag.FitnessCenters = users.FitnesCenterSorting("", "");
                    return View("TrainerLogin");
                }
                else
                {
                    ViewBag.user_name = Username;
                    //ViewBag.FitnessCenters = users.fitnessCenters.Values.ToList();
                    ViewBag.FitnessCenters = users.FitnesCenterSorting("", "");
                    return View("AdminLogin");
                }
            }
            else
            {
                System.Windows.MessageBox.Show("Wrong username or password! ","ERROR", MessageBoxButton.OK);
                return View("IndexLogin");
            }
            
        }
        public ActionResult UserLogin(string Username,string Option,string typeOfSorting,string sortBy,string Name,string Adress,string LowerLimit,string UpperLimit)
        {
            Users users = (Users)Session["users"];         
            if (Option == "Sort")
            {
               ViewBag.FitnessCenters = users.FitnesCenterSorting(typeOfSorting,sortBy);
            }
            else if(Option == "Find")
            {
                int number = 0;
                if (LowerLimit == "" && UpperLimit == "")
                {
                    if (Name == "" && Adress =="")
                    {
                        ViewBag.FitnessCenters = users.FitnesCenterSorting("","");
                    }
                    else
                    {
                        ViewBag.FitnessCenters = users.FitnessCenterSearch(Name, Adress, LowerLimit, UpperLimit);
                    }
                    
                }
                else if(LowerLimit != "" && UpperLimit == "")
                {
                    if (Int32.TryParse(LowerLimit, out number))
                    {
                        ViewBag.FitnessCenters = users.FitnessCenterSearch(Name, Adress, LowerLimit, UpperLimit);
                    }
                    else
                    {
                        ViewBag.FitnessCenters = users.FitnesCenterSorting("", "");
                        MessageBox.Show("You must enter number for LowerLimit");
                    }
                }
                else if (LowerLimit == "" && UpperLimit != "")
                {
                    if (Int32.TryParse(UpperLimit, out number))
                    {
                        ViewBag.FitnessCenters = users.FitnessCenterSearch(Name, Adress, LowerLimit, UpperLimit);
                    }
                    else
                    {
                        ViewBag.FitnessCenters = users.FitnesCenterSorting("", "");
                        MessageBox.Show("You must enter number for UpperLimit");
                    }
                }
                else if(LowerLimit != "" && UpperLimit != "")
                {
                    if (Int32.TryParse(LowerLimit, out number) && Int32.TryParse(UpperLimit, out number))
                    {
                        ViewBag.FitnessCenters = users.FitnessCenterSearch(Name, Adress, LowerLimit, UpperLimit);
                    }
                    else
                    {
                        ViewBag.FitnessCenters = users.FitnesCenterSorting("", "");
                        MessageBox.Show("Must enter a number.");
                    }
                }
                
                
            }
            else
            {
                ViewBag.FitnessCenters = users.FitnesCenterSorting("", "");
            }
            ViewBag.user_name = Username;
            //ViewBag.FitnessCenters = users.SortirajPoNazivuFC("Rastuci");          
            return View();
        }
        public ActionResult TrainerLogin(string Username, string Option, string typeOfSorting, string sortBy, string Name, string Adress, string LowerLimit, string UpperLimit)
        {           
            Users users = (Users)Session["users"];
            if (!users.UserNameCheck(Username))
            {
                return RedirectToAction("PocetnaNeprijavljen","Neprijavljen");
            }
            if (Option == "Sort")
            {
                ViewBag.FitnessCenters = users.FitnesCenterSorting(typeOfSorting, sortBy);
            }
            else if (Option == "Find")
            {
                int number = 0;
                if (LowerLimit == "" && UpperLimit == "")
                {
                    if (Name == "" && Adress == "")
                    {
                        ViewBag.FitnessCenters = users.FitnesCenterSorting("", "");
                    }
                    else
                    {
                        ViewBag.FitnessCenters = users.FitnessCenterSearch(Name, Adress, LowerLimit, UpperLimit);
                    }

                }
                else if (LowerLimit != "" && UpperLimit == "")
                {
                    if (Int32.TryParse(LowerLimit, out number))
                    {
                        ViewBag.FitnessCenters = users.FitnessCenterSearch(Name, Adress, LowerLimit, UpperLimit);
                    }
                    else
                    {
                        ViewBag.FitnessCenters = users.FitnesCenterSorting("", "");
                        MessageBox.Show("Morate uneti broj za DonjuGranicu");
                    }
                }
                else if (LowerLimit == "" && UpperLimit != "")
                {
                    if (Int32.TryParse(UpperLimit, out number))
                    {
                        ViewBag.FitnessCenters = users.FitnessCenterSearch(Name, Adress, LowerLimit, UpperLimit);
                    }
                    else
                    {
                        ViewBag.FitnessCenters = users.FitnesCenterSorting("", "");
                        MessageBox.Show("Morate uneti broj za GornjuGranicu");
                    }
                }
                else if (LowerLimit != "" && UpperLimit != "")
                {
                    if (Int32.TryParse(LowerLimit, out number) && Int32.TryParse(UpperLimit, out number) && LowerLimit != "")
                    {
                        ViewBag.FitnessCenters = users.FitnessCenterSearch(Name, Adress, LowerLimit, UpperLimit);
                    }
                    else
                    {
                        ViewBag.FitnessCenters = users.FitnesCenterSorting("", "");
                        MessageBox.Show("Morate uneti broj");
                    }
                }


            }
            else
            {
                ViewBag.FitnessCenters = users.FitnesCenterSorting("", "");
            }
            ViewBag.user_name = Username;
           // ViewBag.FitnessCenters = users.SortirajPoNazivuFC("Rastuci");
            return View();
        }
        public ActionResult AdminLogin(string Username, string Option, string typeOfSorting, string sortBy, string Name, string Adress, string LowerLimit, string UpperLimit)
        {
            Users users = (Users)Session["users"];
            
            if (Option == "Sort")
            {
                ViewBag.FitnessCenters = users.FitnesCenterSorting(typeOfSorting, sortBy);
               

            }
            else if (Option == "Find")
            {
                int number = 0;
                if (LowerLimit == "" && UpperLimit == "")
                {
                    if (Name == "" && Adress == "")
                    {
                        ViewBag.FitnessCenters = users.FitnesCenterSorting("", "");
                    }
                    else
                    {
                        ViewBag.FitnessCenters = users.FitnessCenterSearch(Name, Adress, LowerLimit, UpperLimit);
                    }

                }
                else if (LowerLimit != "" && UpperLimit == "")
                {
                    if (Int32.TryParse(LowerLimit, out number))
                    {
                        ViewBag.FitnessCenters = users.FitnessCenterSearch(Name, Adress, LowerLimit, UpperLimit);
                    }
                    else
                    {
                        ViewBag.FitnessCenters = users.FitnesCenterSorting("", "");
                        MessageBox.Show("Morate uneti broj za DonjuGranicu");
                    }
                }
                else if (LowerLimit == "" && UpperLimit != "")
                {
                    if (Int32.TryParse(UpperLimit, out number))
                    {
                        ViewBag.FitnessCenters = users.FitnessCenterSearch(Name, Adress, LowerLimit, UpperLimit);
                    }
                    else
                    {
                        ViewBag.FitnessCenters = users.FitnesCenterSorting("", "");
                        MessageBox.Show("Morate uneti broj za GornjuGranicu");
                    }
                }
                else if (LowerLimit != "" && UpperLimit != "")
                {
                    if (Int32.TryParse(LowerLimit, out number) && Int32.TryParse(UpperLimit, out number) && LowerLimit != "")
                    {
                        ViewBag.FitnessCenters = users.FitnessCenterSearch(Name, Adress, LowerLimit, UpperLimit);
                    }
                    else
                    {
                        ViewBag.FitnessCenters = users.FitnesCenterSorting("", "");
                        MessageBox.Show("Morate uneti broj");
                    }
                }
            }
            else
            {
                ViewBag.FitnessCenters = users.FitnesCenterSorting("", "");
            }
            ViewBag.user_name = Username;
           // ViewBag.FitnessCenters = users.SortirajPoNazivuFC("Rastuci");
            return View();
        }
    }
}