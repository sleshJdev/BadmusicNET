using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace MvcSimpleMusicSite_CourseProject.Models
{ 
    public class ListenedRepository : IListenedRepository
    {
        private MusicContext context;

		public ListenedRepository(MusicContext context)
		{
			this.context = context;
		}

        public IQueryable<Listened> All
        {
            get { return context.Listened; }
        }

        public IQueryable<Listened> AllIncluding(params Expression<Func<Listened, object>>[] includeProperties)
        {
            IQueryable<Listened> query = context.Listened;
            foreach (var includeProperty in includeProperties) {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public Listened Find(int id)
        {
            return context.Listened.Find(id);
        }

        public void InsertOrUpdate(Listened listened)
        {
            if (listened.Id == default(int)) {
                // New entity
                context.Listened.Add(listened);
            } else {
                // Existing entity
                context.Entry(listened).State = EntityState.Modified;
            }
        }

        public void Delete(int id)
        {
            var listened = context.Listened.Find(id);
            context.Listened.Remove(listened);
        }

        public void Save()
        {
            context.SaveChanges();
        }
    }

    public interface IListenedRepository
    {
        IQueryable<Listened> All { get; }
        IQueryable<Listened> AllIncluding(params Expression<Func<Listened, object>>[] includeProperties);
        Listened Find(int id);
        void InsertOrUpdate(Listened listened);
        void Delete(int id);
        void Save();
    }
}