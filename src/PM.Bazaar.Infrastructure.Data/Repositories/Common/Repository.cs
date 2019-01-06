using PM.Bazaar.Domain.Interfaces.Entity;
using PM.Bazaar.Domain.Interfaces.Repositories.Common;
using PM.Bazaar.Infrastructure.Data.Contexts;
using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace PM.Bazaar.Infrastructure.Data.Repositories.Common
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity
    {
        protected readonly BazaarContext Context;
        private bool _disposed;

        public Repository(BazaarContext context)
        {
            Context = context;
        }

        public TEntity GetById(int id)
        {
            return Context.Set<TEntity>().FirstOrDefault(c => c.Id == id);
        }

        public IQueryable<TEntity> Set()
        {
            return Context.Set<TEntity>().AsQueryable();
        }

        public IQueryable<TEntity> ByQuery(string query)
        {
            return Context.Database.SqlQuery<TEntity>(query).AsQueryable();
        }

        public IQueryable<TEntity> ByQuery(string query, params object[] parameters)
        {
            return Context.Set<TEntity>().SqlQuery(query, parameters).AsQueryable();
        }

        public bool Contains(TEntity item)
        {
            return Context.Set<TEntity>().Contains(item);
        }

        public int Count()
        {
            return Context.Set<TEntity>().Count();
        }

        public IQueryable<TEntity> OrderBy<TType>(Expression<Func<TEntity, TType>> predicate)
        {
            return Context.Set<TEntity>().OrderBy(predicate);
        }

        public IQueryable<TEntity> OrderByDescending<TType>(Expression<Func<TEntity, TType>> predicate)
        {
            return Context.Set<TEntity>().OrderByDescending(predicate);
        }

        public TEntity SingleOrDefault(Expression<Func<TEntity, bool>> predicate)
        {
            return Context.Set<TEntity>().SingleOrDefault(predicate);
        }

        public TEntity SingleOrDefault()
        {
            return Context.Set<TEntity>().SingleOrDefault();
        }

        public TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate)
        {
            return Context.Set<TEntity>().Where(predicate).FirstOrDefault();
        }

        public TEntity FirstOrDefault()
        {
            return Context.Set<TEntity>().FirstOrDefault();
        }

        public IQueryable<TEntity> Skip(int count)
        {
            return Context.Set<TEntity>().Skip(count);
        }

        public IQueryable<TEntity> Take(int count)
        {
            return Context.Set<TEntity>().Take(count);
        }

        public IQueryable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return Context.Set<TEntity>().Where(predicate);
        }

        public void Insert(TEntity item)
        {
            Context.Set<TEntity>().Add(item);
        }

        public TEntity Remove(TEntity item)
        {
            return Context.Set<TEntity>().Remove(item);
        }

        public void Update(TEntity item)
        {
            Context.Set<TEntity>().Attach(item);
            Context.Entry(item).State = EntityState.Modified;
        }

        public void ExecuteCommand(string query)
        {
            Context.Database.ExecuteSqlCommand(query);
        }

        public void ExecuteCommand(string query, params object[] parameters)
        {
            Context.Database.ExecuteSqlCommand(query, parameters);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    Context.Dispose();
                }
            }

            _disposed = true;
        }
    }
}
