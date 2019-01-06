using PM.Bazaar.Domain.Interfaces.Result;
using PM.Bazaar.Domain.Interfaces.Validation;
using PM.Bazaar.Domain.Values;

namespace PM.Bazaar.Domain.Validations.Advertiser
{
    public class PictureRequiredValidation : Validation<Entities.Advertiser>
    {
        public PictureRequiredValidation()
        {
            Target = "Picture";
            Error = "A foto de perfil deve ser informada";
        }

        public override IResult IsValid(Entities.Advertiser entity)
        {
            var result = new Result();

            if (entity.Avatar == null)
                result.AddError(Target, Error);

            return result;
        }
    }
}
