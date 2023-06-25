using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Http;


namespace MindMission.Application.Interfaces.Services
{
    public interface IUploadService
    {
        public Task<string> Upload(IFormFile file);

    }
    public interface IUploadImageService : IUploadService { }
    public interface IUploadVideoService : IUploadService { }
    public interface IUploadAttachmentService : IUploadService { }
}
