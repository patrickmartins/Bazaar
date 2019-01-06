using PM.Bazaar.Domain.Values;

namespace PM.Bazaar.Infrastructure.CrossCutting.Identity.Interfaces.Services
{
    public interface IAuthCookieService
    {
        int CurrentUserId { get; }
        string CurrentUserName { get; }
        bool IsAuthenticated { get; }
        Result Login(string email, string password, bool remember = false);
        void Logout();
    }
}
