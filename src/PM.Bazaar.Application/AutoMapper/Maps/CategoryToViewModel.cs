using AutoMapper;
using PM.Bazaar.Application.ViewModels;
using PM.Bazaar.Domain.Entities;

namespace PM.Bazaar.Application.AutoMapper.Maps
{
    public class CategoryToViewModel : Profile
    {
        public CategoryToViewModel()
        {
            CreateMap<Category, CategoryViewModel>();
        }
    }
}
