using System.Collections.Generic;
using PM.Bazaar.Domain.Values;

namespace PM.Bazaar.Domain.Interfaces.Result
{
    public interface IResult
    {
        IList<Error> Errors { get; }
        bool Sucess { get; }
    }

    public interface IResult<TResult> : IResult
    {
        TResult Value { get; set; }
    }
}
