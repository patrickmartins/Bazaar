using System.Collections.Generic;
using PM.Bazaar.Domain.Interfaces.Entity;
using PM.Bazaar.Domain.Interfaces.Result;
using PM.Bazaar.Domain.Interfaces.Validation;
using PM.Bazaar.Domain.Values;

namespace PM.Bazaar.Domain.Validators.Common
{
    public class Validator<TEntity> : IValidator<TEntity> where TEntity : Entity
    {
        private readonly ICollection<Validation<TEntity>> _validations;

        public Validator()
        {
            _validations = new HashSet<Validation<TEntity>>();
        }

        protected virtual void AddValidation(Validation<TEntity> validation)
        {
            _validations.Add(validation);
        }

        protected virtual void RemoveValidation(Validation<TEntity> validation)
        {
            _validations.Remove(validation);
        }

        public IResult Validate(TEntity entity)
        {
            var result = new Result();

            foreach (var validation in _validations)
            {
                var res = validation.IsValid(entity);

                if (!res.Sucess)
                    result.AddErrors(res.Errors);
            }

            return result;
        }
    }
}
