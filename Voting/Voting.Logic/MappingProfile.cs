using AutoMapper;
using System;
using Voiting.Repositories.Entities;
using Voting.Logic.Models;

namespace Voting.Logic
{
  public class MappingProfile : Profile
  {
    public MappingProfile()
    {
      CreateMap<QuestionModel, DbQuestion>()
        .ForMember(m => m.Id, opt => opt.MapFrom(src => src.Id ?? Guid.NewGuid()));

      CreateMap<DbQuestion, QuestionModel>();

      CreateMap<AnswerModel, DbAnswer>()
        .ForMember(m => m.Id, opt => opt.MapFrom(src => src.Id ?? Guid.NewGuid()));

      CreateMap<DbAnswer, AnswerModel>();

      CreateMap<DbUser, UserModel>().ReverseMap();

      CreateMap<VoteModel, DbVote>()
        .ForMember(m => m.Id, opt => opt.MapFrom(src => src.Id ?? Guid.NewGuid()))
        .ForMember(m => m.AnswerId, opt => opt.MapFrom(src => src.AnswerId))
        .ForMember(m => m.UserId, opt => opt.MapFrom(src => src.User.Id));

      CreateMap<DbVote, VoteModel>()
        .ForMember(m => m.Id, opt => opt.MapFrom(src => src.Id))
        .ForPath(m => m.User.Id, opt => opt.MapFrom(src => src.UserId))
        .ForPath(m => m.User.FirstName, opt => opt.MapFrom(src => src.UserFirstName))
        .ForPath(m => m.User.LastName, opt => opt.MapFrom(src => src.UserLastName));
    }
  }
}
