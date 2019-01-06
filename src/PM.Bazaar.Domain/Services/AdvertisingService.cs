using PM.Bazaar.Domain.Entities;
using PM.Bazaar.Domain.Interfaces.Repositories;
using PM.Bazaar.Domain.Interfaces.Result;
using PM.Bazaar.Domain.Interfaces.Services;
using PM.Bazaar.Domain.Services.Common;
using PM.Bazaar.Domain.Validations.Ad;

namespace PM.Bazaar.Domain.Services
{
    public class AdvertisingService : Service<Ad>, IAdvertisingService
    {
        private readonly ICategoryRepository _categoryRepository;

        public AdvertisingService(IAdRepository adRepository, ICategoryRepository categoryRepository) : base(adRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public IResult PublishAd(Ad item)
        {
            var result = new CategoryExistValidation(_categoryRepository).IsValid(item);
            
            if(result.Sucess)
                result = Insert(item);

            return result;
        }

        public IResult RemoveAd(Ad item)
        {
            return Remove(item);
        }

        public IResult UpdateAd(Ad item)
        {
            return Update(item);
        }

        public IResult Ask(int idAd, Question question)
        {
            var result = GetById(idAd);

            if (result.Sucess)
            {
                var responseResult = result.Value.Ask(question);

                if (!responseResult.Sucess)
                    return responseResult;

                return Update(result.Value);
            }

            return result;
        }

        public IResult Answer(int idAd, Response response)
        {
            var result = GetById(idAd);

            if (result.Sucess)
            {
                var responseResult = result.Value.Answer(response);

                if (!responseResult.Sucess)
                    return responseResult;

                return Update(result.Value);
            }

            return result;
        }
    }
}
