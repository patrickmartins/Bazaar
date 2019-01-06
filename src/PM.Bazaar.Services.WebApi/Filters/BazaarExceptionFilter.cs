using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Filters;

namespace PM.Bazaar.Services.WebApi.Filters
{
    public class BazaarExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            actionExecutedContext.ActionContext.Response = actionExecutedContext.Request.CreateResponse(HttpStatusCode.InternalServerError);
        }

        public override Task OnExceptionAsync(HttpActionExecutedContext actionExecutedContext, CancellationToken cancellationToken)
        {
            return Task.Factory.StartNew(c => { OnException(actionExecutedContext); }, new object(), cancellationToken);
        }
    }
}