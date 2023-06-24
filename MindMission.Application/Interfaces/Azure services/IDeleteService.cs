namespace MindMission.Application.Interfaces.Azure_services
{
    public interface IDeleteService
    {
        public Task Delete(string containerName, string fileUrl);

    }
}
