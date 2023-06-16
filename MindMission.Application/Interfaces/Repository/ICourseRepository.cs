using MindMission.Application.DTOs;
using MindMission.Domain.Models;

namespace MindMission.Application.Repository_Interfaces
{
    public interface ICourseRepository : IRepository<Course, int>
    {
        Task<Course> GetByNameAsync(string name);
        Task<IQueryable<Course>> GetAllByCategoryAsync(int categoryId);
        Task<IQueryable<Course>> GetRelatedCoursesAsync(int courseId);
        Task<IQueryable<Course>> GetAllByInstructorAsync(string instructorId);
        Task<IQueryable<Course>> GetInstructorOtherCourses(string instructorId, int courseId);
        Task<IQueryable<Course>> GetTopRatedCoursesAsync(int topNumber);
        Task<IQueryable<Course>> GetRecentCoursesAsync(int recentNumber);

        Task<StudentCourseDto> GetCourseByIdWithStudentsAsync(int courseId, int studentsNumber);


        Task<IQueryable<StudentCourseDto>> GetRelatedCoursesWithStudentsAsync(int courseId, int studentsNumber);
        Task<IQueryable<StudentCourseDto>> GetInstructorOtherWithStudentsCourses(string instructorId, int courseId, int studentsNumber);


        Task<Course> GetFeatureThisWeekCourse();

        Task<Course> AddCourseAsync(Course course);
        Task<Course> UpdateCourseAsync(int id, Course course);


    }
}