using AutoMapper;
using MindMission.Application.DTOs.ArticleDtos;
using MindMission.Domain.Models;

namespace MindMission.Application.Mapper
{
    public class ArticleProfile : Profile
    {
        public ArticleProfile()
        {
            CreateMap<Article, ArticleDto>()
                .ForMember(dest => dest.LessonName, opts => opts.MapFrom(src => src.Lesson.Title));

            CreateMap<ArticleCreateDto, Article>();
            CreateMap<Article, ArticleCreateDto>();

        }
    }
}
