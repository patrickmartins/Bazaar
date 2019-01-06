using PM.Bazaar.Application.ApplicationServices.Common;
using PM.Bazaar.Application.Extensions;
using PM.Bazaar.Application.ViewModels;
using System.Collections.Generic;
using PM.Bazaar.Application.Interfaces;
using PM.Bazaar.Domain.Interfaces.Services;
using PM.Bazaar.Domain.Interfaces.UnitOfWork;

namespace PM.Bazaar.Application.ApplicationServices
{
    public class CategoryApplicationService : ApplicationService, ICategoryApplicationService
    {
        private readonly ICategoryService _service;

        public CategoryApplicationService(ICategoryService service, IUoW uow) : base(uow)
        {
            _service = service;
        }

        public IEnumerable<CategoryViewModel> All()
        {
            return _service.All().MapEntityTo<CategoryViewModel>();
        }
    }
}
