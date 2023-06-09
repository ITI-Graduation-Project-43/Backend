using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting.Internal;
using MindMission.API.Controllers.Base;
using MindMission.Application.DTOs;
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
        private readonly InstructorMappingService _instructorMappingService;
        /* private readonly IWebHostEnvironment _environment;*/
        private readonly BlobServiceClient blobServiceClient;
        private readonly BlobContainerClient containerClient;
        private readonly IWebHostEnvironment hostingEnvironment;

        public InstructorController(MindMissionDbContext context, InstructorMappingService instructorMappingService, IInstructorService instructorService, BlobServiceClient blobServiceClient, IConfiguration configuration, IWebHostEnvironment hostingEnvironment) : base(instructorMappingService)
        {
            _context = context;
            _instructorService = instructorService;
            _instructorMappingService = instructorMappingService;
            this.blobServiceClient = blobServiceClient;
            this.hostingEnvironment = hostingEnvironment;

            string containerName = configuration["AzureStorage:ContainerName2"];
            containerClient = blobServiceClient.GetBlobContainerClient(containerName);
        }

        #region get

        [HttpGet]
        public async Task<ActionResult<IQueryable<InstructorDto>>> GetAllInstructors([FromQuery] PaginationDto pagination)
        {
            return await GetEntitiesResponse(() => _instructorService.GetAllAsync(), pagination, "Instructors");
        }

        [HttpGet("TopTenInstructors")]
        public async Task<ActionResult<IEnumerable<InstructorDto>>> GetTopTenInstructors([FromQuery] PaginationDto pagination)
        {
            return await GetEntitiesResponse(() => _instructorService.GetTopInstructorsAsync(), new PaginationDto { PageNumber = 1, PageSize = 10 }, "Top 10 Instructors");
        }

        [HttpGet("{instructorId}")]
        public async Task<ActionResult<InstructorDto>> GetById(string instructorId)
        {
            /*var instructor = await _instructorService.GetByIdAsync(instructorId, i => i.Courses);
            if (instructor is null) return NotFoundResponse("instructor");
            var instructorDto = await MapEntityToDTO(instructor);
            var response = CreateResponse(instructorDto, new PaginationDto { PageNumber = 1, PageSize = 1 }, "instructor");
            return Ok(response);*/
            return await GetEntityResponse(() => _instructorService.GetByIdAsync(instructorId, i => i.Courses), "Instructor");
        }

        #endregion get

        [HttpPatch("{instructorId}")]
        public async Task<ActionResult> UpdateInstructor(string instructorId, InstructorDto instructorDto)
        {
            return await UpdateEntityResponse(_instructorService.GetByIdAsync, _instructorService.UpdateAsync, instructorId, instructorDto, "instructor");
        }

        /*  [HttpPost("UploadImage")]
          public async Task<ActionResult> UploadImage()
          {
              bool Results = false;
              try
              {
                  var _uploadedfiles = Request.Form.Files;
                  foreach (IFormFile source in _uploadedfiles)
                  {
                      string Filename = source.FileName;
                      string Filepath = GetFilePath(Filename);

                      if (!System.IO.Directory.Exists(Filepath))
                      {
                          System.IO.Directory.CreateDirectory(Filepath);
                      }

                      string imagepath = Filepath + "\\image.png";

                      if (System.IO.File.Exists(imagepath))
                      {
                          System.IO.File.Delete(imagepath);
                      }
                      using (FileStream stream = System.IO.File.Create(imagepath))
                      {
                          await source.CopyToAsync(stream);
                          Results = true;
                      }
                  }
              }
              catch (Exception ex)
              {
              }
              return Ok(Results);
          }

          [NonAction]
          private string GetFilePath(string id)
          {
              return this._environment.WebRootPath + "\\Upload\\instructor\\" + id;
          }*/

        [HttpPost("UploadImage")]
        public async Task<IActionResult> UploadProfilePicture(IFormFile ProfilePictureFile, string instructorId)
        {
            Instructor instructor = await _instructorService.GetByIdAsync(instructorId);
            if (instructor == null)
            {
                return NotFound("Instructor not found.");
            }
            if (ProfilePictureFile == null || ProfilePictureFile.Length == 0)
            {
                return BadRequest("No file was uploaded.");
            }

            string fileName = Guid.NewGuid().ToString() + Path.GetExtension(ProfilePictureFile.FileName);

            BlobClient blobClient = containerClient.GetBlobClient(fileName);
            using (Stream stream = ProfilePictureFile.OpenReadStream())
            {
                await blobClient.UploadAsync(stream, true);
            }

            instructor.ProfilePicture = blobClient.Uri.ToString();

            _context.Entry(instructor).Property(x => x.ProfilePicture).IsModified = true;

            // Save the updated entity
            await _context.SaveChangesAsync();

            return Ok(instructor.ProfilePicture);
        }

    }
}