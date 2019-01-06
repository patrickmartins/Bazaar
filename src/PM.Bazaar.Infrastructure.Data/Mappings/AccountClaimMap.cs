using PM.Bazaar.Infrastructure.CrossCutting.Identity.Entities;
using System.Data.Entity.ModelConfiguration;

namespace PM.Bazaar.Infrastructure.Data.Mappings
{
    public class AccountClaimMap : EntityTypeConfiguration<AccountClaim>
    {
        public AccountClaimMap()
        {
            HasKey(u => u.Id);
        }
    }
}
