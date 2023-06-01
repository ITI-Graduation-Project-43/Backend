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
            return await GetEntitiesResponse(_EnrollmentService.GetAllAsync, pagination, "Enrollments");
        }

        // GET: api/Enrollment/Student/{StudentId}

        [HttpGet("Student/{StudentId}")]
        public async Task<ActionResult<IEnumerable<EnrollmentDto>>> GetEnrollmentsByStudentId(string StudentId, [FromQuery] PaginationDto pagination)
        {
            return await GetEntitiesResponse(() => _EnrollmentService.GetAllByStudentIdAsync(StudentId), pagination, "Enrollments");
        }

        // GET: api/Enrollment/Course/{CourseId}

        [HttpGet("Course/{CourseId}")]
        public async Task<ActionResult<IEnumerable<EnrollmentDto>>> GetEnrollmentsByCourseId(int CourseId, [FromQuery] PaginationDto pagination)
        {
            return await GetEntitiesResponse(() => _EnrollmentService.GetAllByCourseIdAsync(CourseId), pagination, "Enrollments");
        }

        // GET: api/Enrollment/{EnrollmentId}
        [HttpGet("{EnrollmentId}")]
        public async Task<ActionResult<EnrollmentDto>> GetEnrollmentById(int EnrollmentId)
        {
            return await GetEntityResponse(() => _EnrollmentService.GetByIdAsync(EnrollmentId), "Enrollment");
        }

        #endregion GET

        #region Add

        // POST: api/Enrollment
        [HttpPost]
        public async Task<ActionResult<EnrollmentDto>> AddEnrollment([FromBody] EnrollmentDto EnrollmentDTO)
        {
            return await AddEntityResponse(_EnrollmentService.AddAsync, EnrollmentDTO, "Enrollment", nameof(GetEnrollmentById));
        }

        #endregion Add

        #region Delete

        // DELETE: api/Enrollment/{EnrollmentId}
        [HttpDelete("{EnrollmentId}")]
        public async Task<IActionResult> DeleteEnrollment(int EnrollmentId)
        {
            return await DeleteEntityResponse(_EnrollmentService.GetByIdAsync, _EnrollmentService.DeleteAsync, EnrollmentId);
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