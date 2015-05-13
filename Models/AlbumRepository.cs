using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace MvcSimpleMusicSite_CourseProject.Models
{
    public class AlbumRepository : IAlbumRepository
    {
        private MusicContext context;

        public AlbumRepository(MusicContext context)
        {
            this.context = context;
        }

        public IQueryable<Album> All
        {
            get { return context.Album; }
        }

        public IQueryable<Album> AllIncluding(params Expression<Func<Album, object>>[] includeProperties)
        {
            IQueryable<Album> query = context.Album;
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }
            return query;
        }
         
        public Album Find(int id)
        {
            var result = context.Album.Find(id);

            result.Artist = context.Artist.Find(result.ArtistId);

            return result;
        }

        public void InsertOrUpdate(string TitleAlbum, string TitleArtist, string Year)
        {
            if (null == context.Album.SingleOrDefault(a => a.Title == TitleAlbum))
            {
                Album album = new Album
                {
                    Title = TitleAlbum,

                    Year = Year,

                    Artist = context.Artist.SingleOrDefault(a => a.Title == TitleArtist)
                };

                context.Album.Add(album);

                Save();
            }
        }

        public void InsertOrUpdate(Album album)
        {
            if (album.Id == default(int))
            {
                // New entity
                context.Album.Add(album);
            }
            else
            {
                // Existing entity
                context.Entry(album).State = EntityState.Modified;
            }
        }

        public void Delete(int id)
        {
            var album = context.Album.Find(id);
            context.Album.Remove(album);
        }

        public void Save()
        {
            context.SaveChanges();
        }
    }

    public interface IAlbumRepository
    {
        IQueryable<Album> All { get; }
        IQueryable<Album> AllIncluding(params Expression<Func<Album, object>>[] includeProperties);      
        Album Find(int id);
        void InsertOrUpdate(string TitleAlbum, string TitleArtist, string Year);
        void InsertOrUpdate(Album album);
        void Delete(int id);
        void Save();
    }
}