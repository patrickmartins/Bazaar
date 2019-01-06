using System;
using System.Linq;
using System.Linq.Expressions;

namespace PM.Bazaar.Domain.Interfaces.Repositories.Common
{
    public interface IRepository<TEntity> : IDisposable where TEntity : Entity.Entity
    {
        IQueryable<TEntity> Set();
        IQueryable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);
        void Insert(TEntity item);
        TEntity Remove(TEntity item);
        void Update(TEntity item);
        bool Contains(TEntity item);
        TEntity GetById(int id);
        TEntity SingleOrDefault(Expression<Func<TEntity, bool>> predicate);
        TEntity SingleOrDefault();
        TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate);
        TEntity FirstOrDefault();
        IQueryable<TEntity> Take(int count);
        IQueryable<TEntity> Skip(int count);
        IQueryable<TEntity> OrderBy<TType>(Expression<Func<TEntity, TType>> predicate);
        IQueryable<TEntity> OrderByDescending<TType>(Expression<Func<TEntity, TType>> predicate);
        int Count();
        void ExecuteCommand(string query);
        void ExecuteCommand(string query, params object[] parameters);
    }
}
