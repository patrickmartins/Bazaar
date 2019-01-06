using PM.Bazaar.Domain.Interfaces.Entity;
using PM.Bazaar.Domain.Interfaces.Specification;

namespace PM.Bazaar.Domain.Specification.Specifications.Common
{
    public class OrSpecification<TEntity> : SpecificationQuery<TEntity> where TEntity : Entity
    {
        public OrSpecification(ISpecificationQuery<TEntity> left, ISpecificationQuery<TEntity> right)
        {
            Expression = MergeExpression(left.GetExpression(), right.GetExpression(), System.Linq.Expressions.Expression.Or);
        }
    }
}
