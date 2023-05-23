using MindMission.Application.DTOs;
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


            var instructor = await _instructorService.GetByIdAsync(course.InstructorId);
            if (instructor != null)
            {
                courseDTO.InstructorId = instructor.Id;
                courseDTO.InstructorName = instructor.FirstName + " " + instructor.LastName;
                courseDTO.InstructorBio = instructor.Bio;
                courseDTO.InstructorProfilePicture = instructor.ProfilePicture;
                courseDTO.InstructorTitle = instructor.Title;
                courseDTO.InstructorDescription = instructor.Description;
                courseDTO.InstructorNoOfCourses = instructor.Courses.Count;
                courseDTO.InstructorNoOfStudents = instructor.NoOfStudents;
                courseDTO.InstructorAvgRating = instructor.AvgRating;
                courseDTO.InstructorNoOfRatings = instructor.NoOfRatings;
            }

            // Get additional information about the category
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
