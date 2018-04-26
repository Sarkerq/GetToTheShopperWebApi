using GetToTheShopper.WebApi.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace GetToTheShopper.WebApi.Repositories.Implementations
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
