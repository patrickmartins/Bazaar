using PM.Bazaar.Domain.Interfaces.Result;
using PM.Bazaar.Domain.Interfaces.Validation;
using PM.Bazaar.Domain.Values;
using PM.Bazaar.Infrastructure.CrossCutting.Configuration;

namespace PM.Bazaar.Domain.Validations.Ad
{
    public class LengthDescriptionValidation : Validation<Domain.Entities.Ad>
    {
        private readonly int _minCharacters = int.Parse(Configs.MinCharactersDescriptionAd);
        private readonly int _maxCharacters = int.Parse(Configs.MaxCharactersDescriptionAd);

        public LengthDescriptionValidation()
        {
            Target = "Description";
            Error = $"A descrição deve conter no mínimo {_minCharacters} e no máximo {_maxCharacters} caracteres";
        }

        public override IResult IsValid(Entities.Ad entity)
        {
            var result = new Result();

            if (entity.Description.Length < _minCharacters || entity.Description.Length > _maxCharacters)
                result.AddError(Target, Error);

            return result;
        }
    }
}
