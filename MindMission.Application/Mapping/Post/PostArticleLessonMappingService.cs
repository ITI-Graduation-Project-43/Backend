﻿using MindMission.Application.DTOs.PostDtos;
using MindMission.Application.Mapping.Base;
using MindMission.Domain.Enums;
using MindMission.Domain.Models;

namespace MindMission.Application.Mapping.Post
{
    public class PostArticleLessonMappingService : IMappingService<Lesson, PostArticleLessonDto>
    {
        public async Task<PostArticleLessonDto> MapEntityToDto(Lesson lesson)
        {
            var postArticleLessonDto = new PostArticleLessonDto
            {
                LessonId = lesson.Id,
                Title = lesson.Title,
                Description = lesson.Description,
                NoOfHours = lesson.NoOfHours,
                IsFree = lesson.IsFree,
                Content = lesson.Articles?.FirstOrDefault()?.Content ?? string.Empty
            };

            return postArticleLessonDto;
        }

        public Lesson MapDtoToEntity(PostArticleLessonDto postArticleLessonDto)
        {
            var lesson = new Lesson
            {
                Id = postArticleLessonDto.LessonId,
                Title = postArticleLessonDto.Title,
                Description = postArticleLessonDto.Description,
                NoOfHours = postArticleLessonDto.NoOfHours,
                IsFree = postArticleLessonDto.IsFree,
                Type = LessonType.Article,
            };

            if (!string.IsNullOrEmpty(postArticleLessonDto.Content))
            {
                var article = new Article
                {
                    Content = postArticleLessonDto.Content
                };
                lesson.Articles = new List<Article> { article };
            }

            return lesson;
        }
    }
}
