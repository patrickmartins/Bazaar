using PM.Bazaar.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace PM.Bazaar.Infrastructure.Data.Mappings
{
    public class QuestionMap : EntityTypeConfiguration<Question>
    {
        public QuestionMap()
        {
            HasKey(c => c.Id);

            Property(c => c.Id)
                .IsRequired()
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(c => c.Description).IsRequired().HasMaxLength(2000);
            Property(c => c.Date).IsRequired();

            HasRequired(c => c.Ad)
                .WithMany(c => c.Questions)
                .HasForeignKey(c => c.IdAd);

            HasRequired(c => c.User)
                .WithMany(c => c.Questions)
                .HasForeignKey(c => c.IdUser)
                .WillCascadeOnDelete(false);
        }
    }
}
