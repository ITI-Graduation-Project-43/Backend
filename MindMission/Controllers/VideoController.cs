using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MindMission.Domain.Models;
using MindMission.Infrastructure.Context;

namespace MindMission.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VideoController : Controller
    {
        private readonly MindMissionDbContext dbContext;
        private readonly BlobContainerClient containerClient;

        public VideoController(MindMissionDbContext dbContext, BlobServiceClient blobServiceClient, IConfiguration configuration)
        {
            this.dbContext = dbContext;

            string containerName = configuration["AzureStorage:VideoContainer"];
            containerClient = blobServiceClient.GetBlobContainerClient(containerName);
        }

        [HttpPost("upload")]
        public async Task<IActionResult> UploadVideo(IFormFile videoFile, int lessonId)
        {
            if (videoFile == null || videoFile.Length == 0)
            {
                return BadRequest("No file was uploaded.");
            }

            string fileName = Guid.NewGuid().ToString() + Path.GetExtension(videoFile.FileName);

            BlobClient blobClient = containerClient.GetBlobClient(fileName);
            using (Stream stream = videoFile.OpenReadStream())
            {
                await blobClient.UploadAsync(stream, true);
            }

            Video video = new()
            {
                VideoUrl = blobClient.Uri.ToString(),
                LessonId = lessonId,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            // Save the Video object to the database
            dbContext.Videos.Add(video);
            await dbContext.SaveChangesAsync();

            return Ok(video);
        }

        [HttpGet("download/{videoId}")]
        public async Task<IActionResult> DownloadVideo(int videoId)
        {
            Video video = dbContext.Videos.FirstOrDefault(v => v.Id == videoId);

            if (video == null || string.IsNullOrEmpty(video.VideoUrl))
            {
                return NotFound();
            }

            // Get the BlobClient for the video
            BlobClient blobClient = new(new Uri(video.VideoUrl));

            if (!await blobClient.ExistsAsync())
            {
                return NotFound();
            }

            // Retrieve the video content
            var response = await blobClient.DownloadAsync();

            // Return the file stream with appropriate content type
            return File(response.Value.Content, response.Value.ContentType, Path.GetFileName(video.VideoUrl));
        }
    }
}
