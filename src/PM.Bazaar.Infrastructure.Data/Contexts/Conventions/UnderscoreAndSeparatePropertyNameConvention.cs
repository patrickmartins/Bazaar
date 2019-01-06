using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text.RegularExpressions;

namespace PM.Bazaar.Infrastructure.Data.Contexts.Conventions
{
    public class UnderscoreAndSeparatePropertyNameConvention : IStoreModelConvention<EntityType>
    {
        public void Apply(EntityType item, DbModel model)
        {
            var entitySets = model.StoreModel.Container.EntitySets.SingleOrDefault(c => c.ElementType.FullName.Equals(item.FullName))?.ElementType.Properties;

            if (entitySets != null)
                foreach (var set in entitySets)
                    set.Name = Regex.Replace(set.Name, ".[A-Z]", m => m.Value[0] + "_" + m.Value[1]).ToLower();
        }
    }    
}
