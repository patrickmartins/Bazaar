using PM.Bazaar.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace PM.Bazaar.Infrastructure.Data.Mappings
{
    public class AdMap : EntityTypeConfiguration<Ad>
    {
        public AdMap()
        {
            HasKey(c => c.Id);

            Property(c => c.Id)
                .IsRequired()
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(c => c.Description).IsRequired().HasMaxLength(5000);
            Property(c => c.Price).IsRequired();
            Property(c => c.Date).IsRequired();

            HasRequired(c => c.Category)
                .WithMany()
                .HasForeignKey(c => c.IdCategory);

            HasMany(c => c.Pictures)
                .WithMany()
                .Map(c => 
                {
                    c.MapLeftKey("id_ad");
                    c.MapRightKey("id_images");
                    c.ToTable("ads_images");
                });
        }
    }
}
