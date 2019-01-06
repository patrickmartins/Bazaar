using System;
using PM.Bazaar.Domain.Validators;
using System.Collections.Generic;
using System.Linq;
using PM.Bazaar.Domain.Interfaces.Entity;
using PM.Bazaar.Domain.Interfaces.Result;
using PM.Bazaar.Domain.Values;

namespace PM.Bazaar.Domain.Entities
{
    public class Ad : Entity
    {
        public string Title { get; private set; }
        public string Description { get; private set; }
        public double Price { get; private set; }
        public DateTime Date { get; private set; }

        public virtual ICollection<Image> Pictures { get; private set; }
        public virtual ICollection<Question> Questions { get; private set; }
        public virtual Category Category { get; private set; }
        public virtual int IdCategory { get; private set; }

        public virtual Advertiser Advertiser { get; private set; }
        public virtual int IdAdvertiser { get; private set; }

        protected Ad() { }

        public Ad(string title, string description, DateTime date, int idCategory, double price, int idAdvertiser)
        {
            Title = title;
            Description = description;
            Date = date;
            Price = price;

            Pictures = new HashSet<Image>();
            Questions = new HashSet<Question>();

            IdCategory = idCategory;
            IdAdvertiser = idAdvertiser;
        }

        public Ad(string title, string description, DateTime date, Category category, double price, Advertiser advertiser) : this(title, description, date, category.Id, price, advertiser.Id)
        {
            Category = category;
            Advertiser = advertiser;
        }

        public Ad(int id, string title, string description, DateTime date, Category category, double price, Advertiser advertiser) : this(title, description, date, category, price, advertiser)
        {
            Id = id;
        }

        public override IResult IsValid()
        {
            var validator = new AdValidator();

            return validator.Validate(this);
        }

        public IResult AddPicture(Image picture)
        {
            var result = picture.IsValid();

            if (result.Sucess)
                Pictures.Add(picture);

            return result;
        }

        public IResult Ask(Question question)
        {
            var result = question.IsValid();

            if (question.IdUser == IdAdvertiser)
                return new Result(new Error("O anunciante não pode fazer uma pergunta em seu próprio anúncio", "IdAdvertiser"));

            if (result.Sucess)
                Questions.Add(question);

            return result;
        }

        public IResult Answer(Response response)
        {
            var question = Questions.FirstOrDefault(c => c.Id == response.Id);

            if (question == null)
                return new Result(new Error("A pergunta para essa resposta não existe", "IdQuestion"));

            if (response.IdAdvertiser != IdAdvertiser)
                return new Result(new Error("Somente o anunciante pode responder uma pergunta", "IdAdvertiser"));

            return question.Answer(response);
        }

        public IResult ChangeCategory(Category category)
        {
            var result = category.IsValid();

            if (result.Sucess)
                Category = category;

            return result;
        }

        public void UpdateTitle(string title)
        {
            Title = title;
        }

        public void UpdateDescription(string description)
        {
            Description = description;
        }

        public void UpdateDate(DateTime date)
        {
            Date = date;
        }

        public void UpdatePrice(double price)
        {
            Price = price;
        }
    }
}
