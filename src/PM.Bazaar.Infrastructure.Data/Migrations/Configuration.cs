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
            context.Set<Category>().Add(new Category("Ve�culos"));
            context.Set<Category>().Add(new Category("Casa"));
            context.Set<Category>().Add(new Category("Decora��o"));
            context.Set<Category>().Add(new Category("M�sica"));
            context.Set<Category>().Add(new Category("Esportes"));
            context.Set<Category>().Add(new Category("Eletr�nicos"));
            context.Set<Category>().Add(new Category("Moda"));
            context.Set<Category>().Add(new Category("Livros"));
            context.Set<Category>().Add(new Category("Brinquedos"));
        }
    }
}
