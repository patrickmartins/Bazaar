using PM.Bazaar.Domain.Entities;
using PM.Bazaar.Domain.Validations.Advertiser;
using PM.Bazaar.Domain.Validators.Common;

namespace PM.Bazaar.Domain.Validators
{
    public sealed class AdvertiserValidator : Validator<Advertiser>
    {
        public AdvertiserValidator()
        {
            AddValidation(new FormatNameValidation());
            AddValidation(new LengthNameValidation());
            AddValidation(new FormatLastNameValidation());
            AddValidation(new LengthLastNameValidation());
            AddValidation(new PictureRequiredValidation());
        }
    }
}
