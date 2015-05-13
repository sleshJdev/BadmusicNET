using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using MvcSimpleMusicSite_CourseProject.Helpers;
 
namespace MvcSimpleMusicSite_CourseProject.Models
{
    public class TrackRepository : ITrackRepository
    {
        private const string basePath = "../Content/UploadingMusic/";
        
        private MusicContext context;

        public TrackRepository(MusicContext context)
        {
            this.context = context;
        }

        public IEnumerable<Track> All
        {
            get { return context.Track; }
        }

        public IEnumerable<Track> AllIncluding(params Expression<Func<Track, object>>[] includeProperties)
        {
            IQueryable<Track> query = context.Track;
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }

            return query;
        }

        public IEnumerable<Track> Find(string s)
        {
            var result = context.Track.Where(t => t.Title.Contains(s)).ToList();

            if (null != result && result.Count() > 0)
                foreach (var item in result)
                {
                    item.Album = context.Album.Find(item.AlbumId);

                    item.Album.Artist = context.Artist.Find(item.Album.ArtistId);

                    item.Genre = context.Genre.Find(item.GenreId);
                }

            return result;
        }

        public Track Find(int id)
        {
            Track track = new Track();

            track = context.Track.Find(id);

            track.Album = context.Album.Find(track.AlbumId);

            track.Album.Artist = context.Artist.Find(track.Album.ArtistId);

            track.Genre = context.Genre.Find(track.GenreId);

            return track;
        }

        public void InsertOrUpdate(string TitleTrack,
            string TitleArtist,
            string TitleGenre,
            string TitleAlbum,
            string Url,
            string time,
            string bitrate,
            string size)
        {
            if (null == context.Track.SingleOrDefault(a => a.Title == TitleTrack))
            {
                Track track = new Track
                {
                    Title = TitleTrack,

                    Mp3Url = basePath + Url,

                    CreateAt = DateTime.Now,

                    Size = CustomHelpers.FormattingStr(size),

                    Bitrate = bitrate,

                    Time = time,

                    Genre = context.Genre.SingleOrDefault(g => g.Title == TitleGenre),

                    Album = context.Album.SingleOrDefault(al => al.Title == TitleAlbum)
                };

                context.Track.Add(track);

                Save();
            }
        }

        public void InsertOrUpdate(Track track)
        {
            if (track.Id == default(int))
            {               
                context.Track.Add(track);
            }
            else
            {                               
                context.Entry(track).State = EntityState.Modified;

            }
        }

        public void Delete(int id)
        {
            var track = context.Track.Find(id);
            context.Track.Remove(track);
        }

        public void Save()
        {
             context.SaveChanges();
        }
    }

    public interface ITrackRepository
    {
        IEnumerable<Track> All { get; }
        IEnumerable<Track> AllIncluding(params Expression<Func<Track, object>>[] includeProperties);
        IEnumerable<Track> Find(string s);
        Track Find(int id);
        void InsertOrUpdate(string TitleTrack, string TitleArtist, string TitleGenre, string TitleAlbum, string Url, string time, string bitrate, string size);
        void InsertOrUpdate(Track track);
        void Delete(int id);
        void Save();
    }
}