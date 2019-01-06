using PM.Bazaar.Infrastructure.Data.Contexts.Conventions;
using PM.Bazaar.Infrastructure.Data.Mappings;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Diagnostics;

namespace PM.Bazaar.Infrastructure.Data.Contexts
{
    public class BazaarContext : DbContext
    {
        public BazaarContext()
            : base("BazaarConnection")
        {
            Database.Log = c => Debug.WriteLine(c);
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Add(new UnderscoreAndSeparateTableNameConvention());
            modelBuilder.Conventions.Add(new UnderscoreAndSeparatePropertyNameConvention());
            modelBuilder.Conventions.Add(new PluralizingTableNameConvention());
            
            modelBuilder.Properties<string>()
                .Configure(p => p.HasColumnType("varchar"));

            modelBuilder.Properties<string>()
                .Configure(p => p.HasMaxLength(100));

            modelBuilder.Configurations.Add(new AdMap());
            modelBuilder.Configurations.Add(new AdvertiserMap());
            modelBuilder.Configurations.Add(new ImageMap());
            modelBuilder.Configurations.Add(new QuestionMap());
            modelBuilder.Configurations.Add(new ResponseMap());
            modelBuilder.Configurations.Add(new CategoryMap());

            modelBuilder.Configurations.Add(new AccountMap());
            modelBuilder.Configurations.Add(new AccountClaimMap());
            modelBuilder.Configurations.Add(new AccountLoginMap());
            modelBuilder.Configurations.Add(new AccountRoleMap());
            modelBuilder.Configurations.Add(new RoleMap());

            base.OnModelCreating(modelBuilder);
        }
    }
}
