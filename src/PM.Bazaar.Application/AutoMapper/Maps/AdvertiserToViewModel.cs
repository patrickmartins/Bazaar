using AutoMapper;
using PM.Bazaar.Application.ViewModels;
using PM.Bazaar.Domain.Entities;

namespace PM.Bazaar.Application.AutoMapper.Maps
{
    public class AdvertiserToViewModel : Profile
    {
        public AdvertiserToViewModel()
        {
            CreateMap<Advertiser, UserViewModel>()
                .ForMember(c => c.Avatar, x => x.MapFrom(c => c.Avatar.Id));

            CreateMap<Advertiser, AccountViewModel>()
                .ForMember(c => c.Avatar, x => x.MapFrom(c => c.Avatar.Id));
        }
    }
}
