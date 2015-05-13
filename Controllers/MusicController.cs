using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcSimpleMusicSite_CourseProject.Models;
using System.IO;
using System.Web.Security;
using MvcSimpleMusicSite_CourseProject.Helpers;

namespace MvcSimpleMusicSite_CourseProject.Controllers
{
    public class MusicController : Controller
    {
        private readonly IGenreRepository genreRepository;
        private readonly IArtistRepository artistRepository;
        private readonly ITrackRepository trackRepository;
        private readonly IAlbumRepository albumRepository;
        private readonly ILikeRepository likeRepository;
        private readonly ILikeRepository listenedRepository;
        private readonly IDowloadRepository dowloadRepository;

        const int UnoMb = 1048576;

        public MusicController(IGenreRepository genreRepository,
            IArtistRepository artistRepository,
            ITrackRepository trackRepository,
            IAlbumRepository albumRepository,
            ILikeRepository likeRepository,
            ILikeRepository listenedRepository,
            IDowloadRepository dowloadRepository)
        {
            this.genreRepository = genreRepository;
            this.artistRepository = artistRepository;
            this.trackRepository = trackRepository;
            this.albumRepository = albumRepository;
            this.likeRepository = likeRepository;
            this.listenedRepository = listenedRepository;
            this.dowloadRepository = dowloadRepository;
        }


        [HttpPost]
        public void Upload()
        {
            UploadedFile file = RetrieveFileFromRequest();

            SaveFile(file);
        }

        private UploadedFile RetrieveFileFromRequest()
        {            
            byte[] fileContents = null;

            if (Request.Files.Count > 0)
            {  
                var file = Request.Files[0];
                fileContents = new byte[file.ContentLength];
                file.InputStream.Read(fileContents, 0, file.ContentLength);                
            }
            else 
                if (Request.ContentLength > 0)
            {                
                fileContents = new byte[Request.ContentLength];
                Request.InputStream.Read(fileContents, 0, Request.ContentLength);                 
            }

            return new UploadedFile()
            {                
                FileSize = fileContents != null ? fileContents.Length : 0,
                Contents = fileContents
            };
        }

        private ActionResult SaveFile(UploadedFile file)
        {
            System.IO.FileStream stream = null;

            string filePath = null;

            string name = null;

            try
            {

                name = DateTime.Now.Ticks + ".mp3";

                filePath = Path.Combine(Server.MapPath("~/Content/UploadingMusic"), name);

                stream = new FileStream(filePath, FileMode.Create, FileAccess.Write);

                if (stream.CanWrite)
                {
                    stream.Write(file.Contents, 0, file.Contents.Length);
                }
            }
            finally
            {
                if (stream != null)
                {
                    stream.Close();
                }
            }

            WritinginDb(filePath, name);

            return RedirectToAction("Index", "Site");
        }

        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(HttpPostedFileBase[] fileUpload)
        {
            if (null == fileUpload[0] || fileUpload.Count() == 0)
            {
                return View();
            }

            foreach (var item in fileUpload)
            {
                string name = DateTime.Now.Ticks.ToString() + ".mp3";

                string filePath = Path.Combine(Server.MapPath("~/Content/UploadingMusic"), name);

                item.SaveAs(filePath);

                WritinginDb(filePath, name);

            }

            return RedirectToAction("Index", "Site");
        }

        public void WritinginDb(string filePath, string name)
        {
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
                name,
                tl.Properties.Codecs.First().Duration.ToString(),
                tl.Properties.AudioBitrate.ToString(),
                ((double)(tl.InvariantEndPosition / UnoMb)).ToString());

            dowloadRepository.InsertOrUpdate(title);
        }

        public FileResult Send_Mp3(int Id)
        {
            Track trackForSend = trackRepository.Find(Id);

            string filePath = Server.MapPath("~/" + trackForSend.Mp3Url.Remove(0, 3));

            string fileType = "audio/mp3";

            string fileName = trackForSend.Album.Artist.Title + "_" + trackForSend.Title;

            return File(filePath, fileType, fileName);
        }

        public ActionResult Search(string searchContext, int IdGenre = 0)
        {
            IEnumerable<Track> result = null;

            if (IdGenre > 0)
            {
                result = genreRepository.Find(IdGenre).Tracks;

                return View(result);
            }

            IEnumerable<Track> resultTrack = trackRepository.Find(searchContext);

            IEnumerable<Artist> resultArtist = artistRepository.Find(searchContext);

            if (null != resultArtist && resultArtist.Count() > 0)
            {
                bool flag = true;

                foreach (var item in resultArtist)
                {
                    foreach (var _item in item.Albums)
                    {
                        if (flag)
                        {
                            result = _item.Tracks;
                            flag = !flag;
                            continue;
                        }
                        result = result.Concat(_item.Tracks);
                    }
                }
            }

            if (null != result)
                result = result.Concat(resultTrack);
            else
                result = resultTrack;

            //IEnumerable<Track> _result = null;

            //foreach (var item in result)
            //{
            //    if (_result.Where(t => t.Id == item.Id) == null)
            //    {
            //        _result.Concat((IEnumerable<Track>)item);
            //    }
            //}

            return View(result);
        }
    }
}

