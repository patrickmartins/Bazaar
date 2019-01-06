using Microsoft.AspNet.Identity;
using PM.Bazaar.Domain.Interfaces.Repositories.Common;
using PM.Bazaar.Infrastructure.CrossCutting.Identity.Entities;

namespace PM.Bazaar.Infrastructure.CrossCutting.Identity.Interfaces.Repositories
{
    public interface IAccountRepository : IRepository<Account>,
        IUserLoginStore<Account, int>,
        IUserEmailStore<Account, int>,
        IUserClaimStore<Account, int>,
        IUserRoleStore<Account, int>,
        IUserPasswordStore<Account, int>,
        IUserSecurityStampStore<Account, int>,
        IQueryableUserStore<Account, int>
    { }
}
