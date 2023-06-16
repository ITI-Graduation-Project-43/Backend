
using MindMission.Domain.Models;

namespace MindMission.Application.Interfaces.Repository
{
    public interface ITrackingTimeRepository
    {
        Task<IQueryable<TimeTracking>> GetAll();
        Task<IQueryable<TimeTracking>> GetByCourseId(int CourseId);
        Task<IQueryable<TimeTracking>> GetByStudentId(string StudentId);
        Task<TimeTracking> Create(string studentId, int courseId);
        Task<TimeTracking> Update(string studentId, int courseId);
        Task<List<Student>> GetLastfourStudentIds();
    }
}
