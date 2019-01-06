namespace PM.Bazaar.Domain.Specification.Specifications.Ad
{
    public class MaxPrice : SpecificationQuery<Entities.Ad>
    {
        public MaxPrice(double price)
        {
            Expression = c => c.Price <= price;
        }
    }
}
