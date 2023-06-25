using AutoMapper;
using MindMission.Application.DTOs.QuestionDtos;
using MindMission.Application.DTOs.QuizDtos;
using MindMission.Domain.Models;


namespace MindMission.Application.Mapper
{
    /// <summary>
    /// This class is used by AutoMapper to understand how to map from the Quiz, Question entities to the corresponding DTOs and vice versa.
    /// </summary>
    public class QuizProfile : Profile
    {
        public QuizProfile()
        {
            CreateMap<Quiz, QuizDto>()
                .ForMember(dest => dest.LessonName, opts => opts.MapFrom(src => src.Lesson.Title))
                .ForMember(dest => dest.LessonDescription, opt => opt.MapFrom(src => src.Lesson.Description))
                .ForMember(dest => dest.Questions, opt => opt.MapFrom(src => src.Questions));



            CreateMap<Question, QuizQuestionDto>();
            CreateMap<QuizQuestionDto, Question>();

            CreateMap<QuizCreateDto, Quiz>();
            CreateMap<Quiz, QuizCreateDto>();


        }
    }
}
