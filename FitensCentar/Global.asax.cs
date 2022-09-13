using FitnesCentar.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace FitnesCentar
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            //ovde odmah da ubacim pocetne podatke
            Users users = new Users();
            users.ListOfUsers();
            users.GroupTrainingsOfVisitors();
            users.TrainerFitnessCenter();
            users.FitnessCentersOfAdmin();
            users.ListOfFitnessCenters();
            users.ListaGrupnihTreninga();
            users.GroupTrainingsOfTrainer();
            users.VisitorsOfGroupTraining();
            users.ListOfComments();
            HttpContext.Current.Application["users"] = users;
        }
    }
}
