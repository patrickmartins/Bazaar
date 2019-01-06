using PM.Bazaar.Domain.Entities;
using PM.Bazaar.Domain.Enuns;
using PM.Bazaar.Domain.Interfaces.Result;
using PM.Bazaar.Domain.Interfaces.Specification;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace PM.Bazaar.Domain.Interfaces.Services
{
    public interface IAdvertisingService
    {
        IEnumerable<Ad> Search<TKey>(ISpecificationQuery<Ad> specification, int page, int pageSize, OrderType order, Expression<Func<Ad, TKey>> orderBy);
        IEnumerable<Ad> All(int page, int pageSize);
        IResult<Ad> GetById(int id);
        IResult PublishAd(Ad item);
        IResult RemoveAd(Ad item);
        IResult UpdateAd(Ad item);
        IResult Ask(int idAd, Question question);
        IResult Answer(int idAd, Response response);
    }
}
