using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Owin;
using PM.Bazaar.Infrastructure.CrossCutting.Configuration;
using PM.Bazaar.Infrastructure.CrossCutting.Identity.Interfaces.Services;
using PM.Bazaar.Services.WebApi;
using System;
using System.Threading.Tasks;
using System.Web.Http;

[assembly: OwinStartup(typeof(Startup))]

namespace PM.Bazaar.Services.WebApi
{
    public class Startup
    {
        private readonly int _minPasswordCharacters = int.Parse(Configs.MinCharactersPassword);
        private readonly int _maxPasswordCharacters = int.Parse(Configs.MaxCharactersPassword);
        private readonly int _minEmailCharacters = int.Parse(Configs.MinCharactersEmail);
        private readonly int _maxEmailCharacters = int.Parse(Configs.MaxCharactersEmail);

        public void Configuration(IAppBuilder app)
        {
            SimpleInjectorInitializer.Initialize(app);

            ConfigureAuth(app);
        }

        public void ConfigureAuth(IAppBuilder app)
        {
            var serverOptions = new OAuthAuthorizationServerOptions
            {
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/oauth/token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(30),
                Provider = new OAuthAuthorizationServerProvider
                {
                    OnGrantResourceOwnerCredentials = OnGrantResourceOwnerCredentials,
                    OnValidateClientAuthentication = OnValidateClientAuthentication
                }
            };

            app.UseOAuthBearerTokens(serverOptions);
        }

        private Task OnValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            return Task.Factory.StartNew(context.Validated);
        }

        private Task OnGrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            return Task.Factory.StartNew(() =>
            {
                var accountService = (IAccountService)GlobalConfiguration.Configuration.DependencyResolver.GetService(typeof(IAccountService));

                if (context.Password.Length < _minPasswordCharacters || context.Password.Length > _maxPasswordCharacters ||
                    context.UserName.Length < _minEmailCharacters || context.UserName.Length > _maxEmailCharacters)
                {
                    context.SetError("Usuário ou senha incorreto");
                    return;
                }

                var user = accountService.Get(context.UserName, context.Password);

                if (user.Sucess)
                    context.Validated(accountService.CreateIdentity(user.Value, "Bearer"));
                else
                    context.SetError("Usuário ou senha incorreto");
            });
        }
    }
}