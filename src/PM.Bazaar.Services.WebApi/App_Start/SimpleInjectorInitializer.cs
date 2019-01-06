using System.Web.Http;
using Owin;
using PM.Bazaar.Infrastructure.CrossCutting.IoC;
using SimpleInjector.Integration.WebApi;
using SimpleInjector.Lifestyles;

namespace PM.Bazaar.Services.WebApi
{
    public static class SimpleInjectorInitializer
    {
        public static void Initialize(IAppBuilder app)
        {
            var container = new BazaarContainer(new AsyncScopedLifestyle());

            app.Use(async (context, next) => {
                using (AsyncScopedLifestyle.BeginScope(container))
                {
                    await next();
                }
            });
            
            GlobalConfiguration.Configuration.DependencyResolver =
                new SimpleInjectorWebApiDependencyResolver(container);
        }
    }
}