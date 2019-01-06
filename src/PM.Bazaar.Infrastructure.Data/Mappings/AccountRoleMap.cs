using PM.Bazaar.Infrastructure.CrossCutting.Identity.Entities;
using System.Data.Entity.ModelConfiguration;

namespace PM.Bazaar.Infrastructure.Data.Mappings
{
    public class AccountRoleMap : EntityTypeConfiguration<AccountRole>
    {
        public AccountRoleMap()
        {
            HasKey(u => new { u.RoleId, u.UserId });
        }
    }
}
