using Azure.Storage.Blobs;
using Microsoft.Extensions.Configuration;
using MindMission.Application.Interfaces.Azure_services;


namespace MindMission.Application.Services.Azure_services
{

    public class DeleteUploadedServices : IDeleteService
    {
        private readonly BlobServiceClient _blobServiceClient;
        private readonly IConfiguration _configuration;
        public DeleteUploadedServices(BlobServiceClient blobServiceClient, IConfiguration configuration)
        {
            _blobServiceClient = blobServiceClient;
            _configuration = configuration;
        }
        public async Task Delete(string containerName, string fileUrl)
        {

            string container = _configuration[$"AzureStorage:{containerName}"];
            BlobContainerClient containerClient = _blobServiceClient.GetBlobContainerClient(container);
            BlobClient blobClient = containerClient.GetBlobClient(fileUrl);

            await blobClient.DeleteIfExistsAsync();

        }
    }

}
