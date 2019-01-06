using PM.Bazaar.Domain.Interfaces.Result;
using PM.Bazaar.Domain.Interfaces.Validation;
using PM.Bazaar.Domain.Values;
using PM.Bazaar.Infrastructure.CrossCutting.Identity.Entities;

namespace PM.Bazaar.Infrastructure.CrossCutting.Identity.Validations
{
    public class EmailIsRequiredValidation : Validation<Account>
    {
        public EmailIsRequiredValidation()
        {
            Target = "Email";
            Error = "O e-mail não foi informado";
        }

        public override IResult IsValid(Account entity)
        {
            var result = new Result();

            if (string.IsNullOrEmpty(entity.Email))
                result.AddError(Target, Error);

            return result;
        }
    }
}
