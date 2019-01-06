using PM.Bazaar.Domain.Interfaces.Result;
using PM.Bazaar.Domain.Interfaces.Validation;
using PM.Bazaar.Domain.Values;
using PM.Bazaar.Infrastructure.CrossCutting.Configuration;

namespace PM.Bazaar.Domain.Validations.Advertiser
{
    public class LengthLastNameValidation : Validation<Domain.Entities.Advertiser>
    {
        private int _minCharacters = int.Parse(Configs.MinCharactersLastName);
        private int _maxCharacters = int.Parse(Configs.MaxCharactersLastName);

        public LengthLastNameValidation()
        {
            Target = "LastName";
            Error = $"O sobrenome deve conter no mínimo {_minCharacters} e no máximo {_maxCharacters} caracteres";
        }

        public override IResult IsValid(Entities.Advertiser entity)
        {
            var result = new Result();

            if (entity.LastName.Length < _minCharacters || entity.LastName.Length > _maxCharacters)
                result.AddError(Target, Error);

            return result;
        }
    }
}
