using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcSimpleMusicSite_CourseProject.Models;
using System.Globalization;
using System.Web.Security;

namespace MvcSimpleMusicSite_CourseProject.Controllers
{
    public class SiteController : Controller
    {
        private readonly ITrackRepository trackRepository;
        private readonly IGenreRepository genreRepository;
        private readonly IDowloadRepository downloadRepository;

        public SiteController(ITrackRepository trackRepository,
            IGenreRepository genreRepository,
            IDowloadRepository downloadRepository)
        {
            this.trackRepository = trackRepository;
            this.genreRepository = genreRepository;
            this.downloadRepository = downloadRepository;
        }
 
        public ViewResult Index()
        {
            return View(trackRepository.AllIncluding(
                track => track.Genre,
                track => track.Album.Artist,
                track => track.Likes,
                track => track.Dowloads,
                track => track.Listened)
                );
        }

        public ViewResult Details(int id)
        {
            ViewBag.User = downloadRepository.Find(id).UserName;
            return View(trackRepository.Find(id));
        }

        public ViewResult About()
        {
            return View();
        }

        public ActionResult SetCulture(string lang, string returnUrl)
        {

            Session["Culture"] = new CultureInfo(lang);

            return Redirect(returnUrl);
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult TagCloud()
        {
            var genres = genreRepository.All;
            return Json(genres, JsonRequestBehavior.AllowGet);
        }
    }
}
