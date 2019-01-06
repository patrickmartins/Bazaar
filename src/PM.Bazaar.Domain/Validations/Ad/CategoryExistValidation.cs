using PM.Bazaar.Domain.Interfaces.Repositories;
using PM.Bazaar.Domain.Interfaces.Result;
using PM.Bazaar.Domain.Interfaces.Validation;
using PM.Bazaar.Domain.Values;

namespace PM.Bazaar.Domain.Validations.Ad
{
    public class CategoryExistValidation : Validation<Entities.Ad>
    {
        private readonly ICategoryRepository _repository;

        public CategoryExistValidation(ICategoryRepository repository)
        {
            Target = "Category";
            Error = "A categoria informada não é válida";

            _repository = repository;
        }

        public override IResult IsValid(Entities.Ad entity)
        {
            var result = new Result();

            if (_repository.GetById(entity.IdCategory) == null)
                result.AddError(Target, Error);

            return result;
        }
    }
}
