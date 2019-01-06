using PM.Bazaar.Domain.Validators;
using System;
using PM.Bazaar.Domain.Interfaces.Entity;
using PM.Bazaar.Domain.Interfaces.Result;

namespace PM.Bazaar.Domain.Entities
{
    public class Response : Entity
    {
        public string Description { get; private set; }
        public DateTime Date { get; private set; }

        public virtual Question Question { get; private set; }
        public virtual Advertiser Advertiser { get; private set; }
        public virtual int IdAdvertiser { get; private set; }

        protected Response() { }

        public Response(int id, string description, DateTime date, int idAdvertiser)
        {
            Id = id;
            Description = description;
            Date = date;

            IdAdvertiser = idAdvertiser;
        }

        public Response(int id, string description, DateTime date, Advertiser advertiser) : this(id, description, date, advertiser.Id)
        {
            Advertiser = advertiser;
        }

        public void UpdateDescription(string description)
        {
            Description = description;
        }

        public void UpdateDate(DateTime date)
        {
            Date = date;
        }

        public override IResult IsValid()
        {
            var validator = new ResponseValidator();

            return validator.Validate(this);
        }
    }
}
