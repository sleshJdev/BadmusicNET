using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Text;
using MvcSimpleMusicSite_CourseProject.Models;
using System.Web.Security;

namespace MvcSimpleMusicSite_CourseProject.Areas.Admin.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class AdminController : Controller
    {
        private readonly IGenreRepository genreRepository;
        private readonly IArtistRepository artistRepository;
        private readonly ITrackRepository trackRepository;
        private readonly IAlbumRepository albumRepository;
        private readonly IDowloadRepository dowloadRepository;
        private readonly ILikeRepository likeRepository;
        private readonly IListenedRepository listenedRepository;

        const double UnoMb = 1048576;

        public AdminController(IGenreRepository genreRepository,
            IArtistRepository artistRepository,
            ITrackRepository trackRepository,
            IAlbumRepository albumRepository,
            IDowloadRepository dowloadRepository,
            ILikeRepository likeRepository,
            IListenedRepository listenedRepository)
        {
            this.genreRepository = genreRepository;
            this.artistRepository = artistRepository;
            this.trackRepository = trackRepository;
            this.albumRepository = albumRepository;
            this.dowloadRepository = dowloadRepository;
            this.likeRepository = likeRepository;
            this.listenedRepository = listenedRepository;
        }

        public ViewResult Index()
        {
            return View();
        }

        public ViewResult GetAllTracks()
        {
            var w = dowloadRepository.All;

            ViewBag.Users = dowloadRepository.All;
            return View(trackRepository.AllIncluding(
                track => track.Genre,
                track => track.Album.Artist,
                track => track.Likes,
                track => track.Dowloads,
                track => track.Listened)
                );
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(HttpPostedFileBase fileUpload)
        {
            if (null == fileUpload)
                return View();

            string filePath = Path.Combine(Server.MapPath("~/Content/UploadingMusic"), Path.GetFileName(fileUpload.FileName));

            fileUpload.SaveAs(filePath);

            TagLib.File tl = TagLib.File.Create(filePath);

            string artist = tl.Tag.FirstArtist == "" || tl.Tag.FirstArtist == null ? "Unknow" : tl.Tag.FirstArtist;

            string title = tl.Tag.Title == "" || tl.Tag.Title == null ? "Unknow" : tl.Tag.Title;

            string album = tl.Tag.Album == "" || tl.Tag.Album == null ? "Unknow" : tl.Tag.Album;

            string genre = tl.Tag.FirstGenre == "" || tl.Tag.FirstGenre == null ? "Unknow" : tl.Tag.FirstGenre;

            artistRepository.InsertOrUpdate(artist);

            albumRepository.InsertOrUpdate(album, artist, tl.Tag.Year.ToString());

            genreRepository.InsertOrUpdate(genre);

            trackRepository.InsertOrUpdate(title,
                artist,
                genre,
                album,
                Path.GetFileName(fileUpload.FileName),
                tl.Properties.Codecs.First().Duration.ToString(),
                tl.Properties.AudioBitrate.ToString(),
                ((double)(fileUpload.ContentLength / UnoMb)).ToString());

            dowloadRepository.InsertOrUpdate(title);

            return View();
        }

        public ActionResult GetAllUsers()
        {
            ViewBag.Users = Membership.GetAllUsers();

            return View();
        }

        public ViewResult CreateUser()
        {
            return View();
        }

        [HttpPost]
        public ViewResult CreateUser(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                MembershipCreateStatus createStatus;

                Membership.CreateUser(model.UserName, model.Password, model.Email, null, null, true, null, out createStatus);

                if (createStatus == MembershipCreateStatus.Success)
                {
                    Roles.AddUserToRole(model.UserName, "User");                    
                    
                    return View();
                }
            }
            return View(model);
        }

        [HttpGet]
        public ActionResult DeleteUser(string userName)
        {
            MembershipUser userForRemove = Membership.GetUser(userName);            

            string userRole = Roles.GetRolesForUser(userName)[0];

            Roles.RemoveUserFromRole(userName, userRole);

            Membership.DeleteUser(userName);

            return RedirectToAction("GetAllUsers");
        }


        public ActionResult Edit(int id)
        {
            ViewBag.PossibleGenre = genreRepository.All;
            ViewBag.PossibleAlbum = albumRepository.All;
            ViewBag.PossibleArtist = artistRepository.All;
            return View(trackRepository.Find(id));
        }

        [HttpPost]
        public ActionResult Edit(Track track)
        {

            if (ModelState.IsValid)
            {   
                trackRepository.InsertOrUpdate(track);

                trackRepository.Save();

                return View();
            }
            else
            {   
                ViewBag.PossibleGenre = genreRepository.All;

                ViewBag.PossibleAlbum = albumRepository.All;  
                
                return View();
            }
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            trackRepository.Delete(id);
            trackRepository.Save();

            return RedirectToAction("Delete");
        }

    }
}

