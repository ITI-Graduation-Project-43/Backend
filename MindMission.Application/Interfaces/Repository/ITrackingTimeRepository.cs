
using MindMission.Domain.Models;

namespace MindMission.Application.Interfaces.Repository
{
    public interface ITrackingTimeRepository
    {
        Task<IEnumerable<TimeTracking>> GetAll();
        IQueryable<TimeTracking> GetByCourseIds(List<int> courseIds, int pageNumber, int pageSize);
        IQueryable<TimeTracking> GetByCourseId(int CourseId, int pageNumber, int pageSize);
        IQueryable<TimeTracking> GetByStudentId(string StudentId, int pageNumber, int pageSize);
        Task<TimeTracking> Create(string studentId, int courseId);
        Task<TimeTracking> Update(string studentId, int courseId);
        IQueryable<Student> GetLastfourStudentIds(int courseId);
    }
}
