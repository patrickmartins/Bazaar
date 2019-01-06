using PM.Bazaar.Domain.Interfaces.Entity;
using PM.Bazaar.Domain.Interfaces.Result;
using PM.Bazaar.Domain.Values;

namespace PM.Bazaar.Domain.Entities
{
    public class Category : Entity
    {
        protected Category() { }
        public Category(string name)
        {
            Name = name;
        }
        public Category(int id, string name) : this(name)
        {
            Id = id;
        }

        public string Name { get; private set; }

        public override IResult IsValid()
        {
            return new Result();
        }
    }
}
