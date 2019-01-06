using System.Linq;

namespace PM.Bazaar.Domain.Specification.Specifications.Ad
{
    public class WithCategory : SpecificationQuery<Entities.Ad>
    {
        public WithCategory(int[] idCategories)
        {
            Expression = c => idCategories.Contains(c.IdCategory);
        }
    }
}
