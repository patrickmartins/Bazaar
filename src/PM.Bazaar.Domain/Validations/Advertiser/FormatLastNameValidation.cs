using System.Text.RegularExpressions;
using PM.Bazaar.Domain.Interfaces.Result;
using PM.Bazaar.Domain.Interfaces.Validation;
using PM.Bazaar.Domain.Values;

namespace PM.Bazaar.Domain.Validations.Advertiser
{
    public class FormatLastNameValidation : Validation<Domain.Entities.Advertiser>
    {
        public FormatLastNameValidation()
        {
            Target = "LastName";
            Error = "O sobrenome deve conter somente letras";
        }

        public override IResult IsValid(Entities.Advertiser entity)
        {
            var result = new Result();

            if (!Regex.Match(entity.LastName ?? string.Empty, @"^[A-Za-zÀ-ú\s]*$").Success)
                result.AddError(Target, Error);

            return result;
        }
    }
}
