using Azure.Storage.Blobs.Models;

namespace MindMission.Application.Interfaces.Azure_services
{
    public interface IDownloadService
    {
        Task<BlobDownloadInfo> Download(string containerName, string fileUrl);

    }
}
