using Microsoft.AspNetCore.Mvc;
using MindMission.API.Controllers.Base;
using MindMission.Application.DTOs;
using MindMission.Application.Interfaces.Services;
using MindMission.Application.Mapping;
using MindMission.Application.Service_Interfaces;
using MindMission.Application.Services;
using MindMission.Application.Services_Classes;
using MindMission.Domain.Models;

namespace MindMission.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : BaseController<Student, StudentDto, string>
    {
        private readonly IStudentService _studentService;

        public StudentController(IStudentService studentService, StudentMappingService studentMappingService) : base(studentMappingService)
        {
            _studentService = studentService ?? throw new ArgumentNullException(nameof(studentService));
        }

        [HttpGet]
        public async Task<ActionResult<IQueryable<StudentDto>>> GetAllStudent([FromQuery] PaginationDto pagination)
        {

            return await GetEntitiesResponseWithInclude(
              _studentService.GetAllAsync,
              pagination,
              "Students",
              student => student.User
          );
        }

        [HttpGet("{StudentID}")]
        public async Task<ActionResult<InstructorDto>> GetById(string StudentID)
        {
            return await GetEntityResponseWithInclude(
                    () => _studentService.GetByIdAsync(StudentID,
                        instructor => instructor.User),
                     "Student");

        }


        [HttpGet("{courseId}/students/{recentNumber}")]
        public async Task<ActionResult<IQueryable<StudentDto>>> GetRecentStudents(int recentNumber, int courseId, [FromQuery] PaginationDto pagination)
        {
            return await GetEntitiesResponse(() => _studentService.GetRecentStudentEnrollmentAsync(recentNumber, courseId), pagination, "Students");
        }

        [HttpPatch("{StudnetId}")]
        public async Task<ActionResult> UpdateInstructor(string StudnetId, StudentDto StudentDto)
        {
            return await UpdateEntityResponse(_studentService.GetByIdAsync, _studentService.UpdateAsync, StudnetId, StudentDto, "Student");
        }
        // DELETE: api/Student/{id}

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {

            var course = await _studentService.GetByIdAsync(id);

            if (course == null)
                return NotFound(NotFoundResponse("Course"));
            await _studentService.SoftDeleteAsync(id);
            return NoContent();
        }
    }
}