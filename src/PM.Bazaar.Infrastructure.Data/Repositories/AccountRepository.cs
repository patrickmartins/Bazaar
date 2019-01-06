using Microsoft.AspNet.Identity;
using PM.Bazaar.Infrastructure.CrossCutting.Identity.Entities;
using PM.Bazaar.Infrastructure.Data.Contexts;
using PM.Bazaar.Infrastructure.Data.Repositories.Common;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using PM.Bazaar.Infrastructure.CrossCutting.Identity.Interfaces.Repositories;

namespace PM.Bazaar.Infrastructure.Data.Repositories
{
    public class AccountRepository : Repository<Account>, IAccountRepository
    {
        public AccountRepository(BazaarContext context) : base(context) { }

        public Task CreateAsync(Account account)
        {
            if (account == null)
                throw new ArgumentNullException(nameof(account));

            Context.Set<Account>().Add(account);

            return Task.FromResult(0);
        }

        public Task UpdateAsync(Account account)
        {
            if (account == null)
                throw new ArgumentNullException(nameof(account));

            account.UpdateSecurityStamp(Guid.NewGuid().ToString());

            return Task.FromResult(0);
        }

        public Task DeleteAsync(Account account)
        {
            if (account == null)
                throw new ArgumentNullException(nameof(account));

            Context.Set<Account>().Remove(account);

            return Task.FromResult(0);
        }

        public Task<Account> FindByIdAsync(int accountId)
        {
            return Users.Include(c => c.Advertiser).FirstOrDefaultAsync(u => u.Id == accountId);
        }

        public async Task<Account> FindAsync(UserLoginInfo login)
        {
            var userLogin = await
                Context.Set<AccountLogin>().FirstOrDefaultAsync(l => l.LoginProvider == login.LoginProvider && l.ProviderKey == login.ProviderKey);

            if (userLogin != null)
                return await Users.Include(c => c.Advertiser).FirstOrDefaultAsync(u => u.Id == userLogin.UserId);

            return null;
        }

        public Task<Account> FindByNameAsync(string userName)
        {
            return Users.Include(c => c.Advertiser).FirstOrDefaultAsync(u => u.UserName == userName);
        }

        public IQueryable<Account> Users
        {
            get { return Context.Set<Account>(); }
        }

        public Task SetPasswordHashAsync(Account account, string passwordHash)
        {
            if (account == null)
            {
                throw new ArgumentNullException(nameof(account));
            }

            account.ChangePassword(passwordHash);

            return Task.FromResult(0);
        }

        public Task<string> GetPasswordHashAsync(Account account)
        {
            if (account == null)
                throw new ArgumentNullException(nameof(account));

            return Task.FromResult(account.Password);
        }

        public Task<bool> HasPasswordAsync(Account account)
        {
            return Task.FromResult(account.Password != null);
        }

        public async Task AddToRoleAsync(Account account, string roleName)
        {
            if (account == null)
                throw new ArgumentNullException(nameof(account));

            if (string.IsNullOrWhiteSpace(roleName))
                throw new ArgumentException("value cannot be null or empty", nameof(roleName));

            var roleEntity = await Context.Set<Role>().SingleOrDefaultAsync(r => r.Name.ToUpper() == roleName.ToUpper());

            if (roleEntity == null)
                throw new InvalidOperationException(String.Format(CultureInfo.CurrentCulture, "role not found", roleName));

            var ur = new AccountRole { UserId = account.Id, RoleId = roleEntity.Id };

            Context.Set<AccountRole>().Add(ur);
        }

        public async Task RemoveFromRoleAsync(Account account, string roleName)
        {
            if (account == null)
                throw new ArgumentNullException(nameof(account));

            if (String.IsNullOrWhiteSpace(roleName))
                throw new ArgumentException("value cannot be null or empty", nameof(roleName));

            var roleEntity = await Context.Set<Role>().SingleOrDefaultAsync(r => r.Name.ToUpper() == roleName.ToUpper());
            if (roleEntity != null)
            {
                var userRole = await Context.Set<AccountRole>().FirstOrDefaultAsync(r => roleEntity.Id.Equals(r.RoleId) && r.UserId.Equals(account.Id));

                if (userRole != null)
                    Context.Set<AccountRole>().Remove(userRole);

            }
        }

        public async Task<IList<string>> GetRolesAsync(Account account)
        {
            if (account == null)
                throw new ArgumentNullException(nameof(account));

            var userId = account.Id;

            var query = from userRole in Context.Set<AccountRole>()
                        join role in Context.Set<Role>() on userRole.RoleId equals role.Id
                        where userRole.UserId.Equals(userId)
                        select role.Name;

            return await query.ToListAsync();
        }

        public async Task<bool> IsInRoleAsync(Account account, string roleName)
        {
            if (account == null)
                throw new ArgumentNullException(nameof(account));

            if (string.IsNullOrWhiteSpace(roleName))
                throw new ArgumentException("value cannot be null or empty", nameof(roleName));

            var role = await Context.Set<Role>().SingleOrDefaultAsync(r => r.Name.ToUpper() == roleName.ToUpper());

            if (role != null)
            {
                var userId = account.Id;
                var roleId = role.Id;
                return await Context.Set<AccountRole>().AnyAsync(ur => ur.RoleId.Equals(roleId) && ur.UserId.Equals(userId));
            }

            return false;
        }

        public async Task<IList<Claim>> GetClaimsAsync(Account account)
        {
            if (account == null)
                throw new ArgumentNullException(nameof(account));

            return await Task.FromResult(Context.Set<AccountClaim>().Where(uc => uc.UserId == account.Id).ToList().Select(c => new Claim(c.ClaimType, c.ClaimValue)).ToList());
        }

        public Task AddClaimAsync(Account account, Claim claim)
        {
            if (account == null)
                throw new ArgumentNullException(nameof(account));

            if (claim == null)
                throw new ArgumentNullException(nameof(claim));

            Context.Set<AccountClaim>().Add(new AccountClaim { UserId = account.Id, ClaimType = claim.Type, ClaimValue = claim.Value });

            return Task.FromResult(false);
        }

        public async Task ReplaceClaimAsync(Account account, Claim claim, Claim newClaim)
        {
            if (account == null)
                throw new ArgumentNullException(nameof(account));

            if (claim == null)
                throw new ArgumentNullException(nameof(claim));

            if (newClaim == null)
                throw new ArgumentNullException(nameof(newClaim));

            var matchedClaims = await Context.Set<AccountClaim>().Where(uc => uc.UserId.Equals(account.Id) && uc.ClaimValue == claim.Value && uc.ClaimType == claim.Type).ToListAsync();

            foreach (var matchedClaim in matchedClaims)
            {
                matchedClaim.ClaimValue = newClaim.Value;
                matchedClaim.ClaimType = newClaim.Type;
            }
        }

        public async Task RemoveClaimAsync(Account account, Claim claim)
        {
            if (account == null)
                throw new ArgumentNullException(nameof(account));

            if (claim == null)
                throw new ArgumentNullException(nameof(claim));

            var matchedClaims = await Context.Set<AccountClaim>().Where(uc => uc.UserId.Equals(account.Id) && uc.ClaimValue == claim.Value && uc.ClaimType == claim.Type).ToListAsync();

            foreach (var c in matchedClaims)
                Context.Set<AccountClaim>().Remove(c);
        }

        public Task AddLoginAsync(Account account, UserLoginInfo login)
        {
            if (account == null)
                throw new ArgumentNullException(nameof(account));

            if (login == null)
                throw new ArgumentNullException(nameof(login));

            var l = new AccountLogin
            {
                UserId = account.Id,
                ProviderKey = login.ProviderKey,
                LoginProvider = login.LoginProvider
            };

            Context.Set<AccountLogin>().Add(l);

            return Task.FromResult(false);
        }

        public async Task RemoveLoginAsync(Account account, UserLoginInfo login)
        {
            if (account == null)
                throw new ArgumentNullException(nameof(account));

            var userId = account.Id;

            var entry = await Context.Set<AccountLogin>().SingleOrDefaultAsync(l => l.UserId.Equals(userId) && l.LoginProvider == login.LoginProvider && l.ProviderKey == login.ProviderKey);

            if (entry != null)
                Context.Set<AccountLogin>().Remove(entry);
        }

        public async Task<IList<UserLoginInfo>> GetLoginsAsync(Account account)
        {
            if (account == null)
                throw new ArgumentNullException(nameof(account));

            var userId = account.Id;

            return await Context.Set<AccountLogin>().Where(l => l.UserId.Equals(userId))
                .Select(l => new UserLoginInfo(l.LoginProvider, l.ProviderKey)).ToListAsync();
        }

        public Task<string> GetSecurityStampAsync(Account account)
        {
            if (account == null)
                throw new ArgumentNullException(nameof(account));

            return Task.FromResult(account.SecurityStamp);
        }

        public Task<Account> FindByEmailAsync(string email)
        {
            return Context.Set<Account>()
                .Include(u => u.Logins).Include(u => u.Roles).Include(u => u.Claims)
                .FirstOrDefaultAsync(u => u.Email == email);
        }

        public Task<string> GetEmailAsync(Account account)
        {
            if (account == null)
                throw new ArgumentNullException(nameof(account));

            return Task.FromResult(account.Email);
        }

        public Task SetEmailAsync(Account account, string email)
        {
            if (account == null)
                throw new ArgumentNullException(nameof(account));

            account.UpdateEmail(email);

            return Task.FromResult(0);
        }

        public Task<bool> GetEmailConfirmedAsync(Account account)
        {
            throw new NotImplementedException();
        }

        public Task SetEmailConfirmedAsync(Account account, bool confirmed)
        {
            throw new NotImplementedException();
        }

        public Task SetSecurityStampAsync(Account account, string stamp)
        {
            if (account == null)
                throw new ArgumentNullException(nameof(account));

            account.UpdateSecurityStamp(stamp);
            return Task.FromResult(0);
        }
    }
}
