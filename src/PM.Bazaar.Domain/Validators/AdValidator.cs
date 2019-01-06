using PM.Bazaar.Domain.Entities;
using PM.Bazaar.Domain.Validations.Ad;
using PM.Bazaar.Domain.Validators.Common;

namespace PM.Bazaar.Domain.Validators
{
    public sealed class AdValidator : Validator<Ad>
    {
        public AdValidator()
        {
            AddValidation(new MinPriceValidation());
            AddValidation(new LengthDescriptionValidation());
            AddValidation(new LengthTitleValidation());
            AddValidation(new CountPicturesValidation());
        }
    }
}
