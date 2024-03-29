﻿using MindMission.Application.DTOs.PostDtos;
using MindMission.Application.Mapping.Base;
using MindMission.Domain.Enums;
using MindMission.Domain.Models;

namespace MindMission.Application.Mapping.Post
{
    public class PostVideoLessonMappingService : IMappingService<Lesson, PostVideoLessonDto>
    {
        public async Task<PostVideoLessonDto> MapEntityToDto(Lesson lesson)
        {
            var postVideoLessonDto = new PostVideoLessonDto
            {
                ChapterId = lesson.ChapterId,
                LessonId = lesson.Id,
                Title = lesson.Title,
                Description = lesson.Description,
                NoOfHours = lesson.NoOfHours,
                IsFree = lesson.IsFree,
                VideoUrl = lesson.Video?.VideoUrl ?? string.Empty
            };

            return postVideoLessonDto;
        }

        public Lesson MapDtoToEntity(PostVideoLessonDto postVideoLessonDto)
        {
            var lesson = new Lesson
            {
                ChapterId = postVideoLessonDto.ChapterId,
                Id = postVideoLessonDto.LessonId,
                Title = postVideoLessonDto.Title,
                Description = postVideoLessonDto.Description,
                NoOfHours = postVideoLessonDto.NoOfHours,
                IsFree = postVideoLessonDto.IsFree,
                Type = LessonType.Video,
            };

            if (!string.IsNullOrEmpty(postVideoLessonDto.VideoUrl))
            {
                var video = new Video
                {
                    VideoUrl = postVideoLessonDto.VideoUrl
                };
                lesson.Video = video;
            }

            return lesson;
        }
    }
}
