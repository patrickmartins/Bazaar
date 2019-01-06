using PM.Bazaar.Domain.Enuns;
using PM.Bazaar.Domain.Interfaces.Entity;
using PM.Bazaar.Domain.Interfaces.Repositories.Common;
using PM.Bazaar.Domain.Interfaces.Result;
using PM.Bazaar.Domain.Interfaces.Specification;
using PM.Bazaar.Domain.Values;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace PM.Bazaar.Domain.Services.Common
{
    public abstract class Service<TEntity> where TEntity : Entity
    {
        protected readonly IRepository<TEntity> Repository;
        private bool _disposed;

        public Service(IRepository<TEntity> repository)
        {
            Repository = repository;
        }

        public IEnumerable<TEntity> Search<TKey>(ISpecificationQuery<TEntity> specification, int page, int pageSize, OrderType order, Expression<Func<TEntity, TKey>> orderBy)
        {
            var query = order == OrderType.Ascending
                ? Repository.Set().OrderBy(orderBy)
                : Repository.Set().OrderByDescending(orderBy);

            return query.Where(specification.GetExpression()).ToList();
        }

        public IEnumerable<TEntity> All()
        {
            return Repository.Set().ToList();
        }

        public IEnumerable<TEntity> All(int page, int pageSize)
        {
            return Repository.Set()
                .Skip(pageSize * (page - 1))
                .Take(pageSize)
                .ToList();
        }

        public IResult<TEntity> GetById(int id)
        {
            var result = new Result<TEntity>();
            var entitie = Repository.GetById(id);

            if (entitie != null)
                result.Value = entitie;
            else
                result.Errors.Add(new Error("O item informado não foi encontrado", "Id"));

            return result;
        }

        protected virtual IResult Insert(TEntity item)
        {
            var result = item.IsValid();

            if (result.Sucess)
                Repository.Insert(item);

            return result;
        }

        protected virtual IResult Remove(TEntity item)
        {
            var result = GetById(item.Id);

            if (result.Sucess)
                Repository.Remove(result.Value);

            return result;
        }

        protected virtual IResult Update(TEntity item)
        {
            var result = GetById(item.Id);

            if (result.Sucess)
            {
                var resultValidate = item.IsValid();

                if (!resultValidate.Sucess)
                    return resultValidate;

                Repository.Update(item);
            }

            return result;
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
                    Repository.Dispose();
                }
            }

            _disposed = true;
        }
    }
}
