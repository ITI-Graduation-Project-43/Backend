using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using MindMission.API.Controllers.Base;
using MindMission.Application.DTOs;
using MindMission.Application.Factories;
using MindMission.Application.Interfaces.Patch;
using MindMission.Application.Interfaces.Services;
using MindMission.Application.Mapping.Base;
using MindMission.Application.Service_Interfaces;
using MindMission.Domain.Constants;
using MindMission.Domain.Enums;
using MindMission.Domain.Models;
using MindMission.Application.Exceptions;
using MindMission.Application.DTOs.PostDtos;
using MindMission.Application.Services;

namespace MindMission.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : BaseController<Course, CourseDto, int>
    {
        private readonly ICourseService _courseService;
        private readonly IMappingService<Course, PostCourseDto> _postCourseMappingService;
        private readonly ICategoryService _categoryService;
        private readonly IUploadImageService _uploadImageService;
        private readonly ICoursePatchValidator _coursePatchValidator;
        public CourseController(ICourseService courseService,
                                IMappingService<Course, CourseDto> courseMappingService,
                                IMappingService<Course, PostCourseDto> postCourseMappingService,
                                ICategoryService categoryService,
                                IUploadImageService uploadImageService,
                                ICoursePatchValidator coursePatchValidator) : base(courseMappingService)
        {
            _courseService = courseService ?? throw new ArgumentNullException(nameof(courseService));
            _postCourseMappingService = postCourseMappingService ?? throw new ArgumentNullException(nameof(postCourseMappingService));
            _categoryService = categoryService ?? throw new ArgumentNullException(nameof(categoryService));
            _uploadImageService = uploadImageService ?? throw new ArgumentNullException(nameof(uploadImageService));
            _coursePatchValidator = coursePatchValidator ?? throw new ArgumentNullException(nameof(coursePatchValidator));
        }

        #region Get

        // GET: api/Course
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CourseDto>>> GetAllCourses([FromQuery] PaginationDto pagination)
        {
            return await GetEntitiesResponse(_courseService.GetAllAsync, pagination, "Courses");
        }

        // GET: api/Course/category/{categoryId}

        [HttpGet("category/{categoryId}")]
        public async Task<ActionResult<IEnumerable<CourseDto>>> GetCoursesByCategory(int categoryId, [FromQuery] PaginationDto pagination)
        {
            return await GetEntitiesResponse(() => _courseService.GetAllByCategoryAsync(categoryId), pagination, "Courses");
        }

        // GET: api/Course/{courseId}/related

        [HttpGet("{courseId}/related")]
        public async Task<ActionResult<IEnumerable<CourseDto>>> GetRelatedCourses(int courseId, [FromQuery] PaginationDto pagination)
        {
            return await GetEntitiesResponse(() => _courseService.GetRelatedCoursesAsync(courseId), pagination, "Courses");
        }

        // GET: api/Course/instructor/{instructorId}

        [HttpGet("instructor/{instructorId}")]
        public async Task<ActionResult<IEnumerable<CourseDto>>> GetCoursesByInstructor(string instructorId, [FromQuery] PaginationDto pagination)
        {
            return await GetEntitiesResponse(() => _courseService.GetAllByInstructorAsync(instructorId), pagination, "Courses");
        }

        // GET: api/Course/instructorOtherCourses/{instructorId}

        [HttpGet("{courseId}/instructor/{instructorId}")]
        public async Task<ActionResult<IEnumerable<CourseDto>>> GetInstructorOtherCourses(string instructorId, int courseId, [FromQuery] PaginationDto pagination)
        {
            return await GetEntitiesResponse(() => _courseService.GetInstructorOtherCourses(instructorId, courseId), pagination, "Courses");
        }

        // GET: api/Course/top/{topNumber}

        [HttpGet("top/{topNumber}")]
        public async Task<ActionResult<IEnumerable<CourseDto>>> GetTopRatedCourses(int topNumber, [FromQuery] PaginationDto pagination)
        {
            return await GetEntitiesResponse(() => _courseService.GetTopRatedCoursesAsync(topNumber), pagination, "Courses");
        }

        // GET: api/Course/recent/{recentNumber}

        [HttpGet("recent/{recentNumber}")]
        public async Task<ActionResult<IEnumerable<CourseDto>>> GetRecentCourses(int recentNumber, [FromQuery] PaginationDto pagination)
        {
            return await GetEntitiesResponse(() => _courseService.GetRecentCoursesAsync(recentNumber), pagination, "Courses");
        }

        // GET: api/Course/{courseId}/related/{studentsNumber}

        [HttpGet("{courseId}/related/{studentsNumber}")]
        public async Task<ActionResult<IQueryable<CourseDto>>> GetRelatedCoursesWithStudentsAsync(int courseId, int studentsNumber, [FromQuery] PaginationDto pagination)
        {
            var courses = await _courseService.GetRelatedCoursesWithStudentsAsync(courseId, studentsNumber);

            if (courses == null)
            {
                return NotFound(NotFoundResponse("Courses"));
            }


            var coursesList = courses.ToList();
            if (coursesList.Count == 0)
            {
                return NotFound(NotFoundResponse("Courses"));
            }
            var coursesPage = coursesList.Skip((pagination.PageNumber - 1) * pagination.PageSize).Take(pagination.PageSize).ToList();

            var response = ResponseObjectFactory.CreateResponseObject(true, string.Format(SuccessMessages.RetrievedSuccessfully, "Courses"), coursesPage, pagination.PageNumber, pagination.PageSize);
            return Ok(response);
        }


        [HttpGet("{courseId}/instructor/{instructorId}/{studentsNumber}")]
        public async Task<ActionResult<IQueryable<StudentCourseDto>>> GetInstructorOtherCoursesWithStudentsAsync(string instructorId, int courseId, int studentsNumber, [FromQuery] PaginationDto pagination)
        {
            var courses = await _courseService.GetInstructorOtherWithStudentsCourses(instructorId, courseId, studentsNumber);

            if (courses == null)
            {
                return NotFound(NotFoundResponse("Courses"));
            }

            var coursesList = courses.ToList();
            if (coursesList.Count == 0)
            {
                return NotFound(NotFoundResponse("Courses"));
            }
            var coursesPage = coursesList.Skip((pagination.PageNumber - 1) * pagination.PageSize).Take(pagination.PageSize).ToList();

            var response = ResponseObjectFactory.CreateResponseObject(true, string.Format(SuccessMessages.RetrievedSuccessfully, "Courses"), coursesPage, pagination.PageNumber, pagination.PageSize);
            return Ok(response);
        }

        [HttpGet("{courseId}/students/{studentsNumber}")]
        public async Task<ActionResult<StudentCourseDto>> GetCourseByIdWithStudentsAsync(int courseId, int studentsNumber)
        {
            var course = await _courseService.GetCourseByIdWithStudentsAsync(courseId, studentsNumber);

            if (course == null)
            {
                return NotFound(NotFoundResponse("Course"));
            }

            var response = ResponseObjectFactory.CreateResponseObject(true, string.Format(SuccessMessages.RetrievedSuccessfully, "Course"), new List<StudentCourseDto>() { course });
            return Ok(response);
        }

        // GET: api/Course/{Id}
        [HttpGet("{Id}")]
        public async Task<ActionResult<CourseDto>> GetCourseById(int Id)
        {
            return await GetEntityResponse(() => _courseService.GetByIdAsync(Id), "Course");
        }

        // GET: api/Course/name/{name}
        [HttpGet("name/{name}")]
        public async Task<ActionResult<CourseDto>> GetCourseByName(string name)
        {
            return await GetEntityResponse(() => _courseService.GetByNameAsync(name), "Course");
        }

        // GET: api/Course/featureThisWeek
        [HttpGet("featureThisWeek")]
        public async Task<ActionResult<CourseDto>> GetFeatureThisWeekCourse()
        {
            return await GetEntityResponse(() => _courseService.GetFeatureThisWeekCourse(), "Course");
        }


        #endregion Get

        #region Add

        // POST: api/Course
        [HttpPost("withPhoto")]
        public async Task<IActionResult> AddCourseWithPhoto([FromForm] IFormFile courseImg, [FromForm] PostCourseDto postCourseDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(InvalidDataResponse());

            }
            var category = await _categoryService.GetByIdAsync(postCourseDto.CategoryId);
            if (category == null)
            {
                return BadRequest(NotFoundResponse("Category"));
            }

            if (category.Type != CategoryType.Topic)
            {
                return BadRequest(ValidationFailedResponse());
            }

            if (!Enum.IsDefined(typeof(Language), postCourseDto.Language))
            {
                return BadRequest(ValidationFailedResponse());
            }

            if (!Enum.IsDefined(typeof(Level), postCourseDto.Level))
            {
                return BadRequest(ValidationFailedResponse());
            }

            if (postCourseDto.LearningItems == null || !postCourseDto.LearningItems.Any() || postCourseDto.LearningItems.Any(item => string.IsNullOrWhiteSpace(item.Title) || string.IsNullOrWhiteSpace(item.Description)))
            {
                return BadRequest(ValidationFailedResponse());
            }

            if (postCourseDto.EnrollmentItems == null || !postCourseDto.EnrollmentItems.Any() || postCourseDto.EnrollmentItems.Any(item => string.IsNullOrWhiteSpace(item.Title)))
            {
                return BadRequest(ValidationFailedResponse());
            }

            string courseImage = await _uploadImageService.Upload(courseImg);


            if (string.IsNullOrEmpty(courseImage))
            {
                return BadRequest(ValidationFailedResponse());
            }


            postCourseDto.CourseImage = courseImage;

            // Map the course create DTO to a course entity.
            var courseToCreate = _postCourseMappingService.MapDtoToEntity(postCourseDto);

            // Use the service to add the course to the database.
            var createdCourse = await _courseService.AddCourseAsync(courseToCreate);

            // Map the created course entity back to a DTO.
            var courseDto = await _postCourseMappingService.MapEntityToDto(createdCourse);

            if (courseDto == null)
                return NotFound(NotFoundResponse("Course"));
            // Return the created course.


            string message = string.Format(SuccessMessages.CreatedSuccessfully, "Course");

            var response = ResponseObjectFactory.CreateResponseObject(true, message, new List<PostCourseDto> { courseDto });


            return CreatedAtAction(nameof(GetCourseById), new { id = courseDto.Id }, response);
        }
        [HttpPost]
        public async Task<IActionResult> AddCourse([FromBody] PostCourseDto postCourseDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(InvalidDataResponse());

            }
            var category = await _categoryService.GetByIdAsync(postCourseDto.CategoryId);
            if (category == null)
            {
                return BadRequest(NotFoundResponse("Category"));
            }

            if (category.Type != CategoryType.Topic)
            {
                return BadRequest(ValidationFailedResponse());
            }

            if (!Enum.IsDefined(typeof(Language), postCourseDto.Language))
            {
                return BadRequest(ValidationFailedResponse());
            }

            if (!Enum.IsDefined(typeof(Level), postCourseDto.Level))
            {
                return BadRequest(ValidationFailedResponse());
            }

            if (postCourseDto.LearningItems == null || !postCourseDto.LearningItems.Any() || postCourseDto.LearningItems.Any(item => string.IsNullOrWhiteSpace(item.Title) || string.IsNullOrWhiteSpace(item.Description)))
            {
                return BadRequest(ValidationFailedResponse());
            }

            if (postCourseDto.EnrollmentItems == null || !postCourseDto.EnrollmentItems.Any() || postCourseDto.EnrollmentItems.Any(item => string.IsNullOrWhiteSpace(item.Title)))
            {
                return BadRequest(ValidationFailedResponse());
            }


            // Map the course create DTO to a course entity.
            var courseToCreate = _postCourseMappingService.MapDtoToEntity(postCourseDto);

            // Use the service to add the course to the database.
            var createdCourse = await _courseService.AddCourseAsync(courseToCreate);

            // Map the created course entity back to a DTO.
            var courseDto = await _postCourseMappingService.MapEntityToDto(createdCourse);

            if (courseDto == null)
                return NotFound(NotFoundResponse("Course"));
            // Return the created course.


            string message = string.Format(SuccessMessages.CreatedSuccessfully, "Course");

            var response = ResponseObjectFactory.CreateResponseObject(true, message, new List<PostCourseDto> { courseDto });


            return CreatedAtAction(nameof(GetCourseById), new { id = courseDto.Id }, response);
        }


        #endregion Add

        #region Delete

        // DELETE: api/Course/delete/{courseId}
        [HttpDelete("Delete/{courseId}")]
        public async Task<IActionResult> DeleteCourse(int courseId)
        {

            return await DeleteEntityResponse(_courseService.GetByIdAsync, _courseService.DeleteAsync, courseId);
        }
        // DELETE: api/Course/{id}

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {

            var course = await _courseService.GetByIdAsync(id);

            if (course == null)
                return NotFound(NotFoundResponse("Course"));
            await _courseService.SoftDeleteAsync(id);
            return NoContent();
        }
        #endregion Delete

        #region Edit Patch/Put

        // PUT: api/Course/{id}
        [HttpPut(template: "photo/{photoid}")]
        public async Task<IActionResult> UpdateCourseWithPhoto(int id, [FromForm] IFormFile courseImg, [FromForm] PostCourseDto postCourseDto)
        {
            var courseToUpdate = await _courseService.GetByIdAsync(id, c => c.LearningItems,
                                                                       c => c.EnrollmentItems,
                                                                       c => c.EnrollmentItems);
            if (courseToUpdate == null)
            {
                return NotFound(NotFoundResponse("Course"));
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(InvalidDataResponse());
            }

            postCourseDto.Id = id;
            var category = await _categoryService.GetByIdAsync(postCourseDto.CategoryId);
            if (category == null)
            {
                return BadRequest(NotFoundResponse("Category"));
            }

            if (category.Type != CategoryType.Topic)
            {
                return BadRequest(ValidationFailedResponse());
            }

            if (!Enum.IsDefined(typeof(Language), postCourseDto.Language))
            {
                return BadRequest(ValidationFailedResponse());
            }

            if (!Enum.IsDefined(typeof(Level), postCourseDto.Level))
            {
                return BadRequest(ValidationFailedResponse());
            }

            if (postCourseDto.LearningItems == null || !postCourseDto.LearningItems.Any() || postCourseDto.LearningItems.Any(item => string.IsNullOrWhiteSpace(item.Title) || string.IsNullOrWhiteSpace(item.Description)))
            {
                return BadRequest(ValidationFailedResponse());
            }

            if (postCourseDto.EnrollmentItems == null || !postCourseDto.EnrollmentItems.Any() || postCourseDto.EnrollmentItems.Any(item => string.IsNullOrWhiteSpace(item.Title)))
            {
                return BadRequest(ValidationFailedResponse());
            }

            string courseImage = await _uploadImageService.Upload(courseImg);


            if (string.IsNullOrEmpty(courseImage))
            {
                return BadRequest(ValidationFailedResponse());
            }


            postCourseDto.CourseImage = courseImage;
            // Map the course update DTO to a course entity.
            var updatedCourseEntity = _postCourseMappingService.MapDtoToEntity(postCourseDto);

            // Use the service to update the course in the database.
            var updatedCourse = await _courseService.UpdateCourseAsync(id, updatedCourseEntity);

            // Map the updated course entity back to a DTO.
            var courseDto = await _postCourseMappingService.MapEntityToDto(updatedCourse);

            string message = string.Format(SuccessMessages.UpdatedSuccessfully, "Course");
            var response = ResponseObjectFactory.CreateResponseObject(true, message, new List<PostCourseDto> { courseDto });

            return Ok(response);
        }
        [HttpPut(template: "{id}")]

        public async Task<IActionResult> UpdateCourse(int id, [FromBody] PostCourseDto postCourseDto)
        {
            var courseToUpdate = await _courseService.GetByIdAsync(id, c => c.LearningItems,
                                                                       c => c.EnrollmentItems,
                                                                       c => c.EnrollmentItems);
            if (courseToUpdate == null)
            {
                return NotFound(NotFoundResponse("Course"));
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(InvalidDataResponse());
            }

            postCourseDto.Id = id;
            var category = await _categoryService.GetByIdAsync(postCourseDto.CategoryId);
            if (category == null)
            {
                return BadRequest(NotFoundResponse("Category"));
            }

            if (category.Type != CategoryType.Topic)
            {
                return BadRequest(ValidationFailedResponse());
            }

            if (!Enum.IsDefined(typeof(Language), postCourseDto.Language))
            {
                return BadRequest(ValidationFailedResponse());
            }

            if (!Enum.IsDefined(typeof(Level), postCourseDto.Level))
            {
                return BadRequest(ValidationFailedResponse());
            }

            if (postCourseDto.LearningItems == null || !postCourseDto.LearningItems.Any() || postCourseDto.LearningItems.Any(item => string.IsNullOrWhiteSpace(item.Title) || string.IsNullOrWhiteSpace(item.Description)))
            {
                return BadRequest(ValidationFailedResponse());
            }

            if (postCourseDto.EnrollmentItems == null || !postCourseDto.EnrollmentItems.Any() || postCourseDto.EnrollmentItems.Any(item => string.IsNullOrWhiteSpace(item.Title)))
            {
                return BadRequest(ValidationFailedResponse());
            }


            // Map the course update DTO to a course entity.
            var updatedCourseEntity = _postCourseMappingService.MapDtoToEntity(postCourseDto);

            // Use the service to update the course in the database.
            var updatedCourse = await _courseService.UpdateCourseAsync(id, updatedCourseEntity);

            // Map the updated course entity back to a DTO.
            var courseDto = await _postCourseMappingService.MapEntityToDto(updatedCourse);

            string message = string.Format(SuccessMessages.UpdatedSuccessfully, "Course");
            var response = ResponseObjectFactory.CreateResponseObject(true, message, new List<PostCourseDto> { courseDto });

            return Ok(response);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchCourse(int id, [FromBody] JsonPatchDocument<PostCourseDto> patchDoc)
        {
            if (patchDoc != null)
            {
                var courseInDb = await _courseService.GetByIdAsync(id, c => c.LearningItems,
                                                                       c => c.EnrollmentItems,
                                                                       c => c.EnrollmentItems);

                if (courseInDb == null)
                {
                    return NotFound(NotFoundResponse("Course"));
                }


                var courseToUpdateDto = await _postCourseMappingService.MapEntityToDto(courseInDb);
                if (courseToUpdateDto == null)
                {
                    return BadRequest(BadRequestResponse("Dto cannot be null."));
                }

                try
                {
                    await _coursePatchValidator.ValidatePatchAsync(patchDoc);
                }
                catch (ValidationException ex)
                {
                    return BadRequest(ex.Message);
                }



                patchDoc.ApplyTo(courseToUpdateDto, ModelState);

                if (!ModelState.IsValid)
                {
                    return BadRequest(InvalidDataResponse());
                }

                courseInDb = _postCourseMappingService.MapDtoToEntity(courseToUpdateDto);

                var updatedCourseEntity = await _courseService.UpdatePartialAsync(id, courseInDb);

                var courseDto = await _postCourseMappingService.MapEntityToDto(updatedCourseEntity);

                string message = string.Format(SuccessMessages.UpdatedSuccessfully, "Course");
                var response = ResponseObjectFactory.CreateResponseObject(true, message, new List<PostCourseDto> { courseDto });

                return Ok(response);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        #endregion Edit Patch/Put

        [HttpGet("instructor/approved/{instructorId}")]
        public async Task<ActionResult<IEnumerable<CourseDto>>> GetApprovedCoursesByInstructor(string instructorId, [FromQuery] PaginationDto pagination)
        {
            return await GetEntitiesResponse(() => _courseService.GetApprovedCoursesByInstructorAsync(instructorId), pagination, "Courses");
        }

        [HttpGet("instructor/waiting/{instructorId}")]
        public async Task<ActionResult<IEnumerable<CourseDto>>> GetNonApprovedCoursesByInstructor(string instructorId, [FromQuery] PaginationDto pagination)
        {
            return await GetEntitiesResponse(() => _courseService.GetNonApprovedCoursesByInstructorAsync(instructorId), pagination, "Courses");
        }


        [HttpPut("makeCourseApproved/{courseId}")]
        public async Task<ActionResult<Course>> makeCourseApproved(int courseId)
        {
            var course = await _courseService.PutCourseToApprovedAsync(courseId);
            return Ok(course);
        }
    }


}