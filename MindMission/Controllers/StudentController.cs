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
            var Students = await _StudentService.GetAllAsync();
            if (!Students.Any()) return NotFoundResponse("Students");
            var StudentDTOs = await MapEntitiesToDTOs(Students);
            var response = CreateResponse(StudentDTOs, pagination, "Students");
            return Ok(response);
        }

        [HttpGet("{StudentID}")]
        public async Task<ActionResult<InstructorDto>> GetById(string StudentID)
        {
            var Student = await _StudentService.GetByIdAsync(StudentID);
            if (Student is null) return NotFoundResponse("Student");
            var StudentDto = await MapEntityToDTO(Student);
            var response = CreateResponse(StudentDto, new PaginationDto { PageNumber = 1, PageSize = 1 }, "Student");
            return Ok(response);
        }

        [HttpPatch("{StudnetId}")]
        public async Task<ActionResult> UpdateInstructor(string StudnetId, StudentDto StudentDto)
        {
            if (StudnetId != StudentDto.Id) return BadRequest();
            var Student = await _StudentService.GetByIdAsync(StudnetId);
            if (Student is null) return NotFound();
            Student = _StudentMappingService.MapDtoToEntity(StudentDto);
            await _StudentService.UpdateAsync(Student);
            return NoContent();
        }
    }
}
