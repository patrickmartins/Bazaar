using AutoMapper;
using PM.Bazaar.Application.ViewModels;
using PM.Bazaar.Domain.Entities;
using PM.Bazaar.Infrastructure.CrossCutting.Identity.Entities;
using System;

namespace PM.Bazaar.Application.AutoMapper.Maps
{
    public class AccountToViewModel : Profile
    {
        public AccountToViewModel()
        {
            CreateMap<Account, AccountViewModel>()
                .ForMember(c => c.Name, x => x.MapFrom(c => c.Advertiser.Name))
                .ForMember(c => c.LastName, x => x.MapFrom(c => c.Advertiser.LastName))
                .ForMember(c => c.Avatar, x => x.MapFrom(c => c.Advertiser.Avatar.Id));

            CreateMap<Account, UserViewModel>()
                .ForMember(c => c.Name, x => x.MapFrom(c => c.Advertiser.Name))
                .ForMember(c => c.Avatar, x => x.MapFrom(c => c.Advertiser.Avatar.Id));

            CreateMap<RegisterAccountViewModel, Account>()
                .ConstructUsing(c => new Account(c.Email, c.Password, new Advertiser(c.Name, c.LastName, DateTime.Now, new Image(c.Avatar.Bytes, c.Avatar.Hash))))
                .ForAllMembers(c => c.Ignore());
        }
    }
}
