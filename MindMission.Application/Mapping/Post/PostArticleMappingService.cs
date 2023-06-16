using MindMission.Application.DTOs.PostDtos;
using MindMission.Application.Mapping.Base;
using MindMission.Domain.Enums;
using MindMission.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindMission.Application.Mapping.Post
{
    public class PostArticleMappingService : IMappingService<Article, PostArticleDto>
    {

        public async Task<PostArticleDto> MapEntityToDto(Article article)
        {
            var postArticleDto = new PostArticleDto
            {
                Id = article.Id,
                LessonId = article.LessonId,
                Content = article.Content,
            };

            return postArticleDto;
        }

        public Article MapDtoToEntity(PostArticleDto postArticleDto)
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
