using Microsoft.AspNetCore.Mvc;
using MindMission.Application.Factories;
using MindMission.Application.Interfaces.Azure_services;
using MindMission.Application.Interfaces.Services;
using MindMission.Domain.Constants;

namespace MindMission.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AzureController : ControllerBase
    {
        private readonly IUploadImageService _imageUploadService;
        private readonly IUploadVideoService _videoUploadService;
        private readonly IUploadAttachmentService _attachmentUploadService;
        private readonly IDeleteService _deleteService;

        public AzureController(
                                IUploadImageService imageUploadService,
                                IUploadVideoService videoUploadService,
                                IUploadAttachmentService attachmentUploadService,
                                IDeleteService deleteService)
        {
            _imageUploadService = imageUploadService;
            _videoUploadService = videoUploadService;
            _attachmentUploadService = attachmentUploadService;
            _deleteService = deleteService;
        }
        // POST: api/Azure/Video
        [HttpPost("Video")]
        public async Task<IActionResult> UploadVideo(IFormFile file)
        {
            return await UploadFile(file, _videoUploadService, "Video");
        }

        // POST: api/Azure/Image
        [HttpPost("Image")]
        public async Task<IActionResult> UploadImage(IFormFile file)
        {
            return await UploadFile(file, _imageUploadService, "Image");

        }

        // POST: api/Azure/Attachment
        [HttpPost("Attachment")]
        public async Task<IActionResult> UploadAttachment(IFormFile file)
        {
            return await UploadFile(file, _attachmentUploadService, "Attachment");

        }


        // Delete: api/Azure/Video
        [HttpDelete("Video")]
        public async Task<IActionResult> DeleteVideo(string fileUrl)
        {
            return await DeleteFile(fileUrl, "VideoContainer");

        }

        // Delete: api/Azure/Image
        [HttpDelete("Image")]
        public async Task<IActionResult> DeleteImage(string fileUrl)
        {
            return await DeleteFile(fileUrl, "PhotosContainer");

        }
        // Delete: api/Azure/Attachment
        [HttpDelete("Attachment")]
        public async Task<IActionResult> DeleteAttachment(string fileUrl)
        {
            return await DeleteFile(fileUrl, "AttachmentContainer");

        }


        private async Task<IActionResult> UploadFile(IFormFile file, IUpload uploadService, string fileType)
        {
            string uploadedFileUrl = await uploadService.Upload(file);

            if (string.IsNullOrEmpty(uploadedFileUrl))
            {
                string failedMessage = ErrorMessages.ValidationFailed;
                var failedResponse = ResponseObjectFactory.CreateResponseObject(false, failedMessage, new List<string>());
                return BadRequest(failedResponse);
            }

            string message = string.Format(SuccessMessages.UploadedSuccessfully, fileType);
            var response = ResponseObjectFactory.CreateResponseObject(true, message, new List<string> { uploadedFileUrl });
            return Ok(response);
        }

        private async Task<IActionResult> DeleteFile(string fileUrl, string containerName)
        {
            try
            {
                await _deleteService.Delete(containerName, fileUrl);

                string message = "File deleted successfully";
                var response = ResponseObjectFactory.CreateResponseObject(true, message, new List<string>());
                return Ok(response);
            }
            catch (Exception ex)
            {
                string errorMessage = $"Error deleting file: {ex.Message}";
                var response = ResponseObjectFactory.CreateResponseObject(false, errorMessage, new List<string>());
                return BadRequest(response);
            }
        }
    }
}
