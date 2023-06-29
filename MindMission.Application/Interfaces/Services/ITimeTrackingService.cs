using MindMission.Application.Interfaces.Repository;


namespace MindMission.Application.Interfaces.Services
{
    public  interface ITimeTrackingService : ITrackingTimeRepository
    {
        object GetCourseVisitCount(int courseId, int pageNumber, int pageSize);
        
        Task<long> GetTotalHours(string instructorId, int pageNumber, int pageSize);
        
    }

}
