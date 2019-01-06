using PM.Bazaar.Domain.Interfaces.Result;

namespace PM.Bazaar.Domain.Interfaces.Entity
{
    public abstract class Entity
    {
        public int Id { get; protected set; }
        public abstract IResult IsValid();
    }
}
