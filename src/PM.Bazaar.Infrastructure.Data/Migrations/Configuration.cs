using PM.Bazaar.Domain.Entities;

namespace PM.Bazaar.Infrastructure.Data.Migrations
{
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<Contexts.BazaarContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }
        
        protected override void Seed(Contexts.BazaarContext context)
        {
            context.Set<Category>().Add(new Category("Veículos"));
            context.Set<Category>().Add(new Category("Casa"));
            context.Set<Category>().Add(new Category("Decoração"));
            context.Set<Category>().Add(new Category("Música"));
            context.Set<Category>().Add(new Category("Esportes"));
            context.Set<Category>().Add(new Category("Eletrônicos"));
            context.Set<Category>().Add(new Category("Moda"));
            context.Set<Category>().Add(new Category("Livros"));
            context.Set<Category>().Add(new Category("Brinquedos"));
        }
    }
}
