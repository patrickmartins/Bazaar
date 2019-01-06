using System.Collections.Generic;
using PM.Bazaar.Application.ViewModels;

namespace PM.Bazaar.Application.Interfaces
{
    public interface ICategoryApplicationService 
    {
        IEnumerable<CategoryViewModel> All();
    }
}
