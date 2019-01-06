using AutoMapper;
using PM.Bazaar.Application.ViewModels.Common;
using System.Collections.Generic;
using PM.Bazaar.Domain.Interfaces.Entity;

namespace PM.Bazaar.Application.Extensions
{
    public static class MapperExtension
    {
        public static TModel MapEntityTo<TModel>(this Entity entity) where TModel : ViewModel
        {
            return Mapper.Map<Entity, TModel>(entity);
        }

        public static IEnumerable<TModel> MapEntityTo<TModel>(this IEnumerable<Entity> entity)
            where TModel : ViewModel
        {
            return Mapper.Map<IEnumerable<Entity>, IEnumerable<TModel>>(entity);
        }

        public static TEntity MapModelTo<TEntity>(this ViewModel model) where TEntity : Entity
        {
            return Mapper.Map<ViewModel, TEntity>(model);
        }

        public static IEnumerable<TEntity> MapModelTo<TEntity>(this IEnumerable<ViewModel> model)
            where TEntity : Entity
        {
            return Mapper.Map<IEnumerable<ViewModel>, IEnumerable<TEntity>>(model);
        }
    }
}
