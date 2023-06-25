using Azure.Storage.Blobs;
using MindMission.Application.Interfaces.Azure_services;
using MindMission.Application.Exceptions;
using Azure.Storage.Blobs.Models;
using Microsoft.Extensions.Configuration;
using Azure.Storage.Sas;
using Microsoft.AspNetCore.Mvc;
using System.IO;

namespace MindMission.Application.Services.Upload
{
    public class DownloadService : IDownloadService
    {
        private readonly BlobServiceClient _blobServiceClient;
        private readonly IConfiguration _configuration;
        public DownloadService(BlobServiceClient blobServiceClient, IConfiguration configuration)
        {
            _blobServiceClient = blobServiceClient;
            _configuration = configuration;
        }
        public async Task<BlobDownloadInfo> Download(string containerName, string fileUrl)
        {
            string container = _configuration[$"AzureStorage:{containerName}"];
            BlobContainerClient containerClient = _blobServiceClient.GetBlobContainerClient(container);

            if (!await containerClient.ExistsAsync())
            {
                throw new NotFoundException($"Container {container} not found");
            }

            // Check if the fileUrl is a full URL, if so, extract the file name
            if (Uri.IsWellFormedUriString(fileUrl, UriKind.Absolute))
            {
                Uri uri = new Uri(fileUrl);
                fileUrl = Path.GetFileName(uri.LocalPath);
            }

            BlobClient blobClient = containerClient.GetBlobClient(fileUrl);

            if (!await blobClient.ExistsAsync())
            {
                throw new NotFoundException($"The requested file {fileUrl} does not exist in {container}.");
            }
            else
            {
                BlobDownloadInfo download = await blobClient.DownloadAsync();
                return download;
            }
        }

    }

}