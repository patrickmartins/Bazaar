using Microsoft.Owin;
using PM.Bazaar.Application.ApplicationServices;
using PM.Bazaar.Domain.Services;
using PM.Bazaar.Infrastructure.CrossCutting.Identity.Services;
using PM.Bazaar.Infrastructure.Data.Contexts;
using PM.Bazaar.Infrastructure.Data.Repositories;
using PM.Bazaar.Infrastructure.Data.Repositories.Common;
using PM.Bazaar.Infrastructure.Data.UnitOfWork;
using SimpleInjector;
using System.Web;
using PM.Bazaar.Application.Interfaces;
using PM.Bazaar.Domain.Interfaces.Repositories;
using PM.Bazaar.Domain.Interfaces.Repositories.Common;
using PM.Bazaar.Domain.Interfaces.Services;
using PM.Bazaar.Domain.Interfaces.UnitOfWork;
using PM.Bazaar.Domain.Services.Common;
using PM.Bazaar.Infrastructure.CrossCutting.Identity.Interfaces.Repositories;
using PM.Bazaar.Infrastructure.CrossCutting.Identity.Interfaces.Services;

namespace PM.Bazaar.Infrastructure.CrossCutting.IoC
{
    public class BazaarContainer : Container
    {
        public BazaarContainer(ScopedLifestyle lifestyle)
        {
            Options.DefaultScopedLifestyle = lifestyle;

            Initialize();
        }

        private void Initialize()
        {
            Register<BazaarContext>(Lifestyle.Scoped);

            Register<IUoW, UoW>(Lifestyle.Scoped);

            #region Repositories

            Register(typeof(IRepository<>), typeof(Repository<>), Lifestyle.Scoped);
            Register<IAccountRepository, AccountRepository>(Lifestyle.Scoped);
            Register<IAdRepository, AdRepository>(Lifestyle.Scoped);
            Register<ICategoryRepository, CategoryRepository>(Lifestyle.Scoped);

            #endregion

            #region Domain Services

            Register<IAdvertisingService, AdvertisingService>(Lifestyle.Scoped);
            Register<IImageService, ImageService>(Lifestyle.Scoped);
            Register<ICategoryService, CategoryService>(Lifestyle.Scoped);


            #endregion

            #region Application Services

            Register<IAdvertisingApplicationService, AdvertisingApplicationService>(Lifestyle.Scoped);
            Register<IAccountApplicationService, AccountApplicationService>(Lifestyle.Scoped);
            Register<IImageApplicationService, ImageApplicationService>(Lifestyle.Scoped);
            Register<ICategoryApplicationService, CategoryApplicationService>(Lifestyle.Scoped);
            Register<IAuthCookieService, AuthCookieApplicationService>(Lifestyle.Scoped);

            #endregion

            #region Identity Services

            Register<IAccountService, AccountService>(Lifestyle.Scoped);
            Register<IAuthService, AuthService>(Lifestyle.Scoped);

            Register(() =>
            {
                if (HttpContext.Current != null && HttpContext.Current.Items["owin.Environment"] == null && IsVerifying)
                {
                    return new OwinContext().Authentication;
                }
                return HttpContext.Current.GetOwinContext().Authentication;
            });

            #endregion
        }
    }
}
