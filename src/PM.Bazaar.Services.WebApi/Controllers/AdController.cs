using Microsoft.AspNet.Identity;
using PM.Bazaar.Application.Interfaces;
using PM.Bazaar.Application.ViewModels;
using PM.Bazaar.Services.WebApi.Extensions;
using PM.Bazaar.Services.WebApi.Filters;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PM.Bazaar.Services.WebApi.Controllers
{
    [RoutePrefix("api/ad")]
    [BazaarExceptionFilter]
    public class AdController : ApiController
    {
        private readonly IAdvertisingApplicationService _service;

        public AdController(IAdvertisingApplicationService service)
        {
            _service = service;
        }

        [HttpGet]
        [Route("search")]
        public HttpResponseMessage Search([FromUri] SearchAdViewModel filter)
        {
            if (!ModelState.IsValid)
                return Request.CreateResponse(HttpStatusCode.BadRequest, ModelState.ToResult());

            return Request.CreateResponse(HttpStatusCode.OK, _service.Search(filter ?? new SearchAdViewModel()));
        }

        [Route("")]
        [HttpPost]
        [Authorize]
        public HttpResponseMessage PublishAd(RegisterAdViewModel item)
        {
            if (!ModelState.IsValid)
                return Request.CreateResponse(HttpStatusCode.BadRequest, ModelState.ToResult());

            item.IdAdvertiser = User.Identity.GetUserId<int>();

            var result = _service.PublishAd(item);

            if (!result.Sucess)
                return Request.CreateResponse(HttpStatusCode.BadRequest, result);

            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [Route("{idAd:int}/ask")]
        [HttpPost]
        [Authorize]
        public HttpResponseMessage Ask(int idAd, RegisterQuestionViewModel question)
        {
            if (!ModelState.IsValid)
                return Request.CreateResponse(HttpStatusCode.BadRequest, ModelState.ToResult());

            question.IdAdvertiser = User.Identity.GetUserId<int>();
            question.IdAd = idAd;

            var result = _service.Ask(question);

            if (!result.Sucess)
                return Request.CreateResponse(HttpStatusCode.BadRequest, result);

            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [Route("{idAd:int}/question/{idQuestion:int}/answer")]
        [HttpPost]
        [Authorize]
        public HttpResponseMessage Answer(int idAd, int idQuestion, RegisterResponseViewModel response)
        {
            if (!ModelState.IsValid)
                return Request.CreateResponse(HttpStatusCode.BadRequest, ModelState.ToResult());

            response.IdAdvertiser = User.Identity.GetUserId<int>();
            response.IdQuestion = idQuestion;
            response.IdAd = idAd;

            var result = _service.Answer(response);

            if (!result.Sucess)
                return Request.CreateResponse(HttpStatusCode.BadRequest, result);

            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [Route("{id:int}")]
        [HttpGet]
        public HttpResponseMessage GetById(int id)
        {
            var result = _service.GetById(id);

            if (!result.Sucess)
                return Request.CreateResponse(HttpStatusCode.NotFound, result);

            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        [Route("most-recent/{number:int:max(20)}")]
        [HttpGet]
        public HttpResponseMessage MostRecent(int number)
        {
            var result =
                _service.Search(new SearchAdViewModel
                {
                    Page = 1,
                    PageSize = number,
                    Order = SearchAdViewModel.OrderBy.Publish
                });

            return Request.CreateResponse(HttpStatusCode.OK, result);
        }
    }
}
