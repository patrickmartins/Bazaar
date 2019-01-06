using System;
using PM.Bazaar.Application.ViewModels;
using PM.Bazaar.Domain.Interfaces.Result;

namespace PM.Bazaar.Application.Interfaces
{
    public interface IImageApplicationService
    {
        IResult SaveImage(RegisterImageViewModel item);
        IResult RemoveImage(ImageViewModel item);
        IResult<ImageViewModel> GetById(int id);
        IResult<ImageViewModel> GetByHash(Guid hash);
    }
}
