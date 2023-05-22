using MindMission.Domain.Enums;
using MindMission.Domain.Models;

namespace MindMission.Application.Repository_Interfaces
{
    public interface ICourseRepository : IRepository<Course, int>
    {
        Task<Course> GetByNameAsync(string name);
        Task<IEnumerable<Course>> GetAllByCategoryAsync(int categoryId);
        Task<IEnumerable<Course>> GetRelatedCoursesAsync(int courseId);
        Task<IEnumerable<Course>> GetAllByInstructorAsync(string instructorId);
        Task<IEnumerable<Course>> GetTopRatedCoursesAsync(int topNumber);
        Task<IEnumerable<Course>> GetRecentCoursesAsync(int recentNumber);

    }
}
