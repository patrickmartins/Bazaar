using PM.Bazaar.Domain.Interfaces.Entity;
using PM.Bazaar.Domain.Interfaces.Specification;

namespace PM.Bazaar.Domain.Specification.Specifications.Common
{
    public class AndSpecification<TEntity> : SpecificationQuery<TEntity> where TEntity : Entity
    {
        public AndSpecification(ISpecificationQuery<TEntity> left, ISpecificationQuery<TEntity> right)
        {
            Expression = MergeExpression(left.GetExpression(), right.GetExpression(), System.Linq.Expressions.Expression.And);
        }
    }
}
