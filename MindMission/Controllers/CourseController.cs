using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using MindMission.API.Utilities;
using MindMission.Application.DTO;
using MindMission.Application.Service_Interfaces;
using MindMission.Domain.Enums;
using MindMission.Domain.Models;
using MindMission.Infrastructure.Exceptions;

namespace MindMission.API.Controllers
{
    // TODO: Refactor code and add Try catch block, comments
    // TODO: Add data so i can test each action
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
            if (courses == null) return NotFound("No courses found.");

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

        // GET: api/Course/{courseId}
        [HttpGet("{courseId}")]
        public async Task<IActionResult> GetCourseById(int courseId)
        {
            var course = await _courseService.GetByIdAsync(courseId);

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

        // GET: api/Course/name/{name}
        [HttpGet("name/{name}")]
        public async Task<ActionResult<CourseDto>> GetCourseByName(string name)
        {
            try
            {
                var course = await _courseService.GetByNameAsync(name);

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
            catch (EntityNotFoundException)
            {
                return NotFound();
            }
        }

        // GET: api/Course/category/{categoryId}

        [HttpGet("category/{categoryId}")]
        public async Task<ActionResult<IEnumerable<CourseDto>>> GetCoursesByCategory(int categoryId)
        {
            var courses = await _courseService.GetAllByCategoryAsync(categoryId);
            if (courses == null) return NotFound("No courses found.");

            var courseDTOs = await Task.WhenAll(courses.Select(course => MapCourseToDTO(course)));

            var response = new ResponseObject<CourseDto>
            {
                Success = true,
                Message = $"All Courses for category {categoryId}",
                Items = courseDTOs.ToList(),
                PageNumber = 1,
                ItemsPerPage = 10,
                TotalPages = courseDTOs.Length
            };

            return Ok(response);
        }

        // GET: api/Course/{courseId}/related

        [HttpGet("{courseId}/related")]
        public async Task<ActionResult<IEnumerable<CourseDto>>> GetRelatedCourses(int courseId)
        {
            var courses = await _courseService.GetRelatedCoursesAsync(courseId);
            if (courses == null) return NotFound("No courses found.");

            var courseDTOs = await Task.WhenAll(courses.Select(course => MapCourseToDTO(course)));

            var response = new ResponseObject<CourseDto>
            {
                Success = true,
                Message = $"All Courses related to {courseId}",
                Items = courseDTOs.ToList(),
                PageNumber = 1,
                ItemsPerPage = 10,
                TotalPages = courseDTOs.Length
            };

            return Ok(response);
        }

        // GET: api/Course/instructor/{instructorId}

        [HttpGet("instructor/{instructorId}")]
        public async Task<ActionResult<IEnumerable<CourseDto>>> GetCoursesByInstructor(string instructorId)
        {
            var courses = await _courseService.GetAllByInstructorAsync(instructorId);
            if (courses == null) return NotFound("No courses found.");

            var courseDTOs = await Task.WhenAll(courses.Select(course => MapCourseToDTO(course)));

            var response = new ResponseObject<CourseDto>
            {
                Success = true,
                Message = $"All Courses for {instructorId}",
                Items = courseDTOs.ToList(),
                PageNumber = 1,
                ItemsPerPage = 10,
                TotalPages = courseDTOs.Length
            };

            return Ok(response);
        }

        // GET: api/Course/top/{topNumber}

        [HttpGet("top/{topNumber}")]
        public async Task<ActionResult<IEnumerable<CourseDto>>> GetTopRatedCourses(int topNumber)
        {
            var courses = await _courseService.GetTopRatedCoursesAsync(topNumber);
            if (courses == null) return NotFound("No courses found.");

            var courseDTOs = await Task.WhenAll(courses.Select(course => MapCourseToDTO(course)));

            var response = new ResponseObject<CourseDto>
            {
                Success = true,
                Message = $"Top {topNumber} Courses",
                Items = courseDTOs.ToList(),
                PageNumber = 1,
                ItemsPerPage = 10,
                TotalPages = courseDTOs.Length
            };

            return Ok(response);
        }

        // GET: api/Course/recent/{recentNumber}

        [HttpGet("recent/{recentNumber}")]
        public async Task<ActionResult<IEnumerable<CourseDto>>> GetRecentCourses(int recentNumber)
        {
            var courses = await _courseService.GetRecentCoursesAsync(recentNumber);
            if (courses == null) return NotFound("No courses found.");

            var courseDTOs = await Task.WhenAll(courses.Select(course => MapCourseToDTO(course)));

            var response = new ResponseObject<CourseDto>
            {
                Success = true,
                Message = $"Recent {recentNumber} Courses",
                Items = courseDTOs.ToList(),
                PageNumber = 1,
                ItemsPerPage = 10,
                TotalPages = courseDTOs.Length
            };

            return Ok(response);
        }
        #endregion

        #region Add 

        // POST: api/Course
        [HttpPost]
        public async Task<ActionResult<CourseDto>> AddCourse([FromBody] CourseDto courseDTO)
        {

            var course = MapDTOToCourse(courseDTO);
            await _courseService.AddAsync(course);

            var result = await MapCourseToDTO(course);

            return CreatedAtAction(nameof(GetCourseById), new { id = result.Id }, result);
        }

        #endregion

        #region Delete
        // DELETE: api/Course/{courseId}
        [HttpDelete("{courseId}")]
        public async Task<IActionResult> DeleteCourse(int courseId)
        {
            var course = await _courseService.GetByIdAsync(courseId);

            if (course == null)
            {
                return NotFound();
            }

            await _courseService.DeleteAsync(courseId);

            return NoContent();
        }
        #endregion

        #region Edit Patch/Put 
        // PUT: api/Course/{courseId}

        [HttpPut("{courseId}")]
        public async Task<ActionResult> UpdateCourse(int courseId, CourseDto courseDto)
        {
            if (courseId != courseDto.Id)
            {
                return BadRequest();
            }
            var course = await _courseService.GetByIdAsync(courseId);
            if (course == null)
            {
                return NotFound();
            }

            course = MapDTOToCourse(courseDto);


            await _courseService.UpdateAsync(course);

            return NoContent();
        }
        // PATCH: api/Course/{courseId}

        [HttpPatch("{courseId}")]
        public async Task<ActionResult> PatchCourse(int courseId, [FromBody] JsonPatchDocument<CourseDto> patchDocument)
        {
            if (patchDocument == null)
            {
                return BadRequest();
            }

            var course = await _courseService.GetByIdAsync(courseId);
            if (course == null)
            {
                return NotFound();
            }

            var courseDto = await MapCourseToDTO(course);

            // apply patch
            patchDocument.ApplyTo(
                 courseDto,
                 error =>
                 {
                     ModelState.AddModelError("JsonPatch", error.ErrorMessage);
                 });

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            course = MapDTOToCourse(courseDto);

            await _courseService.UpdateAsync(course);

            return NoContent();
        }
        #endregion

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
        }

        private static Course MapDTOToCourse(CourseDto courseDTO)
        {
            return new Course
            {
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
                UpdatedAt = null, // Not updated yet
                CategoryId = courseDTO.CategoryId,
                InstructorId = courseDTO.InstructorId
            };
        }
        #endregion

    }
}
