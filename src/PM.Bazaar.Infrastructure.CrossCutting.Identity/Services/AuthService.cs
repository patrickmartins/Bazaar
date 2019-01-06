using System.Security.Claims;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using PM.Bazaar.Infrastructure.CrossCutting.Identity.Entities;
using PM.Bazaar.Infrastructure.CrossCutting.Identity.Interfaces.Services;

namespace PM.Bazaar.Infrastructure.CrossCutting.Identity.Services
{
    public class AuthService : SignInManager<Account, int>, IAuthService
    {
        public int CurrentUserId => AuthenticationManager.User.Identity.GetUserId<int>();
        public string CurrentUserName => AuthenticationManager.User.Identity.Name;
        public bool IsAuthenticated => AuthenticationManager.User.Identity.IsAuthenticated;

        public AuthService(AccountService service, IAuthenticationManager authenticationManager) : base(service, authenticationManager)
        { }

        public SignInStatus PasswordSignIn(string userName, string password, bool isPersistent, bool shouldLockout)
        {
            return PasswordSignInAsync(userName, password, isPersistent, shouldLockout).Result;
        }

        public void SignIn(AuthenticationProperties properties, params ClaimsIdentity[] identities)
        {
            AuthenticationManager.SignIn(properties, identities);
        }

        public void SignIn(params ClaimsIdentity[] identities)
        {
            AuthenticationManager.SignIn(identities);
        }

        public void SignOut(params string[] authenticationTypes)
        {
            AuthenticationManager.SignOut(authenticationTypes);
        }

        public void Challenge(params string[] authenticationTypes)
        {
            AuthenticationManager.Challenge(authenticationTypes);
        }

        public void Challenge(AuthenticationProperties properties, params string[] authenticationTypes)
        {
            AuthenticationManager.Challenge(properties, authenticationTypes);
        }
    }
}
