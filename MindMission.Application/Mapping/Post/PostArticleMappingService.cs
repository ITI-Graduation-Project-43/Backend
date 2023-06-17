using MindMission.Application.DTOs.ArticleDtos;
using MindMission.Application.Mapping.Base;
using MindMission.Domain.Models;


namespace MindMission.Application.Mapping.Post
{
    public class PostArticleMappingService : IMappingService<Article, ArticleCreateDto>
    {

        public async Task<ArticleCreateDto> MapEntityToDto(Article article)
        {
            var postArticleDto = new ArticleCreateDto
            {
                Id = article.Id,
                LessonId = article.LessonId,
                Content = article.Content,
            };

            return postArticleDto;
        }

        public Article MapDtoToEntity(ArticleCreateDto postArticleDto)
        {
            var article = new Article
            {
                Id = postArticleDto.Id,
                LessonId = postArticleDto.LessonId,
                Content = postArticleDto.Content
            };



            return article;
        }

    }
}
