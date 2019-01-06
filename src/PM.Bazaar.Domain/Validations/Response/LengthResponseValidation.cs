using PM.Bazaar.Domain.Interfaces.Result;
using PM.Bazaar.Domain.Interfaces.Validation;
using PM.Bazaar.Domain.Values;
using PM.Bazaar.Infrastructure.CrossCutting.Configuration;

namespace PM.Bazaar.Domain.Validations.Response
{
    public class LengthResponseValidation : Validation<Domain.Entities.Response>
    {
        private int _minCharacters = int.Parse(Configs.MinCharactersQuestion);
        private int _maxCharacters = int.Parse(Configs.MaxCharactersQuestion);

        public LengthResponseValidation()
        {
            Target = "Response";
            Error = $"A resposta deve conter no mínimo {_minCharacters} e no máximo {_maxCharacters} caracteres";
        }

        public override IResult IsValid(Entities.Response entity)
        {
            var result = new Result();

            if (entity.Description.Length < _minCharacters || entity.Description.Length > _maxCharacters)
                result.AddError(Target, Error);

            return result;
        }
    }
}
