using PM.Bazaar.Domain.Entities;
using PM.Bazaar.Domain.Validations.Picture;
using PM.Bazaar.Domain.Validators.Common;

namespace PM.Bazaar.Domain.Validators
{
    public sealed class PictureValidator : Validator<Image>
    {
        public PictureValidator()
        {
            AddValidation(new PictureEmptyValidation());
        }
    }
}
