using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MindMission.API.Controllers.Base;
using MindMission.Domain.Models;
using MindMission.Application.DTOs;
using MindMission.Application.Mapping;
using MindMission.Application.Service_Interfaces;
using MindMission.Application.Services;
using System;
using MindMission.Application.Interfaces.Services;

namespace MindMission.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InstructorController : BaseController<Instructor, InstructorDto,string>
    {
        private readonly IInstructorService _instructorService;
        private readonly InstructorMappingService _instructorMappingService;
       /* private readonly IWebHostEnvironment _environment;*/


        public InstructorController(InstructorMappingService instructorMappingService, IInstructorService instructorService) : base(instructorMappingService)
        {
            _instructorService = instructorService;
            _instructorMappingService = instructorMappingService;
            
        }
        #region get
        [HttpGet]
        public async Task<ActionResult<IEnumerable<InstructorDto>>> GetAllInstructors([FromQuery] PaginationDto pagination)
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
        #endregion
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
    }
}
