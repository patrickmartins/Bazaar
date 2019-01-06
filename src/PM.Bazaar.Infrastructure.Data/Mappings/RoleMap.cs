using PM.Bazaar.Infrastructure.CrossCutting.Identity.Entities;
using System.Data.Entity.ModelConfiguration;

namespace PM.Bazaar.Infrastructure.Data.Mappings
{
    public class RoleMap : EntityTypeConfiguration<Role>
    {
        public RoleMap()
        {
            HasKey(u => u.Id);

            HasMany(u => u.Users)
                .WithRequired(u => u.Role).HasForeignKey(u => u.RoleId);
        }
    }
}
