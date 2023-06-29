using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Mvc;
using MindMission.API.Controllers.Base;
using MindMission.API.Utilities;
using MindMission.Application.DTOs;
using MindMission.Application.Factories;
using MindMission.Application.Interfaces.Services;
using MindMission.Application.Mapping;
using MindMission.Application.Mapping.Base;
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


        public StudentController(MindMissionDbContext context, IStudentService studentService, IMappingService<Student, StudentDto> studentMappingService, BlobServiceClient blobServiceClient, IConfiguration configuration) : base(studentMappingService)
        {
            _context = context;
            _studentService = studentService ?? throw new ArgumentNullException(nameof(studentService));
            string containerName = configuration["AzureStorage:PhotosContainer"];
            containerClient = blobServiceClient.GetBlobContainerClient(containerName);
        }

        [HttpGet]
        public async Task<ActionResult<IQueryable<StudentDto>>> GetAllStudent([FromQuery] PaginationDto pagination)
        {

            return await GetEntitiesResponseWithIncludePagination(
              _studentService.GetAllAsync,
              _studentService.GetTotalCountAsync,
              pagination,
              "Students",
              student => student.User,
              Student => Student.Enrollments,
              Student => Student.Wishlists
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
            return await GetEntitiesResponsePagination(() => _studentService.GetRecentStudentEnrollmentAsync(recentNumber, courseId), _studentService.GetTotalCountAsync, pagination, "Students");
        }

        [HttpPost("UploadImage")]
        public async Task<IActionResult> UploadProfilePicture(IFormFile ProfilePictureFile, string id)
        {
            var student = await _studentService.GetByIdAsync(id);
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
            if (student.ProfilePicture != null && result > 0)
            {
                var link = new List<string>() { student.ProfilePicture };
                return Ok(ResponseObjectFactory.CreateResponseObject(true, "The image is updated successfully", link));
            }
            return Ok(ResponseObjectFactory.CreateResponseObject(false, "Some wrong during saving your image", new List<string>()));
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult> UpdateStudentAsync(string id, StudentDto StudentDto)
        {
            if (ModelState.IsValid)
            {
                var Student = await _studentService.GetByIdAsync(id);
                if (Student != null)
                {
                    Student.FirstName = StudentDto.FirstName;
                    Student.LastName = StudentDto.LastName;
                    Student.Bio = StudentDto.Bio;
                    await _studentService.UpdatePartialAsync(id, Student);
                    return Ok(ResponseObjectFactory.CreateResponseObject(true, "This student is updated successfully", new List<string>()));
                }
                return BadRequest(ResponseObjectFactory.CreateResponseObject(false, "This student is not found", new List<string>()));
            }
            return BadRequest(ResponseObjectFactory.CreateResponseObject(false, ModelStateErrors.BadRequestError(ModelState), new List<string>()));
        }

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