using PM.Bazaar.Domain.Interfaces.Result;
using PM.Bazaar.Domain.Interfaces.Validation;
using PM.Bazaar.Domain.Values;

namespace PM.Bazaar.Domain.Validations.Picture
{
    public class PictureEmptyValidation : Validation<Entities.Image>
    {
        public PictureEmptyValidation()
        {
            Target = "Bytes";
            Error = "A imagem não deve ser vazia";
        }

        public override IResult IsValid(Entities.Image entity)
        {
            var result = new Result();

            if (entity.Bytes.Length <= 0)
                result.AddError(Target, Error);

            return result;
        }
    }
}
