using System.Net;
using System.Net.Http;
using PM.Bazaar.Application.Abstractions;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using PM.Bazaar.Application.ViewModels;
using PM.Bazaar.Services.WebApi.Extensions;
using PM.Bazaar.Services.WebApi.Filters;

namespace PM.Bazaar.Services.WebApi.Controllers
{
    [RoutePrefix("api/question")]
    [BazaarExceptionFilter]
    public class QuestionController : ApiController
    {
        private readonly IQuestionApplicationService _service;

        public QuestionController(IQuestionApplicationService service)
        {
            _service = service;
        }

        [Route("")]
        [HttpPost]
        [Authorize]
        public HttpResponseMessage Insert(RegisterQuestionViewModel question)
        {
            if (!ModelState.IsValid)
                return Request.CreateResponse(HttpStatusCode.BadRequest, ModelState.GetResult());

            question.IdUser = User.Identity.GetUserId<int>();

            var result = _service.CreateQuestion(question);

            if (!result.Sucess)
                return Request.CreateResponse(HttpStatusCode.BadRequest, result);

            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [Route("by-ad/{ad:int}")]
        public HttpResponseMessage GetByAd(int ad)
        {
            var result = _service.GetByAd(ad);

            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        [Route("by-ad/{ad:int}/count/{count:int}")]
        public HttpResponseMessage GetByAd(int ad, int count)
        {
            var result = _service.GetByAd(ad, count);

            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        [Route("by-ad/{ad:int}/count/{count:int}/initial/{initialId:int}/")]
        public HttpResponseMessage GetByAd(int ad, int count, int initialId)
        {
            var result = _service.GetByAd(ad, initialId, count);

            return Request.CreateResponse(HttpStatusCode.OK, result);
        }
    }
}
