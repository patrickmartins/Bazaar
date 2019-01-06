using Microsoft.AspNet.Identity;
using PM.Bazaar.Domain.Entities;
using PM.Bazaar.Infrastructure.CrossCutting.Identity.Validators;
using System.Collections.Generic;
using PM.Bazaar.Domain.Interfaces.Entity;
using PM.Bazaar.Domain.Interfaces.Result;
using PM.Bazaar.Domain.Values;

namespace PM.Bazaar.Infrastructure.CrossCutting.Identity.Entities
{
    public class Account : Entity, IUser<int>
    {
        protected Account() { }
        public Account(string email, string password, Advertiser advertiser)
        {
            Email = email;
            Advertiser = advertiser;
            Password = password;

            Id = advertiser.Id;

            Claims = new HashSet<AccountClaim>();
            Logins = new HashSet<AccountLogin>();
            Roles = new HashSet<AccountRole>();
        }

        public string UserName { get; set; }
        public string Email
        {
            get
            {
                return UserName;
            }
            private set
            {
                UserName = value;
            }
        }
        public string Password { get; private set; }
        public string SecurityStamp { get; private set; }
        public virtual Advertiser Advertiser { get; private set; }
        public virtual ICollection<AccountClaim> Claims { get; private set; }
        public virtual ICollection<AccountLogin> Logins { get; private set; }
        public virtual ICollection<AccountRole> Roles { get; private set; }

        public void UpdateSecurityStamp(string stamp)
        {
            SecurityStamp = stamp;
        }

        public void ChangePassword(string password)
        {
            Password = password;
        }

        public void UpdateEmail(string email)
        {
            Email = email;
        }

        public override IResult IsValid()
        {
            var validator = new AccountValidator();

            var result = (Result)validator.Validate(this);

            result.AddErrors(Advertiser.IsValid().Errors);

            return result;
        }
    }
}
