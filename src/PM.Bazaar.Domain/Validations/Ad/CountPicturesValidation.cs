using PM.Bazaar.Infrastructure.CrossCutting.Configuration;
using System.Linq;
using PM.Bazaar.Domain.Interfaces.Result;
using PM.Bazaar.Domain.Interfaces.Validation;
using PM.Bazaar.Domain.Values;

namespace PM.Bazaar.Domain.Validations.Ad
{
    public class CountPicturesValidation : Validation<Domain.Entities.Ad>
    {
        private double _minPicture = double.Parse(Configs.MinPicturesAd);
        private double _maxPicture = double.Parse(Configs.MaxPicturesAd);

        public CountPicturesValidation()
        {
            Target = "Pictures";
            Error = $"O anúncio deve conter no mínimo {_minPicture} e no máximo {_maxPicture} fotos do produto";
        }

        public override IResult IsValid(Entities.Ad entity)
        {
            var result = new Result();

            if (!entity.Pictures.Any())
                result.AddError(Target, Error);

            return result;
        }
    }
}
