using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Data.Entity.Migrations;
using System.Data.Entity;
using MvcSimpleMusicSite_CourseProject.Models;
using MvcSimpleMusicSite_CourseProject.Migrations;
using System.Globalization;
using System.Threading;

namespace MvcSimpleMusicSite_CourseProject
{

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_AcquireRequestState(object sender, EventArgs e)
        {

            if (HttpContext.Current.Session != null)
            {
                CultureInfo ci = (CultureInfo)this.Session["Culture"];

                if (ci == null)
                {
                    string langName = "ru";

                    if (HttpContext.Current.Request.UserLanguages != null && HttpContext.Current.Request.UserLanguages.Length != 0)
                    {
                        langName = HttpContext.Current.Request.UserLanguages[0].Substring(0, 2);
                    }
                    ci = new CultureInfo(langName);

                    this.Session["Culture"] = ci;
                }
                Thread.CurrentThread.CurrentUICulture = ci;

                Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(ci.Name);
            }
        }

        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                        "Default",
                        "{controller}/{action}/{id}",
                        new { controller = "Site", action = "Index", id = UrlParameter.Optional }

                    );
        }

        protected void Application_Start()
        {

            DbMigrator migrator = new DbMigrator(new Configuration());
            migrator.Update();

            Bootstrapper.Initialise();           

            AreaRegistration.RegisterAllAreas();

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);

        }
    }
}