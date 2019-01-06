using System.Collections.Generic;
using System.Linq.Expressions;

namespace PM.Bazaar.Domain.Specification.Util
{
    internal class ParameterRebinder : ExpressionVisitor
    {
        private readonly Dictionary<ParameterExpression, ParameterExpression> map;

        public ParameterRebinder(Dictionary<ParameterExpression, ParameterExpression> map)
        {
            this.map = map ?? new Dictionary<ParameterExpression, ParameterExpression>();
        }

        public static Expression ReplaceParameters(Dictionary<ParameterExpression, ParameterExpression> map, Expression exp)
        {
            return new ParameterRebinder(map).Visit(exp);
        }

        protected override Expression VisitParameter(ParameterExpression parameter)
        {
            if (map.TryGetValue(parameter, out var replacement))
                parameter = replacement;

            return base.VisitParameter(parameter);
        }
    }
}
