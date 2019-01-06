namespace PM.Bazaar.Domain.Specification.Specifications.Ad
{
    public class MinPrice : SpecificationQuery<Entities.Ad>
    {
        public MinPrice(double price)
        {
            Expression = c => c.Price >= price;
        }
    }
}
