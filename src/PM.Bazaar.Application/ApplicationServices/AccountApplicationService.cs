using Microsoft.AspNet.Identity;
using PM.Bazaar.Application.ApplicationServices.Common;
using PM.Bazaar.Application.Extensions;
using PM.Bazaar.Application.ViewModels;
using PM.Bazaar.Infrastructure.CrossCutting.Identity.Entities;
using System;
using System.Collections.Generic;
using PM.Bazaar.Application.Interfaces;
using PM.Bazaar.Domain.Interfaces.Result;
using PM.Bazaar.Domain.Interfaces.Services;
using PM.Bazaar.Domain.Interfaces.UnitOfWork;
using PM.Bazaar.Domain.Values;
using PM.Bazaar.Infrastructure.CrossCutting.Identity.Interfaces.Services;

namespace PM.Bazaar.Application.ApplicationServices
{
    public class AccountApplicationService : ApplicationService, IAccountApplicationService
    {
        private readonly IAccountService _service;
        private readonly IImageService _imageService;

        public AccountApplicationService(IAccountService service, IImageService imageService, IUoW uow) : base(uow)
        {
            _service = service;
            _imageService = imageService;
        }

        public IResult AddLogin(int accountId, UserLoginInfo login)
        {
            return _service.AddLogin(accountId, login);
        }

        public IResult ChangePassword(ChangePasswordViewModel model)
        {
            BeginTransaction();

            var result = _service.ChangePassword(model.Id, model.CurrentPassword, model.Password);

            if (result.Sucess)
                Commit();

            return result;
        }

        public IResult ChangeAvatar(int accountId, Guid imageHash)
        {
            BeginTransaction();

            var resultImage = _imageService.GetByHash(imageHash);

            if (!resultImage.Sucess)
                return resultImage;

            var result =_service.ChangeAvatar(accountId, resultImage.Value);

            if (result.Sucess)
                Commit();

            return result;
        }

        public bool CheckPassword(LoginViewModel account)
        {
            return _service.CheckPassword(account.MapModelTo<Account>(), account.Password);
        }

        public IResult<AccountViewModel> GetByEmail(string email)
        {
            var result = new Result<AccountViewModel>();

            var ad = _service.GetByEmail(email);

            if (ad.Sucess)
                result.SetValue(ad.Value.MapEntityTo<AccountViewModel>());
            else
                result.Errors = ad.Errors;

            return result;
        }

        public IResult<AccountViewModel> GetById(int id)
        {
            var result = new Result<AccountViewModel>();

            var ad = _service.GetById(id);

            if (ad.Sucess)
                result.SetValue(ad.Value.MapEntityTo<AccountViewModel>());
            else
                result.Errors = ad.Errors;

            return result;
        }

        public string GetEmail(int accountId)
        {
            return _service.GetEmail(accountId);
        }

        public IList<UserLoginInfo> GetLogins(int accountId)
        {
            return _service.GetLogins(accountId);
        }

        public string GetSecurityStamp(int accountId)
        {
            return _service.GetSecurityStamp(accountId);
        }

        public IResult CreateAccount(RegisterAccountViewModel item)
        {
            BeginTransaction();

            var account = item.MapModelTo<Account>();

            var result = _service.Insert(account, account.Password);

            if (result.Sucess)
                Commit();

            return result;
        }

        public IResult RemoveAccount(RegisterAccountViewModel item)
        {
            BeginTransaction();

            var result = _service.Remove(item.MapModelTo<Account>());

            if (result.Sucess)
                Commit();

            return result;
        }

        public IResult RemoveLogin(int accountId, UserLoginInfo login)
        {
            BeginTransaction();

            var result = _service.RemoveLogin(accountId, login);

            if (result.Sucess)
                Commit();

            return result;
        }

        public IResult UpdateAccount(RegisterAccountViewModel item)
        {
            BeginTransaction();

            var result = _service.Update(item.MapModelTo<Account>());

            if (result.Sucess)
                Commit();

            return result;
        }
    }
}
