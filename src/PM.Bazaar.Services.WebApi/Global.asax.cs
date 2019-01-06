using PM.Bazaar.Application.AutoMapper;
using System.Web;
using System.Web.Http;

namespace PM.Bazaar.Services.WebApi
{
    public class WebApiApplication : HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            AutoMapperConfiguration.Initialize();
        }
    }
}
