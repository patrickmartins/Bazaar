using PM.Bazaar.Domain.Interfaces.Result;
using PM.Bazaar.Domain.Interfaces.Validation;
using PM.Bazaar.Domain.Values;
using PM.Bazaar.Infrastructure.CrossCutting.Configuration;

namespace PM.Bazaar.Domain.Validations.Ad
{
    public class LengthTitleValidation : Validation<Domain.Entities.Ad>
    {
        private int _minCharacters = int.Parse(Configs.MinCharactersTitleAd);
        private int _maxCharacters = int.Parse(Configs.MaxCharactersTitleAd);

        public LengthTitleValidation()
        {
            Target = "Title";
            Error = $"O título deve conter no mínimo {_minCharacters} e no máximo {_maxCharacters} caracteres";
        }

        public override IResult IsValid(Entities.Ad entity)
        {
            var result = new Result();

            if (entity.Title.Length < _minCharacters || entity.Title.Length > _maxCharacters)
                result.AddError(Target, Error);

            return result;
        }
    }
}
