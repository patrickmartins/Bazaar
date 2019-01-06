using PM.Bazaar.Domain.Interfaces.Result;
using PM.Bazaar.Domain.Interfaces.Validation;
using PM.Bazaar.Domain.Values;
using PM.Bazaar.Infrastructure.CrossCutting.Identity.Entities;
using PM.Bazaar.Infrastructure.CrossCutting.Identity.Interfaces.Repositories;

namespace PM.Bazaar.Infrastructure.CrossCutting.Identity.Validations
{
    public class EmailAlreadyUsedValidation : Validation<Account>
    {
        private readonly IAccountRepository _repository;

        public EmailAlreadyUsedValidation(IAccountRepository repository)
        {
            _repository = repository;

            Target = "Email";
            Error = "O e-mail informado já está sendo usado por outra conta";
        }

        public override IResult IsValid(Account entity)
        {
            var result = new Result();
            var account = _repository.FindByEmailAsync(entity.Email).Result;

            if (account != null && account.Id != entity.Id)
                result.AddError(Target, Error);

            return result;
        }
    }
}
