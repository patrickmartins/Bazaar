using PM.Bazaar.Application.Interfaces;
using PM.Bazaar.Services.WebApi.Filters;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PM.Bazaar.Services.WebApi.Controllers
{
    [RoutePrefix("api/category")]
    [BazaarExceptionFilter]
    public class CategoriesController : ApiController
    {
        private readonly ICategoryApplicationService _service;
        public CategoriesController(ICategoryApplicationService service)
        {
            _service = service;
        }

        [Route("")]
        [HttpGet]
        public HttpResponseMessage All()
        {
            return Request.CreateResponse(HttpStatusCode.OK, _service.All());
        }
    }
}
