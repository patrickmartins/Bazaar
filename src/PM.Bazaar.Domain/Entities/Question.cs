using PM.Bazaar.Domain.Validators;
using System;
using PM.Bazaar.Domain.Interfaces.Entity;
using PM.Bazaar.Domain.Interfaces.Result;

namespace PM.Bazaar.Domain.Entities
{
    public class Question : Entity
    {
        public string Description { get; private set; }
        public DateTime Date { get; private set; }
        public virtual Advertiser User { get; private set; }
        public int IdUser { get; private set; }
        public virtual Ad Ad { get; private set; }
        public int IdAd { get; private set; }
        public virtual Response Response { get; private set; }

        protected Question() { }

        public Question(string description, DateTime date, int idAdvertiser, int idAd)
        {
            Description = description;
            Date = date;
            IdUser = idAdvertiser;
            IdAd = idAd;
        }

        public Question(int id, string description, DateTime date, int idAdvertiser, int idAd) : this(description, date, idAdvertiser, idAd)
        {
            Id = id;
        }

        public Question(string description, DateTime date, Advertiser advertiser, Ad ad) : this(description, date, advertiser.Id, ad.Id)
        {
            User = advertiser;
            Ad = ad;
        }

        public Question(int id, string description, DateTime date, Advertiser advertiser, Ad ad) : this(description, date, advertiser, ad)
        {
            Id = id;
        }

        public IResult Answer(Response response)
        {
            var result = response.IsValid();

            if (result.Sucess)
                Response = response;

            return result;
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
            var validator = new QuestionValidator();

            return validator.Validate(this);
        }
    }
}
