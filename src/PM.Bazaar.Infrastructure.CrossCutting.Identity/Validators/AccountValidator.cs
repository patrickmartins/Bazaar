using PM.Bazaar.Domain.Validators.Common;
using PM.Bazaar.Infrastructure.CrossCutting.Identity.Entities;
using PM.Bazaar.Infrastructure.CrossCutting.Identity.Validations;

namespace PM.Bazaar.Infrastructure.CrossCutting.Identity.Validators
{
    public sealed class AccountValidator : Validator<Account>
    {
        public AccountValidator()
        {            
            AddValidation(new EmailFormatValidation());
            AddValidation(new EmailIsRequiredValidation());
            AddValidation(new EmailLengthValidation());
            AddValidation(new PasswordLengthValidation());
        }
    }
}
