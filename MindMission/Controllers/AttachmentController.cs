using Microsoft.AspNetCore.Mvc;
using MindMission.Application.DTOs;
using MindMission.Application.Factories;
using MindMission.Application.Interfaces.Services;
using MindMission.Application.Mapping;
using MindMission.Application.Services;
using MindMission.Domain.Models;

namespace MindMission.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttachmentController : ControllerBase
    {
        private readonly IAttachmentService _attachmentService;
        private readonly IAttachmentMappingService _attachmentMappingService;

        public AttachmentController(IAttachmentService attachmentService,
            IAttachmentMappingService attachmentMappingService)
        {
            _attachmentService = attachmentService;
            _attachmentMappingService = attachmentMappingService;
        }

        [HttpPost("Upload")]
        public async Task<IActionResult> PostAttachment([FromForm] AttachmentDto attachmentDto)
        {
            if (attachmentDto == null || attachmentDto.File.Length == 0)
            {
                return BadRequest(new
                {
                    Message = "Entered Attachment is required"
                });
            }

            if (Path.GetExtension(attachmentDto.File.FileName).ToUpper() != $".{attachmentDto.FileType}")
            {
                return BadRequest(new
                {
                    Message = $"upload '{attachmentDto.FileType}' files"
                });
            }

            if (ModelState.IsValid)
            {
                Lesson AttachmentLesson = await _attachmentService.GetAttachmentLessonByIdAsync(attachmentDto.LessonId);
                if (AttachmentLesson != null)
                {
                    try
                    {
                        Attachment Attachment = _attachmentMappingService.MappingDtoToAttachment(attachmentDto);
                        await _attachmentService.AddAttachmentAsync(Attachment, attachmentDto.File, AttachmentLesson);
                        var FileDataDto = _attachmentMappingService.AttachmentToFileDetailsDto(Attachment);

                        var Response = ResponseObjectFactory
                        .CreateResponseObject<FileDetailsDto>(true,
                        $"'{attachmentDto.File.FileName}' uploaded Successfully",
                        FileDataDto,
                        1, 10);
                        return Ok(Response);
                    }
                    catch
                    {
                        return StatusCode(500, "Internal Server Error");
                    }
                }
                return BadRequest(new
                {
                    Message = "Non-Existing Lesson"
                });
            }
            return BadRequest(ModelState);
        }

        [HttpPost("Server/Download/{id:int}")]
        public async Task<IActionResult> DownloadAttachmentForServer(int id)
        {
            if (id > 0)
            {
                Attachment Attachment = await _attachmentService.GetAttachmentByIdAsync(id);
                if (Attachment != null)
                {
                    try
                    {
                        await _attachmentService.DownloadAttachmentAsync(Attachment);
                        var FileDataDto = _attachmentMappingService.AttachmentToFileDetailsDto(Attachment);

                        var Response = ResponseObjectFactory
                            .CreateResponseObject<FileDetailsDto>(true,
                            $"'{Attachment.Name}' downloaded Successfully",
                            FileDataDto,
                            1, 10);
                        return Ok(Response);
                    }
                    catch
                    {
                        return StatusCode(500, "Internal Server Error");
                    }
                }
                return BadRequest(new
                {
                    Message = "Non-Existing Attachment"
                });
            }
            return BadRequest(new
            {
                Message = "Invalid Attachment Id"
            });
        }

        [HttpGet("Client/Download/{id:int}")]
        public async Task<IActionResult> DownloadAttachmentForClient(int id)
        {
            if (id > 0)
            {
                Attachment Attachment = await _attachmentService.GetAttachmentByIdAsync(id);
                if (Attachment != null)
                {
                    try
                    {
                        //////
                        //return File(file content in bytes -from database- , MIME type -generic- , filename)
                        //////
                        //return File(Attachment.FileData, "application/octet-stream", Attachment.FileName);
                        return Ok("try to azure it better");
                    }
                    catch
                    {
                        return StatusCode(500, "Internal Server Error");
                    }
                }
                return NotFound(new
                {
                    Message = "Non-Existing Attachment"
                });
            }
            return BadRequest(new
            {
                Message = "Invalid Id"
            });
        }


    }
}