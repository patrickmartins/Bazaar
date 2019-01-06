using PM.Bazaar.Infrastructure.CrossCutting.Identity.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace PM.Bazaar.Infrastructure.Data.Mappings
{
    public class AccountMap : EntityTypeConfiguration<Account>
    {
        public AccountMap()
        {
            HasKey(c => c.Id);

            Property(c => c.Id)
                .IsRequired()
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(c => c.UserName).IsRequired().HasMaxLength(60);
            Property(c => c.Password).IsRequired().HasMaxLength(100);
            Property(c => c.Email).IsRequired().HasMaxLength(60);

            HasMany(u => u.Claims)
                .WithRequired()
                .HasForeignKey(u => u.UserId);

            HasMany(u => u.Logins)
                .WithRequired()
                .HasForeignKey(u => u.UserId);

            HasMany(u => u.Roles)
                .WithRequired(u => u.User)
                .HasForeignKey(u => u.UserId);

            HasRequired(u => u.Advertiser)
                .WithRequiredPrincipal();
        }
    }    
}
