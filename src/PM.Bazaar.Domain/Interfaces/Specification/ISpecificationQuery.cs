using System;
using System.Linq.Expressions;

namespace PM.Bazaar.Domain.Interfaces.Specification
{
    public interface ISpecificationQuery<TEntity> where TEntity : Entity.Entity
    {
        Expression<Func<TEntity, bool>> GetExpression();
        ISpecificationQuery<TEntity> And(ISpecificationQuery<TEntity> specification);
        ISpecificationQuery<TEntity> Or(ISpecificationQuery<TEntity> specification);
    }
}
