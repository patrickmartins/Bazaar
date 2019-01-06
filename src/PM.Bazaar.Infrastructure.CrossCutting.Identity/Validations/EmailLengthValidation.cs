using PM.Bazaar.Domain.Interfaces.Result;
using PM.Bazaar.Domain.Interfaces.Validation;
using PM.Bazaar.Domain.Values;
using PM.Bazaar.Infrastructure.CrossCutting.Configuration;
using PM.Bazaar.Infrastructure.CrossCutting.Identity.Entities;

namespace PM.Bazaar.Infrastructure.CrossCutting.Identity.Validations
{
    class EmailLengthValidation : Validation<Account>
    {
        private readonly int _minCharacters = int.Parse(Configs.MinCharactersEmail);
        private readonly int _maxCharacters = int.Parse(Configs.MaxCharactersEmail);

        public EmailLengthValidation()
        {
            Target = "Email";
            Error = $"O email deve conter no mínimo {_minCharacters} e no máximo {_maxCharacters} caracteres";
        }

        public override IResult IsValid(Account entity)
        {
            var result = new Result();

            if (entity.Email.Length < _minCharacters || entity.Email.Length > _maxCharacters)
                result.AddError(Target, Error);

            return result;
        }
    }
}
