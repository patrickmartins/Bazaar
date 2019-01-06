using System;
using AutoMapper;
using PM.Bazaar.Application.AutoMapper.Maps;

namespace PM.Bazaar.Application.AutoMapper
{
    public static class AutoMapperConfiguration
    {
        public static void Initialize()
        {
            Mapper.Initialize(c =>
            {
                c.AddProfile(new AccountToViewModel());
                c.AddProfile(new AdToViewModel());
                c.AddProfile(new ImageToViewModel());
                c.AddProfile(new QuestionToViewModel());
                c.AddProfile(new ResponseToViewModel());
                c.AddProfile(new AdvertiserToViewModel());

                c.ValueTransformers.Add<DateTime>(x => DateTime.SpecifyKind(x, DateTimeKind.Utc));
            });
        }
    }
}
