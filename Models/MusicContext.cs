using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace MvcSimpleMusicSite_CourseProject.Models
{
    public class MusicContext : DbContext
    {
        public DbSet<Track> Track { get; set; }

        public DbSet<Artist> Artist { get; set; }

        public DbSet<Genre> Genre { get; set; }

        public DbSet<Album> Album { get; set; }

        public DbSet<Like> Like { get; set; }        

        public DbSet<Dowload> Dowload { get; set; }    

        public DbSet<Listened> Listened { get; set; }

    }

    public class MusicContextInizializer : DropCreateDatabaseIfModelChanges<MusicContext>
    {
        protected override void Seed(MusicContext context)
        {
            //var genres = new List<Genre>
            //{
            //    new Genre{Id = 1, Title = "Rock", Description = "Some description"},
            //    new Genre{Id = 2, Title = "Blus", Description = "Some description"}
            //};
            //foreach (var item in genres)
            //    context.Genre.Add(item);
            //context.SaveChanges();

            //var artists = new List<Artist>
            //{
            //    new Artist{Id = 1, Title = "AC/DC"},
            //    new Artist{Id = 2, Title = "Avril Lavigne"}
            //};
            //foreach (var item in artists)
            //    context.Artist.Add(item);
            //context.SaveChanges();

            //var albums = new List<Album>
            //{
            //    new Album{Id = 1, Title = "Album 1", PhotoUrl = "url", Year = "2001", Artist = artists.Single( a => a.Title == "AC/DC")},
            //    new Album{Id = 2, Title = "Album 2", PhotoUrl = "url", Year = "2002", Artist = artists.Single( a => a.Title == "Avril Lavigne")}
            //};
            //foreach (var item in albums)
            //    context.Album.Add(item);
            //context.SaveChanges();

            //var tracks = new List<Track>
            //{
            //    new Track{Id = 1, Title = "Black in bl ack", CreateAt = DateTime.Parse("20013-04-10"), PeridPlaysCount = 0, TotalPlaysCount = 0, Artist = artists.Single( a => a.Title == "AC/DC")},
            //    new Track{Id = 2, Title = "Some title",CreateAt = DateTime.Parse("20013-04-10"), PeridPlaysCount = 0, TotalPlaysCount = 0, Artist = artists.Single( a => a.Title == "Avril Lavigne")}
            //};
            //foreach (var item in tracks)
            //    context.Track.Add(item);
            //context.SaveChanges();

            ////var users = new List<User>
            ////{
            ////    new User{Id = 1, Title = "Вася"},
            ////    new User{Id = 2, Title = "Катя"}
            ////};
            ////foreach (var item in users)
            ////    context.User.Add(item);
            ////context.SaveChanges();

            //var likes = new List<Like>
            //{
            //    new Like{Id = 1, TrackId = 1, UserId = "2"},
            //    new Like{Id = 2, TrackId = 2, UserId = "1"}
            //};
            //foreach (var item in likes)
            //    context.Like.Add(item);
            //context.SaveChanges();

            //var dwnls = new List<Dowload>
            //{
            //    new Dowload{Id = 1, TrackId = 1, UserId = "1"},
            //    new Dowload{Id = 2, TrackId = 2, UserId = "2"}
            //};
            //foreach (var item in dwnls)
            //    context.Dowload.Add(item);
            //context.SaveChanges();

            //var listened = new List<Listened>
            //{
            //    new Listened{Id = 1, TrackId = 2, UserId = "1"},
            //    new Listened{Id = 2, TrackId = 2, UserId = "2"}
            //};
            //foreach (var item in listened)
            //    context.Listened.Add(item);
            //context.SaveChanges();

            base.Seed(context);
        }
    }
}