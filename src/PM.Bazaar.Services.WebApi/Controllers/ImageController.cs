using PM.Bazaar.Application.Interfaces;
using PM.Bazaar.Application.ViewModels;
using PM.Bazaar.Infrastructure.CrossCutting.Configuration;
using PM.Bazaar.Services.WebApi.Extensions;
using PM.Bazaar.Services.WebApi.Filters;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace PM.Bazaar.Services.WebApi.Controllers
{
    [RoutePrefix("api/image")]
    [BazaarExceptionFilter]
    public class ImageController : ApiController
    {
        private readonly IImageApplicationService _service;
        public ImageController(IImageApplicationService service)
        {
            _service = service;
        }

        [HttpGet]
        [Route("{id:int}")]
        public HttpResponseMessage GetById(int id)
        {
            var result = _service.GetById(id);

            if (!result.Sucess)
                return Request.CreateResponse(HttpStatusCode.NotFound, result);

            var response = Request.CreateResponse(HttpStatusCode.OK);

            response.Content = new StreamContent(new MemoryStream(result.Value.Bytes));
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("image/png");

            return response;
        }

        [HttpPost]
        [BazaarValidationPictureFilter]
        [Route("upload-avatar")]
        public async Task<HttpResponseMessage> SaveAvatarAsync()
        {
            return await SaveImageAsync(int.Parse(Configs.MaxWidthAvatar), int.Parse(Configs.MaxHeightAvatar));
        }

        [HttpPost]
        [Authorize]
        [BazaarValidationPictureFilter]
        [Route("upload-picture-ad")]
        public async Task<HttpResponseMessage> SavePictureAsync()
        {
            return await SaveImageAsync(int.Parse(Configs.MaxWidthPictureAd), int.Parse(Configs.MaxHeightPictureAd));
        }

        private async Task<HttpResponseMessage> SaveImageAsync(int maxWidth, int maxHeight)
        {
            var hash = Guid.NewGuid();
            var image = HttpContext.Current.Request.Files[0];
            var buffer = new byte[image.ContentLength];

            await HttpContext.Current.Request.Files[0].InputStream.ReadAsync(buffer, 0, buffer.Length);

            var result = _service.SaveImage(new RegisterImageViewModel { Bytes = buffer.ToImage().Resize(maxWidth, maxHeight).ToArray(), Hash = hash });

            if (!result.Sucess)
                return Request.CreateResponse(HttpStatusCode.BadRequest, result);

            return Request.CreateResponse(HttpStatusCode.OK, hash);
        }
    }
}
