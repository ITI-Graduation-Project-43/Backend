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
    public class CourseController : BaseController<Course, CourseDto, int>
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
            return await GetEntitiesResponseWithInclude(
               _courseService.GetAllAsync,
               pagination,
               "Courses",
               course => course.Instructor,
               Course => Course.Category,
               Course => Course.Chapters
           );
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

        // GET: api/Course/{courseId}
        [HttpGet("{courseId}")]
        public async Task<ActionResult<CourseDto>> GetCourseById(int courseId)
        {
            return await GetEntityResponseWithInclude(
                    () => _courseService.GetByIdAsync(courseId,
                        course => course.Instructor,
                        Course => Course.Category,
                        Course => Course.Chapters
                    ),
                    "Course"
                );
        }

        // GET: api/Course/name/{name}
        [HttpGet("name/{name}")]
        public async Task<ActionResult<CourseDto>> GetCourseByName(string name)
        {
            return await GetEntityResponse(() => _courseService.GetByNameAsync(name), "Course");
        }

        #endregion Get

        #region Add

        // POST: api/Course
        [HttpPost]
        public async Task<ActionResult<CourseDto>> AddCourse([FromBody] CourseDto courseDTO)
        {
            return await AddEntityResponse(_courseService.AddAsync, courseDTO, "Course", nameof(GetCourseById));
        }

        #endregion Add

        #region Delete

        // DELETE: api/Course/{courseId}
        [HttpDelete("{courseId}")]
        public async Task<IActionResult> DeleteCourse(int courseId)
        {
            return await DeleteEntityResponse(_courseService.GetByIdAsync, _courseService.DeleteAsync, courseId);
        }

        #endregion Delete

        #region Edit Patch/Put

        // PUT: api/Course/{courseId}
        [HttpPut("{courseId}")]
        public async Task<ActionResult> UpdateCourse(int courseId, CourseDto courseDto)
        {
            return await UpdateEntityResponse(_courseService.GetByIdAsync, _courseService.UpdateAsync, courseId, courseDto, "Course");
        }

        // PATCH: api/Course/{courseId}
        [HttpPatch("{courseId}")]
        public async Task<ActionResult> PatchCourse(int courseId, [FromBody] JsonPatchDocument<CourseDto> patchDocument)
        {
            if (patchDocument == null)
            {
                return BadRequest();
            }

            return await PatchEntityResponse(_courseService.GetByIdAsync, _courseService.UpdateAsync, courseId, patchDocument);
        }

        #endregion Edit Patch/Put
    }
}