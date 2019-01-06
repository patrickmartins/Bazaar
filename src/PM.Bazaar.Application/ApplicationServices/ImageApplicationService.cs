using PM.Bazaar.Application.ApplicationServices.Common;
using PM.Bazaar.Application.Extensions;
using PM.Bazaar.Application.ViewModels;
using PM.Bazaar.Domain.Entities;
using System;
using PM.Bazaar.Application.Interfaces;
using PM.Bazaar.Domain.Interfaces.Result;
using PM.Bazaar.Domain.Interfaces.Services;
using PM.Bazaar.Domain.Interfaces.UnitOfWork;
using PM.Bazaar.Domain.Values;

namespace PM.Bazaar.Application.ApplicationServices
{
    public class ImageApplicationService : ApplicationService, IImageApplicationService
    {
        private readonly IImageService _service;

        public ImageApplicationService(IImageService service, IUoW uow) : base(uow)
        {
            _service = service;
        }

        public IResult SaveImage(RegisterImageViewModel item)
        {
            BeginTransaction();

            var result = _service.SaveImage(item.MapModelTo<Image>());

            if (result.Sucess)
                Commit();

            return result;
        }

        public IResult RemoveImage(ImageViewModel item)
        {
            BeginTransaction();

            var result = _service.RemoveImage(item.MapModelTo<Image>());

            if (result.Sucess)
                Commit();

            return result;
        }

        public IResult<ImageViewModel> GetById(int id)
        {
            var result = new Result<ImageViewModel>();

            var ad = _service.GetById(id);

            if (ad.Sucess)
                result.SetValue(ad.Value.MapEntityTo<ImageViewModel>());
            else
                result.Errors = ad.Errors;

            return result;
        }

        public IResult<ImageViewModel> GetByHash(Guid hash)
        {
            var result = new Result<ImageViewModel>();

            var ad = _service.GetByHash(hash);

            if (ad.Sucess)
                result.SetValue(ad.Value.MapEntityTo<ImageViewModel>());
            else
                result.Errors = ad.Errors;

            return result;
        }
    }
}
