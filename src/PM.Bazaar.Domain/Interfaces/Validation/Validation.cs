using PM.Bazaar.Domain.Interfaces.Result;

namespace PM.Bazaar.Domain.Interfaces.Validation
{
    public abstract class Validation<TEntity> where TEntity : Entity.Entity
    {
        public string Target { get; protected set; }
        public string Error { get; protected set; }
        public abstract IResult IsValid(TEntity entity);
    }
}
