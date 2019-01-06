using PM.Bazaar.Domain.Entities;
using PM.Bazaar.Domain.Interfaces.Result;
using System;

namespace PM.Bazaar.Domain.Interfaces.Services
{
    public interface IImageService 
    {
        IResult SaveImage(Image image);
        IResult RemoveImage(Image image);
        IResult<Image> GetById(int id);
        IResult<Image> GetByHash(Guid hash);
    }
}
