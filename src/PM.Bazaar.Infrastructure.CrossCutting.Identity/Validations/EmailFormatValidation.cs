using PM.Bazaar.Infrastructure.CrossCutting.Identity.Entities;
using System.Text.RegularExpressions;
using PM.Bazaar.Domain.Interfaces.Result;
using PM.Bazaar.Domain.Interfaces.Validation;
using PM.Bazaar.Domain.Values;

namespace PM.Bazaar.Infrastructure.CrossCutting.Identity.Validations
{
    public class EmailFormatValidation : Validation<Account>
    {
        public EmailFormatValidation()
        {
            Target = "Email";
            Error = "O e-mail está em um formato incorreto";
        }

        public override IResult IsValid(Account entity)
        {
            var result = new Result();

            if (!Regex.Match(entity.Email, @"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$").Success)
                result.AddError(Target, Error);

            return result;
        }
    }
}
