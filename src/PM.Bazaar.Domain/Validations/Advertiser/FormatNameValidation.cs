using System.Text.RegularExpressions;
using PM.Bazaar.Domain.Interfaces.Result;
using PM.Bazaar.Domain.Interfaces.Validation;
using PM.Bazaar.Domain.Values;

namespace PM.Bazaar.Domain.Validations.Advertiser
{
    public class FormatNameValidation : Validation<Domain.Entities.Advertiser>
    {
        public FormatNameValidation()
        {
            Target = "Name";
            Error = "O nome deve conter somente letras";
        }

        public override IResult IsValid(Entities.Advertiser entity)
        {
            var result = new Result();

            if (!Regex.Match(entity.Name, @"^[A-Za-zÀ-ú\s]*$").Success)
                result.AddError(Target, Error);

            return result;
        }
    }
}
