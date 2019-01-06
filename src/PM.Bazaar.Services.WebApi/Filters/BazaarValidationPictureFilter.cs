using PM.Bazaar.Infrastructure.CrossCutting.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using PM.Bazaar.Domain.Values;

namespace PM.Bazaar.Services.WebApi.Filters
{
    public class BazaarValidationPictureFilter : ActionFilterAttribute
    {
        private readonly int _maxPictureLength = int.Parse(Configs.MaxPictureLength);

        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            var mimes = new[] { "image/jpg", "image/png", "image/jpeg" };
            var request = HttpContext.Current.Request;

            if (request.Files.Count == 1)
            {
                if (!mimes.Select(c => c.Equals(request.Files[0].ContentType)).Any())
                    actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.UnsupportedMediaType, new Result(new Error("Tipo de arquivo não suportado", "Image")));

                if (request.Files[0].ContentLength/1024 > _maxPictureLength)
                    actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.BadRequest, new Result(new Error($"O arquivo não deve ultrapassar { _maxPictureLength/1024 } MB", "Image")));
            }
            else
            {
                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.BadRequest, new Result(new Error("Tipo de arquivo não suportado", "Image")));
            }
        }
    }
}