using AutoMapper;
using PM.Bazaar.Application.ViewModels;
using PM.Bazaar.Domain.Entities;

namespace PM.Bazaar.Application.AutoMapper.Maps
{
    public class ImageToViewModel : Profile
    {
        public ImageToViewModel()
        {
            CreateMap<Image, ImageViewModel>();
            CreateMap<ImageViewModel, Image>();

            CreateMap<RegisterImageViewModel, Image>().ConstructUsing(c => new Image(c.Bytes, c.Hash)).ForAllMembers(c => c.Ignore());
        }
    }
}
