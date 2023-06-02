using MindMission.Application.DTOs;
using MindMission.Domain.Enums;
using MindMission.Domain.Models;

namespace MindMission.Application.Repository_Interfaces
{
    public interface ICourseRepository : IRepository<Course, int>
    {
        Task<Course> GetByNameAsync(string name);
        Task<IQueryable<Course>> GetAllByCategoryAsync(int categoryId);
        Task<IQueryable<Course>> GetRelatedCoursesAsync(int courseId);
        Task<IQueryable<Course>> GetAllByInstructorAsync(string instructorId);
        Task<IQueryable<Course>> GetTopRatedCoursesAsync(int topNumber);
        Task<IQueryable<Course>> GetRecentCoursesAsync(int recentNumber);
    }
}