using System.Security.Claims;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;

namespace PM.Bazaar.Infrastructure.CrossCutting.Identity.Interfaces.Services
{
    public interface IAuthService
    {
        int CurrentUserId { get; }
        string CurrentUserName { get; }
        bool IsAuthenticated { get; }
        SignInStatus PasswordSignIn(string userName, string password, bool isPersistent, bool shouldLockout);
        void SignIn(AuthenticationProperties properties, params ClaimsIdentity[] identities);
        void SignIn(params ClaimsIdentity[] identities);
        void SignOut(params string[] authenticationTypes);
        void Challenge(AuthenticationProperties properties, params string[] authenticationTypes);
        void Challenge(params string[] authenticationTypes);
    }
}
