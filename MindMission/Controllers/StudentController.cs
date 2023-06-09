using Microsoft.AspNetCore.Mvc;
using MindMission.API.Controllers.Base;
using MindMission.Application.DTOs;
using MindMission.Application.Interfaces.Services;
using MindMission.Application.Mapping;
using MindMission.Application.Service_Interfaces;
using MindMission.Application.Services;
using MindMission.Domain.Models;

namespace MindMission.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : BaseController<Student, StudentDto, string>
    {
        private readonly IStudentService _StudentService;

        public StudentController(IStudentService studentService, StudentMappingService studentMappingService) : base(studentMappingService)
        {
            _StudentService = studentService ?? throw new ArgumentNullException(nameof(studentService));
        }

        [HttpGet]
        public async Task<ActionResult<IQueryable<StudentDto>>> GetAllStudent([FromQuery] PaginationDto pagination)
        {

            return await GetEntitiesResponseWithInclude(
              _StudentService.GetAllAsync,
              pagination,
              "Students",
              student => student.User
          );
        }

        [HttpGet("{StudentID}")]
        public async Task<ActionResult<InstructorDto>> GetById(string StudentID)
        {
            return await GetEntityResponseWithInclude(
                    () => _StudentService.GetByIdAsync(StudentID,
                        instructor => instructor.User),
                     "Student");

        }


        [HttpGet("{courseId}/students")]
        public async Task<ActionResult<IQueryable<StudentDto>>> GetRecentStudents(int recentNumber, int courseId, [FromQuery] PaginationDto pagination)
        {
            return await GetEntitiesResponse(() => _StudentService.GetRecentStudentEnrollmentAsync(recentNumber, courseId), pagination, "Students");
        }

        [HttpPatch("{StudnetId}")]
        public async Task<ActionResult> UpdateInstructor(string StudnetId, StudentDto StudentDto)
        {
            return await UpdateEntityResponse(_StudentService.GetByIdAsync, _StudentService.UpdateAsync, StudnetId, StudentDto, "Student");
        }

    }
}