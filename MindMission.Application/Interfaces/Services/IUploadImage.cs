using Microsoft.AspNetCore.Http;


namespace MindMission.Application.Interfaces.Services
{
    public interface IUploadImage
    {
        public Task<string> UploadImage(IFormFile imageFile);
    }
}
