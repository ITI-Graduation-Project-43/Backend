using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using MindMission.API.Utilities;
using MindMission.Application.DTO;
using MindMission.Application.Service_Interfaces;
using MindMission.Application.Services;
using MindMission.Domain.Enums;
using MindMission.Domain.Models;

namespace MindMission.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly ICourseService _courseService;
        private readonly IInstructorService _instructorService;

        public CourseController(ICourseService courseService, IInstructorService instructorService)
        {
            _courseService = courseService;
            _instructorService = instructorService;
        }






        #region Get
        // GET: api/Course
        [HttpGet]
        public async Task<IActionResult> GetAllCourses()
        {
            var courses = await _courseService.GetAllAsync();
            if (courses == null) return NotFound("No categories found.");

            var courseDTOs = await Task.WhenAll(courses.Select(course => MapCourseToDTO(course)));

            var response = new ResponseObject<CourseDto>
            {
                Success = true,
                Message = "All Courses",
                Items = courseDTOs.ToList(),
                PageNumber = 1,
                ItemsPerPage = 10,
                TotalPages = courseDTOs.Length
            };

            return Ok(response);
        }

        // GET: api/Course/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCourseById(int id)
        {
            var course = await _courseService.GetByIdAsync(id);

            if (course == null)
            {
                return NotFound();
            }

            var courseDto = await MapCourseToDTO(course);


            var response = new ResponseObject<CourseDto>
            {
                Success = true,
                Message = "Course found",
                Items = new List<CourseDto> { courseDto },
                PageNumber = 1,
                ItemsPerPage = 1,
                TotalPages = 1
            };

            return Ok(response);
        }
        #endregion


        // TODO: Add GET endpoint for searching courses by name
        // TODO: Add GET endpoint for retrieving all courses in a specific category
        // TODO: Add GET endpoint for retrieving related courses in the same category
        // TODO: Add GET endpoint for retrieving all courses from a specific instructor
        // TODO: Add GET endpoint for retrieving top rated courses
        // TODO: Add GET endpoint for retrieving recent courses based on the creation date
        // TODO: Add DELETE endpoint for deleting a course
        // TODO: Add PUT and PATCH endpoints for updating a course

        #region Helper Methods
        private async Task<CourseDto> MapCourseToDTO(Course course)
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
            #endregion
        }

    }
}
