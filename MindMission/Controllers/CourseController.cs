using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using MindMission.API.Controllers.Base;
using MindMission.Application.DTOs;
using MindMission.Application.Mapping;
using MindMission.Application.Service_Interfaces;
using MindMission.Domain.Models;

namespace MindMission.API.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : BaseController<Course, CourseDto>
    {
        private readonly ICourseService _courseService;
        private readonly CourseMappingService _courseMappingService;
        public CourseController(ICourseService courseService, CourseMappingService courseMappingService) : base(courseMappingService)
        {
            _courseService = courseService ?? throw new ArgumentNullException(nameof(courseService));
            _courseMappingService = courseMappingService ?? throw new ArgumentNullException(nameof(courseMappingService));
        }


        #region Get
        // GET: api/Course
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CourseDto>>> GetAllCourses([FromQuery] PaginationDto pagination)
        {
            var courses = await _courseService.GetAllAsync();
            if (courses == null) return NotFoundResponse("Courses");

            var courseDTOs = await MapEntitiesToDTOs(courses);
            var response = CreateResponse(courseDTOs, pagination, "Courses");

            return Ok(response);
        }


        // GET: api/Course/category/{categoryId}

        [HttpGet("category/{categoryId}")]
        public async Task<ActionResult<IEnumerable<CourseDto>>> GetCoursesByCategory(int categoryId, [FromQuery] PaginationDto pagination)
        {
            var courses = await _courseService.GetAllByCategoryAsync(categoryId);
            if (courses == null) return NotFoundResponse("Courses");

            var courseDTOs = await MapEntitiesToDTOs(courses);
            var response = CreateResponse(courseDTOs, pagination, "Courses");
            return Ok(response);
        }

        // GET: api/Course/{courseId}/related

        [HttpGet("{courseId}/related")]
        public async Task<ActionResult<IEnumerable<CourseDto>>> GetRelatedCourses(int courseId, [FromQuery] PaginationDto pagination)
        {
            var courses = await _courseService.GetRelatedCoursesAsync(courseId);
            if (courses == null) return NotFoundResponse("Courses");

            var courseDTOs = await MapEntitiesToDTOs(courses);
            var response = CreateResponse(courseDTOs, pagination, "Courses");
            return Ok(response);
        }

        // GET: api/Course/instructor/{instructorId}

        [HttpGet("instructor/{instructorId}")]
        public async Task<ActionResult<IEnumerable<CourseDto>>> GetCoursesByInstructor(string instructorId, [FromQuery] PaginationDto pagination)
        {
            var courses = await _courseService.GetAllByInstructorAsync(instructorId);
            if (courses == null) return NotFoundResponse("Courses");

            var courseDTOs = await MapEntitiesToDTOs(courses);
            var response = CreateResponse(courseDTOs, pagination, "Courses");
            return Ok(response);
        }

        // GET: api/Course/top/{topNumber}

        [HttpGet("top/{topNumber}")]
        public async Task<ActionResult<IEnumerable<CourseDto>>> GetTopRatedCourses(int topNumber, [FromQuery] PaginationDto pagination)
        {
            var courses = await _courseService.GetTopRatedCoursesAsync(topNumber);
            if (courses == null) return NotFoundResponse("Courses");

            var courseDTOs = await MapEntitiesToDTOs(courses);
            var response = CreateResponse(courseDTOs, pagination, "Courses");

            return Ok(response);
        }

        // GET: api/Course/recent/{recentNumber}

        [HttpGet("recent/{recentNumber}")]
        public async Task<ActionResult<IEnumerable<CourseDto>>> GetRecentCourses(int recentNumber, [FromQuery] PaginationDto pagination)
        {
            var courses = await _courseService.GetRecentCoursesAsync(recentNumber);
            if (courses == null) return NotFoundResponse("Courses");

            var courseDTOs = await MapEntitiesToDTOs(courses);
            var response = CreateResponse(courseDTOs, pagination, "Courses");
            return Ok(response);
        }

        // GET: api/Course/{courseId}
        [HttpGet("{courseId}")]
        public async Task<ActionResult<CourseDto>> GetCourseById(int courseId)
        {
            var course = await _courseService.GetByIdAsync(courseId);

            if (course == null) return NotFoundResponse("Course");


            var courseDto = await MapEntityToDTO(course);
            var response = CreateResponse(courseDto, new PaginationDto { PageNumber = 1, PageSize = 1 }, "Course");

            return Ok(response);
        }

        // GET: api/Course/name/{name}
        [HttpGet("name/{name}")]
        public async Task<ActionResult<CourseDto>> GetCourseByName(string name)
        {

            var course = await _courseService.GetByNameAsync(name);

            if (course == null) return NotFoundResponse("Course");

            var courseDto = await MapEntityToDTO(course);

            var response = CreateResponse(courseDto, new PaginationDto { PageNumber = 1, PageSize = 1 }, "Course");
            return Ok(response);

        }

        #endregion

        #region Add 

        // POST: api/Course
        [HttpPost]
        public async Task<ActionResult<CourseDto>> AddCourse([FromBody] CourseDto courseDTO)
        {

            var course = _courseMappingService.MapDtoToEntity(courseDTO);
            await _courseService.AddAsync(course);

            var result = await _courseMappingService.MapEntityToDto(course);

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

            course = _courseMappingService.MapDtoToEntity(courseDto);


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

            var courseDto = await _courseMappingService.MapEntityToDto(course);

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

            course = _courseMappingService.MapDtoToEntity(courseDto);

            await _courseService.UpdateAsync(course);

            return NoContent();
        }
        #endregion



    }
}
