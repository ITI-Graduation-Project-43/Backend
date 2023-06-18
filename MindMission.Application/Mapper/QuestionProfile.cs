using AutoMapper;
using MindMission.Application.DTOs.QuestionDtos;
using MindMission.Application.DTOs.QuizDtos;
using MindMission.Domain.Models;

namespace MindMission.Application.Mapper
{
    /// <summary>
    /// This class is used by AutoMapper to understand how to map from the  Question entities to the corresponding DTOs and vice versa.
    /// </summary>
    public class QuestionProfile : Profile
    {
        public QuestionProfile()
        {
            CreateMap<Question, QuestionDto>();

            CreateMap<QuestionCreateDto, Question>();
            CreateMap<Question, QuestionCreateDto>();
        }

    }

}
