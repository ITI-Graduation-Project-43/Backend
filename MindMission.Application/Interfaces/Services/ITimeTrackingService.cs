using MindMission.Application.Interfaces.Repository;


namespace MindMission.Application.Interfaces.Services
{
    public  interface ITimeTrackingService : ITrackingTimeRepository
    {
        Task<object> GetCourseVisitCount(int courseId);
        
        Task<long> GetTotalHours(string instructorId);
        
    }

}
