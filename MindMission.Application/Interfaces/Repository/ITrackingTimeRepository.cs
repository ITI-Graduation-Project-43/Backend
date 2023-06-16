
using MindMission.Domain.Models;

namespace MindMission.Application.Interfaces.Repository
{
    public interface ITrackingTimeRepository
    {
        Task<IEnumerable<TimeTracking>> GetAll();
        Task<IEnumerable<TimeTracking>> GetByCourseId(int CourseId);
        Task<IEnumerable<TimeTracking>> GetByStudentId(string StudentId);
        Task<TimeTracking> Create(string studentId, int courseId);
        Task<TimeTracking> Update(string studentId, int courseId);
        Task<List<Student>> GetLastfourStudentIds();
    }
}
