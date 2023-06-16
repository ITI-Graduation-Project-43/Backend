using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Azure.Storage.Blobs;
using MindMission.Application.Interfaces.Services;

namespace MindMission.Application.Services
{
    public class UploadImageService : IUploadImage
    {

        private readonly BlobContainerClient _containerClient;
        public UploadImageService(BlobServiceClient blobServiceClient, IConfiguration configuration)
        {
            string containerName = configuration["AzureStorage:ContainerName2"];
            _containerClient = blobServiceClient.GetBlobContainerClient(containerName);

        }
        public async Task<string> UploadImage(IFormFile imageFile)
        {
            if (imageFile == null || imageFile.Length == 0)
            {
                return null;
            }

            // Validate the file size
            if (imageFile.Length > 10_000_000) // 10 MB
            {
                throw new Exception("The file is too large. The maximum allowed size is 10 MB.");
            }

            // Validate the file extension
            var allowedExtensions = new[] { ".jpg", ".png", ".webp", ".jpeg", ".avif" };
            var extension = Path.GetExtension(imageFile.FileName).ToLowerInvariant();

            if (string.IsNullOrEmpty(extension) || !allowedExtensions.Contains(extension))
            {
                throw new Exception($"Invalid file extension. Allowed extensions are: {string.Join(", ", allowedExtensions)}");
            }

            // Upload the file and save the URL
            string fileName = Guid.NewGuid().ToString() + extension;
            BlobClient blobClient = _containerClient.GetBlobClient(fileName);
            using (Stream stream = imageFile.OpenReadStream())
            {
                await blobClient.UploadAsync(stream, true);
            }

            return blobClient.Uri.ToString();
        }

    }
}
