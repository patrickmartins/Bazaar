using PM.Bazaar.Domain.Interfaces.Result;

namespace PM.Bazaar.Domain.Interfaces.Validation
{
    public interface IValidator<TEntity> where TEntity : Entity.Entity
    {
        IResult Validate(TEntity entity);
    }
}
