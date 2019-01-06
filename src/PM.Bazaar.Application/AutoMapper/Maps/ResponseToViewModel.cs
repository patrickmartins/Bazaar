using System;
using AutoMapper;
using PM.Bazaar.Application.ViewModels;
using PM.Bazaar.Domain.Entities;

namespace PM.Bazaar.Application.AutoMapper.Maps
{
    public class ResponseToViewModel : Profile
    {
        public ResponseToViewModel()
        {
            CreateMap<Response, ResponseViewModel>();
            CreateMap<RegisterResponseViewModel, Response>()
                .ConstructUsing(c => new Response(c.IdQuestion, c.Description, DateTime.UtcNow, c.IdAdvertiser))
                .ForAllMembers(c => c.Ignore());
        }
    }
}
