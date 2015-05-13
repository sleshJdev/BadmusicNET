using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace MvcSimpleMusicSite_CourseProject.Models
{ 
    public class LikeRepository : ILikeRepository
    {
        private MusicContext context;

		public LikeRepository(MusicContext context)
		{
			this.context = context;
		}

        public IQueryable<Like> All
        {
            get { return context.Like; }
        }

        public IQueryable<Like> AllIncluding(params Expression<Func<Like, object>>[] includeProperties)
        {
            IQueryable<Like> query = context.Like;
            foreach (var includeProperty in includeProperties) {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public Like Find(int id)
        {
            return context.Like.Find(id);
        }

        public void InsertOrUpdate(Like like)
        {
            if (like.Id == default(int)) {
                // New entity
                context.Like.Add(like);
            } else {
                // Existing entity
                context.Entry(like).State = EntityState.Modified;
            }
        }

        public void Delete(int id)
        {
            var like = context.Like.Find(id);
            context.Like.Remove(like);
        }

        public void Save()
        {
            context.SaveChanges();
        }
    }

    public interface ILikeRepository
    {
        IQueryable<Like> All { get; }
        IQueryable<Like> AllIncluding(params Expression<Func<Like, object>>[] includeProperties);
        Like Find(int id);
        void InsertOrUpdate(Like like);
        void Delete(int id);
        void Save();
    }
}