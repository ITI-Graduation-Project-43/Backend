using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Http;


namespace MindMission.Application.Interfaces.Services
{
    public interface IUpload
    {
        public Task<string> Upload(IFormFile file);

    }
    public interface IUploadImageService : IUpload { }
    public interface IUploadVideoService : IUpload { }
    public interface IUploadAttachmentService : IUpload { }
}
