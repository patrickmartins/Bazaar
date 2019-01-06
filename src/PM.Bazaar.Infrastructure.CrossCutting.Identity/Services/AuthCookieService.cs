using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using PM.Bazaar.Domain.Values;
using PM.Bazaar.Infrastructure.CrossCutting.Identity.Interfaces.Services;

namespace PM.Bazaar.Infrastructure.CrossCutting.Identity.Services
{
    public class AuthCookieApplicationService : IAuthCookieService
    {
        private readonly IAuthService _service;

        public AuthCookieApplicationService(IAuthService service)
        {
            _service = service;
        }

        public int CurrentUserId => _service.CurrentUserId;

        public string CurrentUserName => _service.CurrentUserName;

        public bool IsAuthenticated => _service.IsAuthenticated;

        public Result Login(string email, string password, bool remember = false)
        {
            var result = new Result();
            var resultService = _service.PasswordSignIn(email, password, remember, true);

            if (resultService == SignInStatus.Failure)
                result.AddError(new Error("Email ou senha incorretos", "Email"));

            return result;
        }

        public void Logout()
        {
            _service.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
        }
    }
}
