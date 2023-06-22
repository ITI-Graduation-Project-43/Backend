using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MindMission.API.Controllers.Base;
using MindMission.Application.DTOs;
using MindMission.Application.Factories;
using MindMission.Application.Interfaces.Services;
using MindMission.Application.Mapping;
using MindMission.Application.Service_Interfaces;
using MindMission.Application.Services;
using MindMission.Application.Services_Classes;
using MindMission.Domain.Models;
using MindMission.Infrastructure.Context;

namespace MindMission.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : BaseController<Student, StudentDto, string>
    {
        private readonly MindMissionDbContext _context;
        private readonly IStudentService _studentService;
        private readonly BlobContainerClient containerClient;


        public StudentController(MindMissionDbContext context, IStudentService studentService, StudentMappingService studentMappingService, BlobServiceClient blobServiceClient, IConfiguration configuration) : base(studentMappingService)
        {
            _context = context;
            _studentService = studentService ?? throw new ArgumentNullException(nameof(studentService));
            string containerName = configuration["AzureStorage:ContainerName2"];
            containerClient = blobServiceClient.GetBlobContainerClient(containerName);
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

        [HttpPost("UploadImage")]
        public async Task<IActionResult> UploadProfilePicture(IFormFile ProfilePictureFile, string StudentId)
        {
            var student = await _studentService.GetByIdAsync(StudentId);
            if (student == null)
            {
                return BadRequest(ResponseObjectFactory.CreateResponseObject(false, "This student doesn't exist", new List<string>()));
            }
            if (ProfilePictureFile == null || ProfilePictureFile.Length == 0)
            {
                return BadRequest(ResponseObjectFactory.CreateResponseObject(false, "invalid image", new List<string>()));
            }

            string fileName = Guid.NewGuid().ToString() + Path.GetExtension(ProfilePictureFile.FileName);

            BlobClient blobClient = containerClient.GetBlobClient(fileName);
            using (Stream stream = ProfilePictureFile.OpenReadStream())
            {
                await blobClient.UploadAsync(stream, true);
            }
            student.ProfilePicture = blobClient.Uri.ToString();
            _context.Entry(student).Property(x => x.ProfilePicture).IsModified = true;
            var result = await _context.SaveChangesAsync();
            if(student.ProfilePicture != null && result >  0)
            {
                return Ok(ResponseObjectFactory.CreateResponseObject(true, "The image is updated successfully", new List<string>()));
            }
            return Ok(ResponseObjectFactory.CreateResponseObject(false, "Some wrong during saving your image", new List<string>()));

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