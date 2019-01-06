using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Text.RegularExpressions;

namespace PM.Bazaar.Infrastructure.Data.Contexts.Conventions
{
    public class UnderscoreAndSeparateTableNameConvention : IStoreModelConvention<EntityType>
    {
        public void Apply(EntityType item, DbModel model)
        {
            var entitySet = model.StoreModel.Container.GetEntitySetByName(item.Name, true);

            entitySet.Table = Regex.Replace(entitySet.Table, ".[A-Z]", m => m.Value[0] + "_" + m.Value[1]).ToLower();
        }
    }
}
