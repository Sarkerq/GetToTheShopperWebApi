using ShoppingList.Core.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingList.Core.Repositories.Implementations
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly DbContext Context;
        private readonly DbSet<TEntity> entities;

        public Repository(DbContext context)
        {
            Context = context;
            entities = Context.Set<TEntity>();
        }


        public IQueryable<TEntity> Include(Expression<Func<TEntity, dynamic>> predicate)
        {
            return entities.Include(predicate);
        }

        public TEntity Find(int id)
        {
            return entities.Find(id);
        }

        public IEnumerable<TEntity> GetAll()
        {
            // Note that here I've repeated Context.Set<TEntity>() in every method and this is causing
            // too much noise. I could get a reference to the DbSet returned from this method in the 
            // constructor and store it in a private field like _entities. This way, the implementation
            // of our methods would be cleaner:
            // 
            // _entities.ToList();
            // _entities.Where();
            // _entities.SingleOrDefault();
            // 
            // I didn't change it because I wanted the code to look like the videos. But feel free to change
            // this on your own.
            return entities.ToList();
        }

        public IEnumerable<TEntity> Where(Expression<Func<TEntity, bool>> predicate)
        {
            return entities.Where(predicate);
        }

        public bool Any(Expression<Func<TEntity, bool>> predicate = null)
        {
            if (predicate != null)
                return entities.Any(predicate);
            return entities.Any();
        }

        public TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate = null)
        {
            if (predicate != null)
                return entities.FirstOrDefault(predicate);
            return entities.FirstOrDefault();
        }

        public TEntity SingleOrDefault(Expression<Func<TEntity, bool>> predicate = null)
        {
            if (predicate != null)
                return entities.SingleOrDefault(predicate);
            return entities.SingleOrDefault();
        }

        public void Add(TEntity entity)
        {
            entities.Add(entity);
        }

        public void AddRange(IEnumerable<TEntity> entities)
        {
            this.entities.AddRange(entities);
        }

        public void Remove(TEntity entity)
        {
            entities.Attach(entity);
            entities.Remove(entity);
        }

        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            this.entities.RemoveRange(entities);
        }

        public void UpdateByObject(TEntity entity)
        {
            Context.Entry(entity).State = EntityState.Modified;
        }

    }
}
