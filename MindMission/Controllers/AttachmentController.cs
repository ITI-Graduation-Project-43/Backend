using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using MindMission.Application.DTOs;
using MindMission.Application.Interfaces.Services;
using MindMission.Application.Repository_Interfaces;

namespace MindMission.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttachmentController : ControllerBase
    {
        private readonly IAttachmentService _attachmentService;

        public AttachmentController(IAttachmentService attachmentService)
        {
            _attachmentService = attachmentService;
        }

        [HttpPost]
        public async Task<IActionResult> PostAttachment([FromForm] AttachmentDto attachmentDto)
        {
            if (attachmentDto == null)
            {
                return BadRequest("Entered Attachment is required");
            }

            if (ModelState.IsValid)
            {
                bool IsExistedLesson = await _attachmentService.IsExistedLesson(attachmentDto.LessonId);
                if(IsExistedLesson)
                    return Ok(await _attachmentService.AddAttachmentAsync(attachmentDto));
                return BadRequest("NotExisted Lesson");
            }
            return BadRequest(ModelState);
        }
    }
}
