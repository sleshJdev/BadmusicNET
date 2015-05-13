using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace MvcSimpleMusicSite_CourseProject.Models
{ 
    public class GenreRepository : IGenreRepository
    {
        private MusicContext context;

		public GenreRepository(MusicContext context)
		{
			this.context = context;
		}

        public IQueryable<Genre> All
        {
            get { return context.Genre; }
        }

        public IQueryable<Genre> AllIncluding(params Expression<Func<Genre, object>>[] includeProperties)
        {
            IQueryable<Genre> query = context.Genre;
            foreach (var includeProperty in includeProperties) {
                query = query.Include(includeProperty);
            }
            return query;
        }
        
        public Genre Find(int id)
        {
            Genre result = context.Genre.Where( g => g.Id == id).Include("Tracks").First();

            foreach (var item in result.Tracks)
            {
                item.Album = context.Album.Find(item.AlbumId);
                item.Album.Artist = context.Artist.Find(item.Album.ArtistId);
            }   

            return result;
        }

        public void InsertOrUpdate(string Title)
        {
            if (null == context.Genre.SingleOrDefault( g => g.Title == Title))
            {
                Genre genre = new Genre
                {
                    Title = Title,

                    Description = "Описание жанра"
                };

                context.Genre.Add(genre);

                Save();
            }
        }         

        public void Delete(int id)
        {
            var genre = context.Genre.Find(id);
            context.Genre.Remove(genre);
        }

        public void Save()
        {
            context.SaveChanges();
        }
    }

    public interface IGenreRepository
    {
        IQueryable<Genre> All { get; }
        IQueryable<Genre> AllIncluding(params Expression<Func<Genre, object>>[] includeProperties);
        Genre Find(int id);
        void InsertOrUpdate(string Title);       
        void Delete(int id);
        void Save();
    }
}