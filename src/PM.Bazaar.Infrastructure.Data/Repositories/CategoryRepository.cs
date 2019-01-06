using PM.Bazaar.Domain.Entities;
using PM.Bazaar.Domain.Interfaces.Repositories;
using PM.Bazaar.Infrastructure.Data.Contexts;
using PM.Bazaar.Infrastructure.Data.Repositories.Common;

namespace PM.Bazaar.Infrastructure.Data.Repositories
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        public CategoryRepository(BazaarContext context) : base(context)
        { }
    }
}
