using PM.Bazaar.Domain.Validators;
using System;
using System.Collections.Generic;
using PM.Bazaar.Domain.Interfaces.Entity;
using PM.Bazaar.Domain.Interfaces.Result;

namespace PM.Bazaar.Domain.Entities
{
    public class Advertiser : Entity
    {
        public string Name { get; private set; }
        public string LastName { get; private set; }
        public DateTime RegistrationDate { get; private set; }
        public int IdAvatar { get; private set; }
        public virtual Image Avatar { get; private set; }
        public virtual ICollection<Ad> Ads { get; private set; }
        public virtual ICollection<Question> Questions { get; private set; }
        public virtual ICollection<Response> Responses { get; private set; }

        protected Advertiser() { }

        public Advertiser(string name, string lastName, DateTime registrationDate, Image picture) : this(name, lastName, registrationDate, picture.Id)
        {
            Avatar = picture;
        }

        public Advertiser(int id, string name, string lastName, DateTime registrationDate, Image picture) : this(name, lastName, registrationDate, picture)
        {
            Id = id;
        }

        public Advertiser(string name, string lastName, DateTime registrationDate, int idAvatar)
        {
            Name = name;
            LastName = lastName;
            RegistrationDate = registrationDate;

            IdAvatar = idAvatar;

            Ads = new HashSet<Ad>();
            Questions = new HashSet<Question>();
            Responses = new HashSet<Response>();
        }

        public Advertiser(int id, string name, string lastName, DateTime registrationDate, int idAvatar) : this(name, lastName, registrationDate, idAvatar)
        {
            Id = id;
        }

        public IResult ChangeAvatar(Image avatar)
        {
            var result = avatar.IsValid();

            if (result.Sucess)
                Avatar = avatar;

            return result;
        }

        public void ChangeName(string newName)
        {
            Name = newName;
        }

        public void ChangeLastName(string newLastName)
        {
            LastName = newLastName;
        }


        public override IResult IsValid()
        {
            var validator = new AdvertiserValidator();

            return validator.Validate(this);
        }
    }
}
