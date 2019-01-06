using Microsoft.AspNet.Identity;
using PM.Bazaar.Infrastructure.CrossCutting.Identity.Entities;
using PM.Bazaar.Infrastructure.CrossCutting.Identity.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Threading.Tasks;
using PM.Bazaar.Domain.Entities;
using PM.Bazaar.Domain.Interfaces.Result;
using PM.Bazaar.Domain.Values;
using PM.Bazaar.Infrastructure.CrossCutting.Identity.Interfaces.Repositories;
using PM.Bazaar.Infrastructure.CrossCutting.Identity.Interfaces.Services;

namespace PM.Bazaar.Infrastructure.CrossCutting.Identity.Services
{
    public class AccountService : UserManager<Account, int>, IAccountService
    {
        protected readonly IAccountRepository Repository;
        private bool _disposed;

        public AccountService(IAccountRepository repository) : base(repository)
        {
            Repository = repository;

            UserValidator = new IdentityUserValidator();
            PasswordValidator = new IdentityPasswordValidator();

            UserLockoutEnabledByDefault = true;
        }

        public IResult Insert(Account item)
        {
            var result = Insert(item, item.Password);

            return result;
        }

        public IResult Update(Account item)
        {
            var result = GetById(item.Id);

            if (result.Sucess)
            {
                var resultValidate = item.IsValid();

                if (!resultValidate.Sucess)
                    return resultValidate;

                Repository.Update(item);
            }

            return result;
        }

        public IQueryable<Account> Find(Expression<Func<Account, bool>> predicate)
        {
            return Repository.Find(predicate);
        }

        public IResult AddClaim(int accountId, Claim claim)
        {
            var result = new Result();

            var temp = AddClaimAsync(accountId, claim).Result.Errors;
            temp.ToList().ForEach(c => { result.AddError("User", c); });

            return result;
        }

        public IResult AddLogin(int accountId, UserLoginInfo login)
        {
            var result = new Result();

            var temp = AddLoginAsync(accountId, login).Result.Errors;
            temp.ToList().ForEach(c => { result.AddError("User", c); });

            return result;
        }

        public IResult AddPassword(int accountId, string password)
        {
            var result = new Result();

            var temp = AddPasswordAsync(accountId, password).Result.Errors;
            temp.ToList().ForEach(c => { result.AddError("User", c); });

            return result;
        }

        public IResult AddToRole(int accountId, string role)
        {
            var result = new Result();

            var temp = AddToRoleAsync(accountId, role).Result.Errors;
            temp.ToList().ForEach(c => { result.AddError("User", c); });

            return result;
        }

        public IResult AddToRoles(int accountId, params string[] roles)
        {
            var result = new Result();

            var temp = AddToRolesAsync(accountId, roles).Result.Errors;
            temp.ToList().ForEach(c => { result.AddError("User", c); });

            return result;
        }

        public IResult ChangePassword(int accountId, string currentPassword, string newPassword)
        {
            var result = GetById(accountId);

            if (!result.Sucess)
                return result;

            result.Value.ChangePassword(newPassword);

            var resultVal = result.Value.IsValid();

            if (!resultVal.Sucess)
                return resultVal;

            if (!CheckPassword(result.Value, currentPassword))
                return new Result(new Error("Senha atual incorreta", "CurrentPassword"));

            var temp = ChangePasswordAsync(accountId, currentPassword, newPassword).Result.Errors;
            temp.ToList().ForEach(c => { result.Errors.Add(new Error(c, "Password")); });

            return result;
        }

        public IResult ChangeAvatar(int accountId, Image avatar)
        {
            var result = GetById(accountId);

            if (!result.Sucess)
                return result;
            
           var valid = result.Value.Advertiser.ChangeAvatar(avatar);

            if (!valid.Sucess)
                return valid;

            return Update(result.Value);
        }

        public bool CheckPassword(Account user, string password)
        {
            return CheckPasswordAsync(user, password).Result;
        }

        public new Task<IdentityResult> CreateAsync(Account user)
        {
            var result = Insert(user);

            return Task.FromResult(new IdentityResult(result.Errors.Select(c => c.Description)));
        }

        public new Task<IdentityResult> CreateAsync(Account user, string password)
        {
            var result = Insert(user, password);

            return Task.FromResult(new IdentityResult(result.Errors.Select(c => c.Description)));
        }

        public virtual IResult Insert(Account user, string password)
        {
            var result = user.IsValid();

            var exists = new EmailAlreadyUsedValidation(Repository);

            if (!exists.IsValid(user).Sucess)
                result.Errors.Add(new Error(exists.Error, exists.Target));

            if (result.Sucess)
                base.CreateAsync(user, password).Wait();

            return result;
        }

        public virtual ClaimsIdentity CreateIdentity(Account user, string authenticationType)
        {
            return CreateIdentityAsync(user, authenticationType).Result;
        }

        public IResult Remove(Account user)
        {
            var result = new Result();

            var temp = DeleteAsync(user).Result.Errors;
            temp.ToList().ForEach(c => { result.AddError("User", c); });

            return result;
        }

        public IResult<Account> Get(string userName, string password)
        {
            var result = new Result<Account>();

            result.SetValue(FindAsync(userName, password).Result);

            if (result.Value == null)
                result.AddError("User", "Usuário não encontrado");

            return result;
        }

        public IResult<Account> GetByEmail(string email)
        {
            var result = new Result<Account>();

            result.SetValue(FindByEmailAsync(email).Result);

            if (result.Value == null)
                result.AddError("User", "O e-mail informado não pertence a nenhum usuário cadastrado");

            return result;
        }

        public IResult<Account> GetByName(string userName)
        {
            var result = new Result<Account>();

            result.SetValue(FindByNameAsync(userName).Result);

            if (result.Value == null)
                result.AddError("User", "Usuário não encontrado");

            return result;
        }

        public IResult<Account> GetById(int accountId)
        {
            var result = new Result<Account>();

            result.SetValue(FindByIdAsync(accountId).Result);

            if (result.Value == null)
                result.AddError("User", "Usuário não encontrado");

            return result;
        }

        public IList<Claim> GetClaims(int accountId)
        {
            return GetClaimsAsync(accountId).Result;
        }

        public string GetEmail(int accountId)
        {
            return GetEmailAsync(accountId).Result;
        }

        public IList<UserLoginInfo> GetLogins(int accountId)
        {
            return GetLoginsAsync(accountId).Result;
        }

        public IList<string> GetRoles(int accountId)
        {
            return GetRolesAsync(accountId).Result;
        }

        public string GetSecurityStamp(int accountId)
        {
            return GetSecurityStampAsync(accountId).Result;
        }

        public bool HasPassword(int accountId)
        {
            return HasPasswordAsync(accountId).Result;
        }

        public bool IsInRole(int accountId, string role)
        {
            return IsInRoleAsync(accountId, role).Result;
        }

        public IResult RemoveClaim(int accountId, Claim claim)
        {
            var result = new Result();

            var temp = RemoveClaimAsync(accountId, claim).Result.Errors;
            temp.ToList().ForEach(c => { result.AddError("User", c); });

            return result;
        }

        public IResult RemoveFromRole(int accountId, string role)
        {
            var result = new Result();

            var temp = RemoveFromRoleAsync(accountId, role).Result.Errors;
            temp.ToList().ForEach(c => { result.AddError("User", c); });

            return result;
        }

        public IResult RemoveFromRoles(int accountId, params string[] roles)
        {
            var result = new Result();

            var temp = RemoveFromRolesAsync(accountId, roles).Result.Errors;
            temp.ToList().ForEach(c => { result.AddError("User", c); });

            return result;
        }

        public IResult RemoveLogin(int accountId, UserLoginInfo login)
        {
            var result = new Result();

            var temp = RemoveLoginAsync(accountId, login).Result.Errors;
            temp.ToList().ForEach(c => { result.AddError("User", c); });

            return result;
        }

        public IResult ResetPassword(int accountId, string token, string newPassword)
        {
            var result = new Result();

            var temp = ResetPasswordAsync(accountId, token, newPassword).Result.Errors;
            temp.ToList().ForEach(c => { result.AddError("User", c); });

            return result;
        }

        public override Task<IdentityResult> UpdateAsync(Account user)
        {
            var result = user.IsValid();

            if (result.Sucess)
                base.UpdateAsync(user).Wait();

            return Task.FromResult(new IdentityResult(result.Errors.Select(c => c.Description)));
        }

        public IResult UpdateSecurityStamp(int accountId)
        {
            var result = new Result();

            var temp = UpdateSecurityStampAsync(accountId).Result.Errors;
            temp.ToList().ForEach(c => { result.AddError("User", c); });

            return result;
        }

        public new void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected override void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    Repository.Dispose();
                }
            }

            _disposed = true;
        }
    }

    public class IdentityUserValidator : IIdentityValidator<Account>
    {
        public Task<IdentityResult> ValidateAsync(Account item)
        {
            return Task.FromResult(IdentityResult.Success);
        }
    }

    public class IdentityPasswordValidator : IIdentityValidator<string>
    {
        public Task<IdentityResult> ValidateAsync(string item)
        {

            return Task.FromResult(IdentityResult.Success);
        }
    }
}
