using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Security;

namespace MvcSimpleMusicSite_CourseProject.Models
{
    public class DowloadRepository : IDowloadRepository
    {
        private MusicContext context;

        public DowloadRepository(MusicContext context)
        {
            this.context = context;
        }

        public IQueryable<Dowload> All
        {
            get { return context.Dowload; }
        }

        public IQueryable<Dowload> AllIncluding(params Expression<Func<Dowload, object>>[] includeProperties)
        {
            IQueryable<Dowload> query = context.Dowload;
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public Dowload Find(int id)
        {
            return context.Dowload.Find(id);
        }

        public void InsertOrUpdate(string trackTitle)
        {
            Track track = new Track();

            track = context.Track.SingleOrDefault(t => t.Title == trackTitle);

            Dowload dwld = new Dowload
            {
                UserName = Membership.GetUser().UserName,

                TrackId = track.Id
            };
            

            InsertOrUpdate(dwld);

        }

        public void InsertOrUpdate(Dowload dowload)
        {
            if (dowload.Id == default(int))
            {
                // New entity
                context.Dowload.Add(dowload);
            }
            else
            {
                // Existing entity
                context.Entry(dowload).State = EntityState.Modified;
            }

            Save();
        }

        public void Delete(int id)
        {
            var dowload = context.Dowload.Find(id);
            context.Dowload.Remove(dowload);
        }

        public void Save()
        {
            context.SaveChanges();
        }
    }

    public interface IDowloadRepository
    {
        IQueryable<Dowload> All { get; }
        IQueryable<Dowload> AllIncluding(params Expression<Func<Dowload, object>>[] includeProperties);
        Dowload Find(int id);
        void InsertOrUpdate(Dowload dowload);
        void InsertOrUpdate(string trackTitle);
        void Delete(int id);
        void Save();
    }
}