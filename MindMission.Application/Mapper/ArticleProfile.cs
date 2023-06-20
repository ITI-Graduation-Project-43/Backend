using AutoMapper;
using MindMission.Application.DTOs.ArticleDtos;
using MindMission.Domain.Models;

namespace MindMission.Application.Mapper
{
    /// <summary>
    /// This class is used by AutoMapper to understand how to map from the Article entity to the corresponding DTOs and vice versa.
    /// </summary>
    public class ArticleProfile : Profile
    {
        public ArticleProfile()
        {
            CreateMap<Article, ArticleDto>()
                .ForMember(dest => dest.LessonName, opts => opts.MapFrom(src => src.Lesson.Title))
                .ForMember(dest => dest.LessonDescription, opt => opt.MapFrom(src => src.Lesson.Description));

            CreateMap<ArticleCreateDto, Article>();
            CreateMap<Article, ArticleCreateDto>();

        }
    }
}
