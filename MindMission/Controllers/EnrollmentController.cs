using Microsoft.AspNetCore.Mvc;
using MindMission.API.Controllers.Base;
using MindMission.Application.DTOs;
using MindMission.Application.Factories;
using MindMission.Application.Mapping;
using MindMission.Application.Service_Interfaces;
using MindMission.Domain.Constants;
using MindMission.Domain.Models;

namespace MindMission.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnrollmentController : BaseController<Enrollment, EnrollmentDto, int>
    {
        private readonly IEnrollmentService _EnrollmentService;
        private readonly EnrollmentMappingService _EnrollmentMappingService;

        public EnrollmentController(IEnrollmentService EnrollmentService, EnrollmentMappingService EnrollmentMappingService) : base(EnrollmentMappingService)
        {
            _EnrollmentService = EnrollmentService ?? throw new ArgumentNullException(nameof(EnrollmentService));
            _EnrollmentMappingService = EnrollmentMappingService ?? throw new ArgumentNullException(nameof(EnrollmentMappingService));
        }

        #region GET

        // GET: api/Enrollment
        [HttpGet]
        public async Task<ActionResult<IQueryable<EnrollmentDto>>> GetAllEnrollment([FromQuery] PaginationDto pagination)
        {
            return await GetEntitiesResponseWithIncludePagination(_EnrollmentService.GetAllAsync, _EnrollmentService.GetTotalCountAsync, pagination, "Enrollments", e => e.Course, e => e.Course.Instructor, e => e.Course.Category, e => e.Student);
        }

        [HttpGet("SuccessfulLearners")]
        public async Task<ActionResult> SuccessfulLearners()
        {
            var TotalSuccessfulLearners = await _EnrollmentService.SuccessfulLearners();
            return Ok(ResponseObjectFactory.CreateResponseObject(true, "Registration Succeeded", new List<int>() { TotalSuccessfulLearners }));
        }

        // GET: api/Enrollment/Student/{StudentId}

        [HttpGet("Student/{StudentId}")]
        public async Task<ActionResult<IEnumerable<EnrollmentDto>>> GetEnrollmentsByStudentId(string StudentId, [FromQuery] PaginationDto pagination)
        {
            return await GetEntitiesResponsePagination(() => _EnrollmentService.GetAllByStudentIdAsync(StudentId, pagination.PageNumber, pagination.PageSize), _EnrollmentService.GetTotalCountAsync, pagination, "Enrollments");
        }

        // GET: api/Enrollment/Course/{CourseId}

        [HttpGet("Course/{CourseId}")]
        public async Task<ActionResult<IEnumerable<EnrollmentDto>>> GetEnrollmentsByCourseId(int CourseId, [FromQuery] PaginationDto pagination)
        {
            return await GetEntitiesResponsePagination(() => _EnrollmentService.GetAllByCourseIdAsync(CourseId, pagination.PageNumber, pagination.PageSize), _EnrollmentService.GetTotalCountAsync, pagination, "Enrollments");
        }

        // GET: api/Enrollment/{Id}
        [HttpGet("{Id}", Name = "GetEnrollmentById")]
        public async Task<ActionResult<EnrollmentDto>> GetEnrollmentById(int Id)
        {
            return await GetEntityResponse(() => _EnrollmentService.GetByIdAsync(Id, e => e.Course, e => e.Course.Instructor, e => e.Course.Category, e => e.Student), "Enrollment");
        }

        [HttpGet("Student/{StudentId}/Course/{CourseId}")]
        public async Task<ActionResult<EnrollmentDto>> GetEnrollmentsByStudentAndCourse(string StudentId, int CourseId)
        {

            var enrollment = await _EnrollmentService.GetByStudentAndCourseAsync(StudentId, CourseId);

            if (enrollment == null)
            {
                return NotFound(NotFoundResponse("Enrollments"));
            }


            string message = string.Format(SuccessMessages.RetrievedSuccessfully, "Enrollments");
            var response = ResponseObjectFactory.CreateResponseObject(true, message, new List<EnrollmentDto> { enrollment });
            return Ok(response);
        }

        #endregion GET

        #region Add

        // POST: api/Enrollment
        [HttpPost]
        public async Task<ActionResult<EnrollmentDto>> AddEnrollment([FromBody] EnrollmentDto EnrollmentDTO)
        {
            var enrollment = _EnrollmentMappingService.MapDtoToEntity(EnrollmentDTO);
            var addedEnrollment = _EnrollmentService.AddAsync(enrollment);
            return Ok(new
            {
                EnrollmentId = addedEnrollment.Id
            });
        }

        #endregion Add

        #region Delete

        // DELETE: api/Enrollment/Delete/{EnrollmentId}
        [HttpDelete("Delete/{EnrollmentId}")]
        public async Task<IActionResult> DeleteEnrollment(int EnrollmentId)
        {
            return await DeleteEntityResponse(_EnrollmentService.GetByIdAsync, _EnrollmentService.DeleteAsync, EnrollmentId);
        }

        // DELETE: api/Enrollment/{id}

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {

            var course = await _EnrollmentService.GetByIdAsync(id);

            if (course == null)
                return NotFound(NotFoundResponse("Course"));
            await _EnrollmentService.SoftDeleteAsync(id);
            return NoContent();
        }

        #endregion Delete

        #region Edit Put

        // PUT: api/Enrollment/{EnrollmentId}
        [HttpPut("{EnrollmentId}")]
        public async Task<ActionResult> UpdateEnrollment(int EnrollmentId, EnrollmentDto EnrollmentDto)
        {
            return await UpdateEntityResponse(_EnrollmentService.GetByIdAsync, _EnrollmentService.UpdateAsync, EnrollmentId, EnrollmentDto, "Enrollment");
        }

        #endregion Edit Put
    }
}