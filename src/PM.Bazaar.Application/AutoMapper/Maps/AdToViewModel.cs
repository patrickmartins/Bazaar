using AutoMapper;
using PM.Bazaar.Application.ViewModels;
using PM.Bazaar.Domain.Entities;
using System;
using System.Linq;

namespace PM.Bazaar.Application.AutoMapper.Maps
{
    public class AdToViewModel : Profile
    {
        public AdToViewModel()
        {
            CreateMap<Ad, DetailedAdViewModel>()
                .ForMember(c => c.Pictures, x => x.MapFrom(c => c.Pictures.Select(y => y.Id)));

            CreateMap<Ad, AdViewModel>()
                .ForMember(c => c.Picture, x => x.MapFrom(c => c.Pictures.First().Id));

            CreateMap<RegisterAdViewModel, Ad>()
                .ConstructUsing(c => new Ad(c.Title, c.Description, DateTime.UtcNow, c.IdCategory, c.Price, c.IdAdvertiser))
                .ForAllMembers(c => c.Ignore());
        }
    }
}
