using System.Collections.Generic;
using System.Linq;
using PM.Bazaar.Domain.Interfaces.Result;

namespace PM.Bazaar.Domain.Values
{
    public class Result : IResult
    {
        public IList<Error> Errors { get; set; }

        public Result(params Error[] errors)
        {
            Errors = errors;
        }

        public Result()
        {
            Errors = new List<Error>();
        }

        public void AddError(string source, string description)
        {
            Errors.Add(new Error(description, source));
        }

        public void AddError(Error error)
        {
            Errors.Add(error);
        }

        public void AddErrors(IList<Error> errors)
        {
            Errors = Errors.Concat(errors).ToList();
        }

        public bool Sucess
        {
            get { return !Errors.Any(); }
        }
    }

    public class Result<TResult> : Result, IResult<TResult>
    {
        public TResult Value { get; set; }

        public Result(TResult value, params Error[] errors) : base(errors)
        {
            Value = value;
        }

        public Result(params Error[] errors) : base(errors) { }

        public Result(TResult value)
        {
            Value = value;
        }

        public Result()
        { }

        public void SetValue(TResult value)
        {
            Value = value;
        }
    }
}