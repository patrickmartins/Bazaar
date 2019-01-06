using PM.Bazaar.Domain.Entities;
using PM.Bazaar.Domain.Validations.Response;
using PM.Bazaar.Domain.Validators.Common;

namespace PM.Bazaar.Domain.Validators
{
    public sealed class ResponseValidator : Validator<Response>
    {
        public ResponseValidator()
        {
            AddValidation(new LengthResponseValidation());
        }
    }
}
