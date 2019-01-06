using PM.Bazaar.Domain.Interfaces.Result;
using PM.Bazaar.Domain.Interfaces.Validation;
using PM.Bazaar.Domain.Values;
using PM.Bazaar.Infrastructure.CrossCutting.Configuration;

namespace PM.Bazaar.Domain.Validations.Advertiser
{
    public class LengthNameValidation : Validation<Domain.Entities.Advertiser>
    {
        private int _minCharacters = int.Parse(Configs.MinCharactersName);
        private int _maxCharacters = int.Parse(Configs.MaxCharactersName);

        public LengthNameValidation()
        {
            Target = "Name";
            Error = $"O nome deve conter no mínimo {_minCharacters} e no máximo {_maxCharacters} caracteres";
        }

        public override IResult IsValid(Entities.Advertiser entity)
        {
            var result = new Result();

            if (entity.Name.Length < _minCharacters || entity.Name.Length > _maxCharacters)
                result.AddError(Target, Error);

            return result;
        }
    }
}
