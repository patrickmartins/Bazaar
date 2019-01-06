using System.Linq;
using PM.Bazaar.Domain.Interfaces.Result;
using PM.Bazaar.Domain.Values;

namespace PM.Bazaar.Services.WebApi.Extensions
{
    public static class ModelStateExtension
    {
        public static IResult ToResult(this System.Web.Http.ModelBinding.ModelStateDictionary dictionary)
        {
            var result = new Result();

            foreach (var state in dictionary)
                if (state.Value.Errors.Count > 0)
                    foreach (var error in state.Value.Errors)
                        result.AddError(state.Key.Split('.').Last(), error.ErrorMessage);

            return result;
        }
    }
}