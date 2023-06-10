using MindMission.Application.DTOs;
using MindMission.Application.Mapping.Base;
using MindMission.Domain.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindMission.Application.Mapping
{
    public class CourseFeedbackMappingService
    {
        public CourseFeedback MapDtoToEntity(AddCourseFeedbackDto dto)
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

        public async Task<AddCourseFeedbackDto> MapEntityToDto(CourseFeedback entity)
        {
            return new AddCourseFeedbackDto()
            {
                CourseId = entity.CourseId,
                StudentId = entity.StudentId,
                InstructorId = entity.InstructorId,
                FeedbackText = entity.FeedbackText,
                CourseRating = entity.CourseRating,
                InstructorRating = entity.InstructorRating
            };
        }

        public async Task<IEnumerable<CourseFeedbackWithCourseDto>> SendMapEntityToDtoWithCourse(IEnumerable<CourseFeedback> entities)
        {
            var Result = new List<CourseFeedbackWithCourseDto>();
            foreach (var courseFeedback in entities)
            {
               Result.Add(new CourseFeedbackWithCourseDto()
                {
                    CourseName = courseFeedback.Course.Title,
                    StudentName = courseFeedback.Student.FirstName + " " + courseFeedback.Student.LastName,
                    StudentImage = courseFeedback.Student.ProfilePicture,
                    InstructorId = courseFeedback.InstructorId,
                    FeedbackText = courseFeedback.FeedbackText,
                    CourseRating = courseFeedback.CourseRating,
                    InstructorRating = courseFeedback.InstructorRating
                });
            }
            return Result;
        }

        public async Task<IEnumerable<CourseFeedbackWithInstructorDto>> SendMapEntityToDtoWithInstructor(IEnumerable<CourseFeedback> entities)
        {
            var Result = new List<CourseFeedbackWithInstructorDto>();
            foreach (var courseFeedback in entities)
            {
                Result.Add(new CourseFeedbackWithInstructorDto()
                {
                    CourseId = courseFeedback.CourseId,
                    StudentName = courseFeedback.Student.FirstName + " " + courseFeedback.Student.LastName,
                    StudentImage = courseFeedback.Student.ProfilePicture,
                    InstructorName = courseFeedback.Instructor.FirstName + " " + courseFeedback.Instructor.LastName,
                    InstructorImage = courseFeedback.Instructor.ProfilePicture,
                    FeedbackText = courseFeedback.FeedbackText,
                    CourseRating = courseFeedback.CourseRating,
                    InstructorRating = courseFeedback.InstructorRating
                });
            }
            return Result;
        }
    }
}
