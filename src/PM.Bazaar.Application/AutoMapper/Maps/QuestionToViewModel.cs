using System;
using AutoMapper;
using PM.Bazaar.Application.ViewModels;
using PM.Bazaar.Domain.Entities;

namespace PM.Bazaar.Application.AutoMapper.Maps
{
    public class QuestionToViewModel : Profile
    {
        public QuestionToViewModel()
        {
            CreateMap<Question, QuestionViewModel>();

            CreateMap<RegisterQuestionViewModel, Question>()
                .ConstructUsing(c => new Question(c.Description, DateTime.UtcNow, c.IdAdvertiser, c.IdAd))
                .ForAllMembers(c => c.Ignore());
        }
    }
}
