using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MindMission.API.Controllers.Base;
using MindMission.Application.DTOs;
using MindMission.Application.Interfaces.Services;
using MindMission.Application.Mapping;
using MindMission.Domain.Models;

namespace MindMission.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : BaseController<Student, StudentDto,string>
    {
        private readonly IStudentService _StudentService;
        private readonly StudentMappingService _StudentMappingService;
        public StudentController(IStudentService context, StudentMappingService Service):base(Service)
        {
            _StudentService = context;
            _StudentMappingService = Service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<StudentDto>>> GetAllStudent([FromQuery] PaginationDto pagination)
        {
            return await GetEntitiesResponse(() => _StudentService.GetAllAsync(), pagination, "Students");
        }

        [HttpGet("{StudentID}")]
        public async Task<ActionResult<InstructorDto>> GetById(string StudentID)
        {
            return await GetEntityResponse(() => _StudentService.GetByIdAsync(StudentID), "Student");
        }

        [HttpPatch("{StudnetId}")]
        public async Task<ActionResult> UpdateInstructor(string StudnetId, StudentDto StudentDto)
        {
            return await UpdateEntityResponse(_StudentService.GetByIdAsync, _StudentService.UpdateAsync, StudnetId, StudentDto, "Student");
        }
    }
}
