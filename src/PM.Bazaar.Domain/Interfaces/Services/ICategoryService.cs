using System.Collections.Generic;
using PM.Bazaar.Domain.Entities;

namespace PM.Bazaar.Domain.Interfaces.Services
{
    public interface ICategoryService
    {
        IEnumerable<Category> All();
    }
}
