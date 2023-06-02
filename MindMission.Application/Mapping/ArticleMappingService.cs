using MindMission.Application.DTOs;
using MindMission.Application.Interfaces.Services;
using MindMission.Application.Mapping.Base;
using MindMission.Domain.Models;

namespace MindMission.Application.Mapping
{
    public class ArticleMappingService : IMappingService<Article, ArticleDto>
    {
        private readonly IArticleService _articleService;

        public ArticleMappingService(IArticleService articleService)
        {
            _articleService = articleService;
        }

        public async Task<ArticleDto> MapEntityToDto(Article article)
        {
            return new ArticleDto
            {
                Id = article.Id,
                Content = article.Content,
                LessonName = article.Lesson.Title,
                CreatedAt = article.CreatedAt,
                UpdatedAt = article.UpdatedAt
            };
        }

        public Article MapDtoToEntity(ArticleDto dto)
        {
            return new Article
            {
                Id = dto.Id,
                Content = dto.Content,
                CreatedAt = dto.CreatedAt,
                UpdatedAt = dto.UpdatedAt
            };
        }
    }
}