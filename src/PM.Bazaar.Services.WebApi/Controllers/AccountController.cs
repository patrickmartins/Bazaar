using Microsoft.AspNet.Identity;
using PM.Bazaar.Application.ViewModels;
using PM.Bazaar.Infrastructure.CrossCutting.Configuration;
using PM.Bazaar.Services.WebApi.Extensions;
using PM.Bazaar.Services.WebApi.Filters;
using System;
using System.Drawing;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using PM.Bazaar.Application.Interfaces;
using PM.Bazaar.Domain.Values;

namespace PM.Bazaar.Services.WebApi.Controllers
{
    [RoutePrefix("api/account")]
    [BazaarExceptionFilter]
    public class AccountController : ApiController
    {
        private readonly IAccountApplicationService _service;

        public AccountController(IAccountApplicationService service)
        {
            _service = service;
        }

        [Route("me")]
        [Authorize]
        [HttpGet]
        public HttpResponseMessage MyAccount()
        {
            var result = _service.GetById(User.Identity.GetUserId<int>());

            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        [Route("register")]
        [HttpPost]
        public HttpResponseMessage Register(RegisterAccountViewModel model)
        {
            if(!ModelState.IsValid)
                return Request.CreateResponse(HttpStatusCode.BadRequest, ModelState.ToResult());

            model.Avatar = new RegisterImageViewModel
            {
                Bytes = Image.FromFile(HttpContext.Current.Server.MapPath(Configs.DefaultAvatarUrl)).ToArray(),
                Hash = Guid.NewGuid()
            };

            var result = _service.CreateAccount(model);

            if(!result.Sucess)
                return Request.CreateResponse(HttpStatusCode.BadRequest, result);

            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        [HttpPost]
        [Authorize]
        [Route("change-avatar")]
        public HttpResponseMessage ChangePicture([FromBody] string hash)
        {
            if(!Guid.TryParse(hash, out var avatarHash))
                return Request.CreateResponse(HttpStatusCode.BadRequest, new Result(new Error("Hash inválido", "Hash")));

            var result = _service.ChangeAvatar(User.Identity.GetUserId<int>(), avatarHash);

            if (!result.Sucess)
                return Request.CreateResponse(HttpStatusCode.BadRequest, result);

            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [HttpPost]
        [Authorize]
        [Route("change-password")]
        public HttpResponseMessage ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
                return Request.CreateResponse(HttpStatusCode.BadRequest, ModelState.ToResult());

            model.Id = User.Identity.GetUserId<int>();

            var result = _service.ChangePassword(model);

            if (!result.Sucess)
                return Request.CreateResponse(HttpStatusCode.BadRequest, result);

            return Request.CreateResponse(HttpStatusCode.OK, result);
        }
    }
}
