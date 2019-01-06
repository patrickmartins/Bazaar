using PM.Bazaar.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace PM.Bazaar.Infrastructure.Data.Mappings
{
    public class ResponseMap : EntityTypeConfiguration<Response>
    {
        public ResponseMap()
        {
            HasKey(c => c.Id);

            Property(c => c.Description).IsRequired().HasMaxLength(2000);
            Property(c => c.Date).IsRequired();

            HasRequired(c => c.Question)
                .WithOptional(c => c.Response);

            HasRequired(c => c.Advertiser)
                .WithMany(c => c.Responses)
                .HasForeignKey(c => c.IdAdvertiser);
        }
    }
}
