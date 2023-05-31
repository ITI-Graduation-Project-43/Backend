using MindMission.Application.DTOs;
using MindMission.Application.Mapping.Base;
using MindMission.Application.Service_Interfaces;
using MindMission.Domain.Enums;
using MindMission.Domain.Models;

namespace MindMission.Application.Mapping
{
    public class CourseMappingService : IMappingService<Course, CourseDto>
    {
        private readonly IInstructorService _instructorService;

        public CourseMappingService(IInstructorService instructorService)
        {
            _instructorService = instructorService;
        }

        public async Task<CourseDto> MapEntityToDto(Course course)
        {
            var courseDTO = new CourseDto
            {
                Id = course.Id,
                Title = course.Title,
                ShortDescription = course.ShortDescription,
                Description = course.Description,
                WhatWillLearn = course.WhatWillLearn,
                Requirements = course.Requirements,
                WholsFor = course.WholsFor,
                ImageUrl = course.ImageUrl,
                Language = (Language)Enum.Parse(typeof(Language), course.Language),
                Price = course.Price,
                Level = (Level)Enum.Parse(typeof(Level), course.Level),
                AvgReview = course.AvgReview,
                NoOfReviews = course.NoOfReviews,
                NoOfStudents = course.NoOfStudents,
                Discount = course.Discount,
                ChapterCount = course.ChapterCount,
                LessonCount = course.LessonCount,
                NoOfVideos = course.NoOfVideos,
                NoOfArticles = course.NoOfArticles,
                NoOfAttachments = course.NoOfAttachments,
                NoOfHours = course.NoOfHours,
                Published = course.Published,
                Approved = course.Approved,
                CreatedAt = course.CreatedAt,
                UpdatedAt = course.UpdatedAt,
                CategoryId = course.CategoryId
            };

            if (course.Instructor != null)
            {
                courseDTO.InstructorId = course.Instructor.Id;
                courseDTO.InstructorName = course.Instructor.FirstName + " " + course.Instructor.LastName;
                courseDTO.InstructorBio = course.Instructor.Bio;
                courseDTO.InstructorProfilePicture = course.Instructor.ProfilePicture;
                courseDTO.InstructorTitle = course.Instructor.Title;
                courseDTO.InstructorDescription = course.Instructor.Description;
                courseDTO.InstructorNoOfCourses = course.Instructor.Courses.Count;
                courseDTO.InstructorNoOfStudents = course.Instructor.NoOfStudents;
                courseDTO.InstructorAvgRating = course.Instructor.AvgRating;
                courseDTO.InstructorNoOfRatings = course.Instructor.NoOfRatings;
            }

            if (course.Category != null)
            {
                courseDTO.CategoryName = course.Category.Name;
            }

            // Map the chapter names
            foreach (var chapter in course.Chapters)
            {
                courseDTO.ChapterNames.Add(chapter.Title);
            }

            return courseDTO;
        }

        public Course MapDtoToEntity(CourseDto courseDTO)
        {
            return new Course
            {
                Id = courseDTO.Id,
                Title = courseDTO.Title,
                ShortDescription = courseDTO.ShortDescription,
                Description = courseDTO.Description,
                WhatWillLearn = courseDTO.WhatWillLearn,
                Requirements = courseDTO.Requirements,
                WholsFor = courseDTO.WholsFor,
                ImageUrl = courseDTO.ImageUrl,
                Language = courseDTO.Language.ToString(),
                Price = courseDTO.Price,
                Level = courseDTO.Level.ToString(),
                AvgReview = courseDTO.AvgReview,
                NoOfReviews = courseDTO.NoOfReviews,
                NoOfStudents = courseDTO.NoOfStudents,
                Discount = courseDTO.Discount,
                ChapterCount = courseDTO.ChapterCount,
                LessonCount = courseDTO.LessonCount,
                NoOfVideos = courseDTO.NoOfVideos,
                NoOfArticles = courseDTO.NoOfArticles,
                NoOfAttachments = courseDTO.NoOfAttachments,
                NoOfHours = courseDTO.NoOfHours,
                Published = courseDTO.Published,
                Approved = courseDTO.Approved,
                CreatedAt = DateTime.Now,
                UpdatedAt = null,
                CategoryId = courseDTO.CategoryId,
                InstructorId = courseDTO.InstructorId
            };
        }
    }
}