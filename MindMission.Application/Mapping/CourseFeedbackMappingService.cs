using MindMission.Application.DTOs;
using MindMission.Application.Mapping.Base;
using MindMission.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindMission.Application.Mapping
{
    public class CourseFeedbackMappingService : IMappingService<CourseFeedback, CourseFeedbackDto>
    {
        public CourseFeedback MapDtoToEntity(CourseFeedbackDto dto)
        {
            return new CourseFeedback()
            {
                CourseId = dto.CourseId,
                StudentId = dto.StudentId,
                InstructorId = dto.InstructorId,
                FeedbackText = dto.FeedbackText,
                CourseRating = dto.CourseRating,
                InstructorRating = dto.InstructorRating
            };
        }

        public async Task<CourseFeedbackDto> MapEntityToDto(CourseFeedback entity)
        {
            return new CourseFeedbackDto()
            {
                CourseId = entity.CourseId,
                StudentId = entity.StudentId,
                InstructorId = entity.InstructorId,
                FeedbackText = entity.FeedbackText,
                CourseRating = entity.CourseRating,
                InstructorRating = entity.InstructorRating
            };
        }
    }
}
