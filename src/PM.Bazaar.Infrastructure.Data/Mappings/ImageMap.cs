using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using PM.Bazaar.Domain.Entities;

namespace PM.Bazaar.Infrastructure.Data.Mappings
{
    public class ImageMap : EntityTypeConfiguration<Image>
    {
        public ImageMap()
        {
            HasKey(c => c.Id);

            Property(c => c.Id)
                .IsRequired()
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(c => c.Hash).IsRequired();
            Property(c => c.Bytes).IsRequired();
        }
    }
}
