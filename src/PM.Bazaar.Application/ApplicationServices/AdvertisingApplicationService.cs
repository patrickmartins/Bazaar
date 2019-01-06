using PM.Bazaar.Application.ApplicationServices.Common;
using PM.Bazaar.Application.Extensions;
using PM.Bazaar.Application.Interfaces;
using PM.Bazaar.Application.ViewModels;
using PM.Bazaar.Domain.Entities;
using PM.Bazaar.Domain.Enuns;
using PM.Bazaar.Domain.Interfaces.Result;
using PM.Bazaar.Domain.Interfaces.Services;
using PM.Bazaar.Domain.Interfaces.UnitOfWork;
using PM.Bazaar.Domain.Values;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace PM.Bazaar.Application.ApplicationServices
{
    public class AdvertisingApplicationService : ApplicationService, IAdvertisingApplicationService
    {
        private readonly IAdvertisingService _adService;
        private readonly IImageService _imageService;

        public AdvertisingApplicationService(IAdvertisingService adService, IImageService imageService, IUoW uow) : base(uow)
        {
            _adService = adService;
            _imageService = imageService;
        }

        public IEnumerable<AdViewModel> Search(SearchAdViewModel filter)
        {
            var specification = SpecificationAdQueryBuilder.MinPrice(filter.MinPrice);

            specification = filter.MaxPrice > 0 ? specification.MaxPrice(filter.MaxPrice) : specification;
            specification = !string.IsNullOrEmpty(filter.KeywordSearch) ? specification.WithKeywordInTitle(filter.KeywordSearch) : specification;
            specification = filter.Categories.Length > 0 ? specification.WithCategory(filter.Categories) : specification;
            
            var orderType = filter.Order == SearchAdViewModel.OrderBy.MaxPrice  ? OrderType.Descending : OrderType.Ascending;

            Expression<Func<Ad, double>> orderByPrice = c => c.Price;
            Expression<Func<Ad, DateTime>> orderByDate = c => c.Date;

            if (filter.Order != SearchAdViewModel.OrderBy.Publish)
                return _adService.Search(
                    specification,
                    filter.Page > 0 ? filter.Page : 1,
                    filter.PageSize > 0 ? filter.PageSize : 20,
                    orderType,
                    orderByPrice
                ).MapEntityTo<AdViewModel>();
            else
                return _adService.Search(
                    specification,
                    filter.Page > 0 ? filter.Page : 1,
                    filter.PageSize > 0 ? filter.PageSize : 20,
                    orderType,
                    orderByDate
                ).MapEntityTo<AdViewModel>();
        }

        public IEnumerable<AdViewModel> All(int initialId, int count)
        {
            return _adService.All(initialId, count).MapEntityTo<AdViewModel>();
        }

        public IResult<DetailedAdViewModel> GetById(int id)
        {
            var result = new Result<DetailedAdViewModel>();

            var ad = _adService.GetById(id);

            if (ad.Sucess)
                result.SetValue(ad.Value.MapEntityTo<DetailedAdViewModel>());
            else
                result.Errors = ad.Errors;

            return result;
        }

        public IResult PublishAd(RegisterAdViewModel item)
        {
            BeginTransaction();

            var ad = item.MapModelTo<Ad>();

            var result = LinkImageToAd(item.Pictures, ad);

            if (!result.Sucess)
                return result;

            result = _adService.PublishAd(ad);

            if (result.Sucess)
                Commit();

            return result;
        }

        public IResult RemoveAd(RegisterAdViewModel item)
        {
            BeginTransaction();

            var result = _adService.RemoveAd(item.MapModelTo<Ad>());

            if (result.Sucess)
                Commit();

            return result;
        }

        public IResult UpdateAd(RegisterAdViewModel item)
        {
            BeginTransaction();

            var result = _adService.UpdateAd(item.MapModelTo<Ad>());

            if (result.Sucess)
                Commit();

            return result;
        }

        public IResult Ask(RegisterQuestionViewModel question)
        {
            BeginTransaction();

            var result = _adService.Ask(question.IdAd, question.MapModelTo<Question>());

            if (result.Sucess)
                Commit();

            return result;
        }

        public IResult Answer(RegisterResponseViewModel response)
        {
            BeginTransaction();

            var result = _adService.Answer(response.IdAd, response.MapModelTo<Response>());

            if (result.Sucess)
                Commit();

            return result;
        }

        private IResult LinkImageToAd(Guid[] images, Ad ad)
        {
            if (images == null)
                return new Result();

            foreach (var hash in images)
            {
                var result = _imageService.GetByHash(hash);

                if (!result.Sucess)
                    return result;

                ad.AddPicture(result.Value);
            }

            return new Result();
        }
    }
}
