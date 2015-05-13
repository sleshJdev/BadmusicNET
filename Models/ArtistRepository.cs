using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace MvcSimpleMusicSite_CourseProject.Models
{
    public class ArtistRepository : IArtistRepository
    {
        private MusicContext context;

        public ArtistRepository(MusicContext context)
        {
            this.context = context;
        }

        public IEnumerable<Artist> All
        {
            get { return context.Artist; }
        }

        public IEnumerable<Artist> AllIncluding(params Expression<Func<Artist, object>>[] includeProperties)
        {
            IQueryable<Artist> query = context.Artist;
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public IEnumerable<Artist> Find(string TitleArtist)
        {
            var result = context.Artist.Where(a => a.Title.Contains(TitleArtist)).Include("Albums").ToList();

            foreach (var item in result)
            {
                foreach (var _item in item.Albums)
                {
                    _item.Tracks = context.Track.Where(t => t.AlbumId == _item.Id).ToList();
                }
            }
            return result;
        }

        public Artist Find(int id)
        {
            return context.Artist.Find(id);
        }

        public void InsertOrUpdate(string TitleArtist)
        {
            if (null == context.Artist.SingleOrDefault(a => a.Title == TitleArtist))
            {
                Artist artist = new Artist
                {
                    Title = TitleArtist
                };

                context.Artist.Add(artist);

                Save();
            }
        }

        public void InsertOrUpdate(Artist artist)
        {
            if (artist.Id == default(int))
            {                
                context.Artist.Add(artist);
            }
            else
            {               
                context.Entry(artist).State = EntityState.Modified;
            }
        }

        public void Delete(int id)
        {
            var artist = context.Artist.Find(id);
            context.Artist.Remove(artist);
        }

        public void Save()
        {
            context.SaveChanges();
        }
    }

    public interface IArtistRepository
    {
        IEnumerable<Artist> All { get; }
        IEnumerable<Artist> AllIncluding(params Expression<Func<Artist, object>>[] includeProperties);
        IEnumerable<Artist> Find(string Title);
        Artist Find(int id);
        void InsertOrUpdate(string Title);
        //void InsertOrUpdate(Artist artist);
        void Delete(int id);
        void Save();
    }
}