using PM.Bazaar.Domain.Entities;
using PM.Bazaar.Domain.Validations.Question;
using PM.Bazaar.Domain.Validators.Common;

namespace PM.Bazaar.Domain.Validators
{
    public sealed class QuestionValidator : Validator<Question>
    {
        public QuestionValidator()
        {
            AddValidation(new LengthQuestionValidation());
        }
    }
}
