using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using MindMission.Application.DTOs;
using MindMission.Application.Interfaces.Services;
using MindMission.Application.Mapping;
using MindMission.Application.Repository_Interfaces;
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
            if (attachmentDto == null)
            {
                return BadRequest("Entered Attachment is required");
            }

            if (ModelState.IsValid)
            {
                Lesson AttachmentLesson = await _attachmentService.GetAttachmentLesson(attachmentDto.LessonId);
                if (AttachmentLesson != null)
                {
                    Attachment Attachment = _attachmentMappingService.MappingDtoToAttachment(attachmentDto);
                    return Ok(await _attachmentService.AddAttachmentAsync(Attachment, attachmentDto.File, AttachmentLesson));
                }

                return BadRequest("Non-Existed Lesson");
            }
            return BadRequest(ModelState);
        }

        [HttpPost("Download")]
        public async Task<IActionResult> DownloadAttachment(int id)
        {
            try
            {
                await _attachmentService.DownloadAttachmentAsync(id);
                return Ok("File download successfully!");
            }
            catch
            {
                return StatusCode(500, "Internal Server Error");
            }
        }
    }
}
