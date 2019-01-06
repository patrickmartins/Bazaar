using System;
using System.Collections.Generic;
using Microsoft.AspNet.Identity;
using PM.Bazaar.Application.ViewModels;
using PM.Bazaar.Domain.Interfaces.Result;

namespace PM.Bazaar.Application.Interfaces
{
    public interface IAccountApplicationService 
    {
        IResult CreateAccount(RegisterAccountViewModel item);
        IResult RemoveAccount(RegisterAccountViewModel item);
        IResult UpdateAccount(RegisterAccountViewModel item);
        IResult<AccountViewModel> GetById(int id);
        IResult AddLogin(int accountId, UserLoginInfo login);
        IResult ChangePassword(ChangePasswordViewModel model);
        IResult ChangeAvatar(int accountId, Guid imageHash);
        bool CheckPassword(LoginViewModel account);
        IResult<AccountViewModel> GetByEmail(string email);
        string GetEmail(int accountId);
        IList<UserLoginInfo> GetLogins(int accountId);
        string GetSecurityStamp(int accountId);
        IResult RemoveLogin(int accountId, UserLoginInfo login);
    }
}
