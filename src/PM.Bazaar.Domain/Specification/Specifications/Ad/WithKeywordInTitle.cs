namespace PM.Bazaar.Domain.Specification.Specifications.Ad
{
    public class WithKeywordInTitle : SpecificationQuery<Entities.Ad>
    {
        public WithKeywordInTitle(string keyword)
        {
            Expression = c => c.Title.Contains(keyword);
        }
    }
}
