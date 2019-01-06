using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using Microsoft.AspNet.Identity;
using PM.Bazaar.Domain.Entities;
using PM.Bazaar.Domain.Interfaces.Result;
using PM.Bazaar.Infrastructure.CrossCutting.Identity.Entities;

namespace PM.Bazaar.Infrastructure.CrossCutting.Identity.Interfaces.Services
{
    public interface IAccountService : IDisposable
    {
        IResult Insert(Account item);
        IResult Remove(Account item);
        IResult Update(Account item);
        IResult<Account> GetById(int id);
        IQueryable<Account> Find(Expression<Func<Account, bool>> predicate);
        IResult AddClaim(int accountId, Claim claim);
        IResult AddLogin(int accountId, UserLoginInfo login);
        IResult AddPassword(int accountId, string password);
        IResult AddToRole(int accountId, string role);
        IResult AddToRoles(int accountId, params string[] roles);
        IResult ChangePassword(int accountId, string currentPassword, string newPassword);
        IResult ChangeAvatar(int accountId, Image picture);

        bool CheckPassword(Account account, string password);
        IResult Insert(Account account, string password);
        ClaimsIdentity CreateIdentity(Account account, string authenticationType);
        IResult<Account> Get(string userName, string password);
        IResult<Account> GetByEmail(string email);
        IResult<Account> GetByName(string userName);
        IList<Claim> GetClaims(int accountId);
        string GetEmail(int accountId);
        IList<UserLoginInfo> GetLogins(int accountId);
        IList<string> GetRoles(int accountId);
        string GetSecurityStamp(int accountId);
        bool HasPassword(int accountId);
        bool IsInRole(int accountId, string role);
        IResult RemoveClaim(int accountId, Claim claim);
        IResult RemoveFromRole(int accountId, string role);
        IResult RemoveFromRoles(int accountId, params string[] roles);
        IResult RemoveLogin(int accountId, UserLoginInfo login);
        IResult ResetPassword(int accountId, string token, string newPassword);
        IResult UpdateSecurityStamp(int accountId);
    }
}
