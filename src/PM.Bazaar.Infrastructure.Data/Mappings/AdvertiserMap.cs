using PM.Bazaar.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace PM.Bazaar.Infrastructure.Data.Mappings
{
    public class AdvertiserMap : EntityTypeConfiguration<Advertiser>
    {
        public AdvertiserMap()
        {
            HasKey(c => c.Id);

            Property(c => c.Name).IsRequired().HasMaxLength(15);
            Property(c => c.LastName).IsRequired().HasMaxLength(30);

            Property(c => c.RegistrationDate).IsRequired();

            HasMany(c => c.Ads)
                .WithRequired(c => c.Advertiser)
                .HasForeignKey(c => c.IdAdvertiser);

            HasRequired(c => c.Avatar)
                .WithMany()
                .HasForeignKey(c => c.IdAvatar)
                .WillCascadeOnDelete(false);
        }
    }
}
