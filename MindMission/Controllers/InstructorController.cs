using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Mvc;
using MindMission.API.Controllers.Base;
using MindMission.API.Utilities;
using MindMission.Application.DTOs;
using MindMission.Application.Factories;
using MindMission.Application.Mapping;
using MindMission.Application.Service_Interfaces;
using MindMission.Domain.Models;
using MindMission.Infrastructure.Context;

namespace MindMission.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InstructorController : BaseController<Instructor, InstructorDto, string>
    {
        private readonly MindMissionDbContext _context;
        private readonly IInstructorService _instructorService;
        private readonly BlobContainerClient containerClient;

        public InstructorController(MindMissionDbContext context, InstructorMappingService instructorMappingService, IInstructorService instructorService, BlobServiceClient blobServiceClient, IConfiguration configuration) : base(instructorMappingService)
        {
            _context = context;
            _instructorService = instructorService;

            string containerName = configuration["AzureStorage:photosContainer"];
            containerClient = blobServiceClient.GetBlobContainerClient(containerName);
        }

        #region get

        [HttpGet]
        public async Task<ActionResult<IQueryable<InstructorDto>>> GetAllInstructors([FromQuery] PaginationDto pagination)
        {
            return await GetEntitiesResponsePagination(
             () => _instructorService.GetAllAsync(pagination.PageNumber, pagination.PageSize),
             _instructorService.GetTotalCountAsync,
             pagination,
             "Instructors"
         );
        }

        [HttpGet("totalTopInstructor")]
        public async Task<ActionResult<IQueryable<InstructorDto>>> GetTotalTopRatedInstructorsAsync([FromQuery] PaginationDto pagination)
        {
            var TotalTopRatedInstructorsAsync = await _instructorService.GetTotalTopRatedInstructorsAsync();
            return Ok(ResponseObjectFactory.CreateResponseObject(true, "Total TopRated Instructors", new List<int>() { TotalTopRatedInstructorsAsync }));
        }

        [HttpGet("TopTenInstructors")]
        public async Task<ActionResult<IQueryable<InstructorDto>>> GetTopTenInstructors(int topNumber, [FromQuery] PaginationDto pagination)
        {

            return await GetEntitiesResponsePagination(() => _instructorService.GetTopRatedInstructorsAsync(topNumber, pagination.PageNumber, pagination.PageSize), _instructorService.GetTotalCountAsync, pagination, "Top {topNumber} Instructors");
        }

        [HttpGet("{instructorId}")]
        public async Task<ActionResult<InstructorDto>> GetById(string instructorId)
        {
            return await GetEntityResponseWithInclude(
                    () => _instructorService.GetByIdAsync(instructorId,
                        instructor => instructor.User,
                        instructor => instructor.Courses),
                     "Instructor");

        }

        #endregion get

        #region Update
        [HttpPatch("{id}")]
        public async Task<ActionResult> UpdateInstructorAsync(string id, InstructorDto InstructorDto)
        {
            if (ModelState.IsValid)
            {
                var Instructor = await _instructorService.GetByIdAsync(id);
                if (Instructor != null)
                {
                    Instructor.FirstName = InstructorDto.FirstName;
                    Instructor.LastName = InstructorDto.LastName;
                    Instructor.Bio = InstructorDto.Bio;
                    Instructor.Title = InstructorDto.Title;
                    Instructor.Description = InstructorDto.Description;
                    await _instructorService.UpdatePartialAsync(id, Instructor);
                    return Ok(ResponseObjectFactory.CreateResponseObject(true, "This instructor is updated successfully", new List<string>()));
                }
                return BadRequest(ResponseObjectFactory.CreateResponseObject(false, "This instructor is not found", new List<string>()));
            }
            return BadRequest(ResponseObjectFactory.CreateResponseObject(false, ModelStateErrors.BadRequestError(ModelState), new List<string>()));
        }

        [HttpPost("UploadImage")]
        public async Task<IActionResult> UploadProfilePicture(IFormFile ProfilePictureFile, string id)
        {
            Instructor instructor = await _instructorService.GetByIdAsync(id);
            if (instructor == null)
            {
                return BadRequest(ResponseObjectFactory.CreateResponseObject(false, "This instructor doesn't exist", new List<string>()));
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

            instructor.ProfilePicture = blobClient.Uri.ToString();

            _context.Entry(instructor).Property(x => x.ProfilePicture).IsModified = true;
            var result = await _context.SaveChangesAsync();
            if (instructor.ProfilePicture != null && result > 0)
            {
                var link = new List<string>() { instructor.ProfilePicture };
                return Ok(ResponseObjectFactory.CreateResponseObject(true, "The image is updated successfully", link));
            }
            return Ok(ResponseObjectFactory.CreateResponseObject(false, "Some wrong during saving your image", new List<string>()));
        }
        #endregion

        #region Delete
        // DELETE: api/Instructor/{id}

        [HttpDelete("{id}")]
        public async Task<ActionResult<IQueryable<InstructorDto>>> Delete(string id, [FromQuery] PaginationDto pagination)
        {

            var course = await _instructorService.GetByIdAsync(id);

            if (course == null)
                return NotFound(NotFoundResponse("Course"));
            await _instructorService.SoftDeleteAsync(id);
            return await GetAllInstructors(pagination);
        }

        #endregion

    }
}