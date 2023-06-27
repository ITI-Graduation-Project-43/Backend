using MindMission.Application.DTOs;
using MindMission.Application.Interfaces.Repository.Base;
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
        Task<IQueryable<Course>> GetNonApprovedCoursesAsync();

        Task<StudentCourseDto> GetCourseByIdWithStudentsAsync(int courseId, int studentsNumber);


        Task<IQueryable<StudentCourseDto>> GetRelatedCoursesWithStudentsAsync(int courseId, int studentsNumber);
        Task<IQueryable<StudentCourseDto>> GetInstructorOtherWithStudentsCourses(string instructorId, int courseId, int studentsNumber);


        Task<Course> GetFeatureThisWeekCourse(int categoryId);

        Task<Course> AddCourseAsync(Course course);
        Task<Course> UpdateCourseAsync(int id, Course course);



        //to be replaced by filter later
        Task<IQueryable<Course>> GetApprovedCoursesByInstructorAsync(string instructorId);
        Task<IQueryable<Course>> GetNonApprovedCoursesByInstructorAsync(string instructorId);

        Task<Course>PutCourseToApprovedAsync(int courseId);

    }
}