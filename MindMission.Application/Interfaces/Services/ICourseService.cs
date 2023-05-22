using MindMission.Application.Repository_Interfaces;
using MindMission.Domain.Enums;
using MindMission.Domain.Models;

namespace MindMission.Application.Service_Interfaces
{
    public interface ICourseService : ICourseRepository
    {
        Task<IEnumerable<Course>> GetAllByCategoryAsync(int categoryId);
        Task<IEnumerable<Course>> GetRelatedCoursesAsync(int courseId);
        Task<IEnumerable<Course>> GetAllByInstructorAsync(string instructorId);
        Task<IEnumerable<Course>> GetTopRatedCoursesAsync(int topNumber);
        Task<IEnumerable<Course>> GetRecentCoursesAsync(int recentNumber);
    }
}
