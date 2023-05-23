using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MindMission.API.Controllers.Base;
using MindMission.Domain.Models;
using MindMission.Application.DTOs;
using MindMission.Application.Mapping;
using MindMission.Application.Service_Interfaces;
using MindMission.Application.Services;

namespace MindMission.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InstructorController : BaseController<Instructor, InstructorDto>
    {
        private readonly IInstructorService _instructorService;
        private readonly InstructorMappingService _instructorMappingService;

        public InstructorController(InstructorMappingService instructorMappingService, IInstructorService instructorService) : base(instructorMappingService)
        {
            _instructorService = instructorService;
            _instructorMappingService = instructorMappingService;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<InstructorDto>>> GetAllInstructors([FromQuery] PaginationDto pagination)
        {
            var instructors = await _instructorService.GetAllAsync();
            if (instructors is null) return NotFoundResponse("Instructors");
            var instructorDTOs = await MapEntitiesToDTOs(instructors);
            var response = CreateResponse(instructorDTOs, pagination, "instructors");
            return Ok(response);
        }

        [HttpGet("TopTenInstructors")]
        public async Task<ActionResult<IEnumerable<InstructorDto>>> GetTopTenInstructors([FromQuery] PaginationDto pagination)
        {
            var instructors = await _instructorService.GetTopInstructorsAsync();
            if (instructors is null) return NotFoundResponse("Instructors");
            var instructorDTOs = await MapEntitiesToDTOs(instructors);
            var response = CreateResponse(instructorDTOs, new PaginationDto { PageNumber = 1, PageSize = 10 }, "instructors");
            return Ok(response);
        }


        [HttpGet("instructorID")]
        public async Task<ActionResult<InstructorDto>> GetById(string InstructorId) 
        {
            var instructor= await _instructorService.GetByIdAsync(InstructorId);
            if (instructor is null) return NotFoundResponse("instructor");
            var instructorDto= await MapEntityToDTO(instructor);
            var response = CreateResponse(instructorDto, new PaginationDto { PageNumber = 1, PageSize = 1 }, "instructor");
            return Ok(response);
        }

    }
}
