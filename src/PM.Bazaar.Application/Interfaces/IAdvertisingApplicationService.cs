using System.Collections.Generic;
using PM.Bazaar.Application.ViewModels;
using PM.Bazaar.Domain.Interfaces.Result;

namespace PM.Bazaar.Application.Interfaces
{
    public interface IAdvertisingApplicationService
    {
        IEnumerable<AdViewModel> Search(SearchAdViewModel filter);
        IEnumerable<AdViewModel> All(int page, int pageSize);
        IResult<DetailedAdViewModel> GetById(int id);
        IResult PublishAd(RegisterAdViewModel item);
        IResult RemoveAd(RegisterAdViewModel item);
        IResult UpdateAd(RegisterAdViewModel item);
        IResult Ask(RegisterQuestionViewModel question);
        IResult Answer(RegisterResponseViewModel response);
    }
}
