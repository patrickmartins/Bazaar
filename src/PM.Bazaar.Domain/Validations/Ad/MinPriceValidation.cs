using PM.Bazaar.Domain.Interfaces.Result;
using PM.Bazaar.Domain.Interfaces.Validation;
using PM.Bazaar.Domain.Values;
using PM.Bazaar.Infrastructure.CrossCutting.Configuration;

namespace PM.Bazaar.Domain.Validations.Ad
{
    public class MinPriceValidation : Validation<Domain.Entities.Ad>
    {
        private double _minPrice = double.Parse(Configs.MinProductPrice);
        private double _maxPrice = double.Parse(Configs.MaxProductPrice);

        public MinPriceValidation()
        {
            Target = "Price";
            Error = $"O preço do item deve ser no mínimo R${_minPrice} e no máximo R${_maxPrice}";
        }

        public override IResult IsValid(Entities.Ad entity)
        {
            var result = new Result();

            if (entity.Price < _minPrice || entity.Price > _maxPrice)
                result.AddError(Target, Error);

            return result;
        }
    }
}
