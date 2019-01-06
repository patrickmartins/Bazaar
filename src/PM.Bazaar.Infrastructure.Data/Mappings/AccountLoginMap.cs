using PM.Bazaar.Infrastructure.CrossCutting.Identity.Entities;
using System.Data.Entity.ModelConfiguration;

namespace PM.Bazaar.Infrastructure.Data.Mappings
{
    public class AccountLoginMap : EntityTypeConfiguration<AccountLogin>
    {
        public AccountLoginMap()
        {
            HasKey(u => new { u.UserId, u.LoginProvider, u.ProviderKey });
        }
    }
}
