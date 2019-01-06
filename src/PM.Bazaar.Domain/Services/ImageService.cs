using PM.Bazaar.Domain.Entities;
using PM.Bazaar.Domain.Interfaces.Repositories.Common;
using PM.Bazaar.Domain.Interfaces.Result;
using PM.Bazaar.Domain.Interfaces.Services;
using PM.Bazaar.Domain.Services.Common;
using PM.Bazaar.Domain.Values;
using System;

namespace PM.Bazaar.Domain.Services
{
    public class ImageService : Service<Image>, IImageService
    {
        public ImageService(IRepository<Image> repository) : base(repository)
        { }

        public IResult<Image> GetByHash(Guid hash)
        {
            var result = new Result<Image>();
            var entitie = Repository.FirstOrDefault(c => c.Hash == hash);

            if (entitie != null)
                result.Value = entitie;
            else
                result.Errors.Add(new Error("A imagem informada não foi encontrada", "Hash"));
            
            return result;
        }

        public IResult RemoveImage(Image image)
        {
            return Remove(image);
        }

        public IResult SaveImage(Image image)
        {
            return Insert(image);
        }
    }
}
