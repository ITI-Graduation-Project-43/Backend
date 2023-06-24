using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MindMission.Application.Interfaces.Azure_services;
using MindMission.Application.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
