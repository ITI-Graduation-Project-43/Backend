using Azure;
using Microsoft.AspNetCore.Mvc;
using MindMission.Application.Factories;
using MindMission.Application.Interfaces.Azure_services;
using MindMission.Application.Interfaces.Services;
using MindMission.Domain.Constants;
using MindMission.Domain.Models;

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
        private readonly IDownloadService _downloadService;

        public AzureController(
                                IUploadImageService imageUploadService,
                                IUploadVideoService videoUploadService,
                                IUploadAttachmentService attachmentUploadService,
                                IDeleteService deleteService,
                                IDownloadService downloadService)
        {
            _imageUploadService = imageUploadService;
            _videoUploadService = videoUploadService;
            _attachmentUploadService = attachmentUploadService;
            _deleteService = deleteService;
            _downloadService = downloadService;
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

        // Download: api/Azure/Video
        [HttpGet("Video")]
        public async Task<IActionResult> DownloadVideo(string fileUrl)
        {
            return await DownloadFile(fileUrl, "VideoContainer");

        }

        // Delete: api/Azure/Image
        [HttpDelete("Image")]
        public async Task<IActionResult> DeleteImage(string fileUrl)
        {
            return await DeleteFile(fileUrl, "PhotosContainer");

        }

        // Get: api/Azure/Image
        [HttpGet("Image")]
        public async Task<IActionResult> DownloadImage(string fileUrl)
        {
            return await DownloadFile(fileUrl, "PhotosContainer");

        }
        // Delete: api/Azure/Attachment
        [HttpDelete("Attachment")]
        public async Task<IActionResult> DeleteAttachment(string fileUrl)
        {
            return await DeleteFile(fileUrl, "AttachmentContainer");

        }
        // Get: api/Azure/Attachment
        [HttpGet("Attachment")]
        public async Task<IActionResult> DownloadAttachment(string fileUrl)
        {
            return await DownloadFile(fileUrl, "AttachmentContainer");

        }


        private async Task<IActionResult> UploadFile(IFormFile file, IUploadService uploadService, string fileType)
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
        private async Task<IActionResult> DownloadFile(string fileUrl, string containerName)
        {
            try
            {
                var downloadInfo = await _downloadService.Download(containerName, fileUrl);

                return File(downloadInfo.Content, downloadInfo.ContentType, Path.GetFileName(fileUrl));

            }
            catch (Exception ex)
            {
                string errorMessage = $"Error downloading file: {ex.Message}";
                var response = ResponseObjectFactory.CreateResponseObject(false, errorMessage, new List<string>());
                return BadRequest(response);
            }
        }
    }
}
