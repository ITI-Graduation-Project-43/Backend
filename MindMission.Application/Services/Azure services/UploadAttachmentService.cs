using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Azure.Storage.Blobs;
using MindMission.Application.Interfaces.Services;

namespace MindMission.Application.Services.Upload
{
    public class UploadAttachmentService : IUploadAttachmentService
    {
        private readonly BlobContainerClient _containerClient;
        public UploadAttachmentService(BlobServiceClient blobServiceClient, IConfiguration configuration)
        {
            string containerName = configuration["AzureStorage:AttachmentContainer"];
            _containerClient = blobServiceClient.GetBlobContainerClient(containerName);
        }

        public async Task<string> Upload(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return null;
            }

            if (file.Length > 10_000_000) // 10 MB
            {
                throw new Exception("The file is too large. The maximum allowed size is 10 MB.");
            }

            var allowedExtensions = new[] { ".doc", ".docx", ".pdf", ".zip", ".txt", ".ebup", ".ppt", ".pptx", ".xls", ".xlsx", ".csv" };
            var extension = Path.GetExtension(file.FileName).ToLowerInvariant();

            if (string.IsNullOrEmpty(extension) || !allowedExtensions.Contains(extension))
            {
                throw new Exception($"Invalid file extension. Allowed extensions are: {string.Join(", ", allowedExtensions)}");
            }

            string fileName = Guid.NewGuid().ToString() + extension;
            BlobClient blobClient = _containerClient.GetBlobClient(fileName);
            using (Stream stream = file.OpenReadStream())
            {
                await blobClient.UploadAsync(stream, true);
            }

            return blobClient.Uri.ToString();
        }

    }

}