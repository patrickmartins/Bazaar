using PM.Bazaar.Domain.Entities;
using PM.Bazaar.Domain.Interfaces.Repositories.Common;
using PM.Bazaar.Domain.Interfaces.Services;
using PM.Bazaar.Domain.Services.Common;

namespace PM.Bazaar.Domain.Services
{
    public class CategoryService: Service<Category>, ICategoryService
    {
        public CategoryService(IRepository<Category> repository) : base(repository)
        { }
    }
}
