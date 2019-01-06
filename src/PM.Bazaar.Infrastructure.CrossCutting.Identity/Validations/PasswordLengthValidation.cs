using PM.Bazaar.Domain.Interfaces.Result;
using PM.Bazaar.Domain.Interfaces.Validation;
using PM.Bazaar.Domain.Values;
using PM.Bazaar.Infrastructure.CrossCutting.Configuration;
using PM.Bazaar.Infrastructure.CrossCutting.Identity.Entities;

namespace PM.Bazaar.Infrastructure.CrossCutting.Identity.Validations
{
    public class PasswordLengthValidation : Validation<Account>
    {
        private readonly int _minCharacters = int.Parse(Configs.MinCharactersPassword);
        private readonly int _maxCharacters = int.Parse(Configs.MaxCharactersPassword);

        public PasswordLengthValidation()
        {
            Target = "Password";
            Error = $"A senha deve conter no mínimo {_minCharacters} e no máximo {_maxCharacters} caracteres";
        }

        public override IResult IsValid(Account entity)
        {
            var result = new Result();

            if (entity.Id == 0 && (entity.Password.Length < _minCharacters || entity.Password.Length > _maxCharacters))
                result.AddError(Target, Error);

            return result;
        }
    }
}
