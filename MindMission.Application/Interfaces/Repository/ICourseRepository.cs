using MindMission.Application.DTOs;
using MindMission.Application.Interfaces.Repository.Base;
using MindMission.Domain.Models;

namespace MindMission.Application.Repository_Interfaces
{
    public interface ICourseRepository : IRepository<Course, int>
    {
        Task<Course> GetByNameAsync(string name);
        IQueryable<Course> GetAllByCategoryAsync(int categoryId, int pageNumber, int pageSize);
        Task<IQueryable<Course>> GetRelatedCoursesAsync(int courseId, int pageNumber, int pageSize);
        IQueryable<Course> GetAllByInstructorAsync(string instructorId, int pageNumber, int pageSize);
        Task<IQueryable<Course>> GetAllByInstructorAsync(string instructorId);

        IQueryable<Course> GetInstructorOtherCourses(string instructorId, int courseId, int pageNumber, int pageSize);
        IQueryable<Course> GetTopRatedCoursesAsync(int topNumber, int pageNumber, int pageSize);
        IQueryable<Course> GetRecentCoursesAsync(int recentNumber, int pageNumber, int pageSize);
        IQueryable<Course> GetNonApprovedCoursesAsync(int pageNumber, int pageSize);

        Task<StudentCourseDto> GetCourseByIdWithStudentsAsync(int courseId, int studentsNumber);

        Task<int> GetCourseNumber();
        Task<int> GetCourseRelatedNumber(int courseId);
        Task<int> GetCourseNumberByCourseId(int courseId);
        Task<int> GetCourseNumberByCourseIdAndInstructorId(int courseId, string instructorId);
        Task<int> GetCourseNumberByCategoryId(int category);
        Task<int> GetCourseNumberByInstructorId(string instructorId);


        Task<decimal> GetAvgRateCourses();

        Task<IQueryable<StudentCourseDto>> GetRelatedCoursesWithStudentsAsync(int courseId, int studentsNumber, int pageNumber, int pageSize);
        IQueryable<StudentCourseDto> GetInstructorOtherWithStudentsCourses(string instructorId, int courseId, int studentsNumber, int pageNumber, int pageSize);


        Task<Course> GetFeatureThisWeekCourse(int categoryId);

        Task<Course> AddCourseAsync(Course course);
        Task<Course> UpdateCourseAsync(int id, Course course);



        //to be replaced by filter later
        IQueryable<Course> GetApprovedCoursesByInstructorAsync(string instructorId, int pageNumber, int pageSize);
        IQueryable<Course> GetNonApprovedCoursesByInstructorAsync(string instructorId, int pageNumber, int pageSize);

        Task<Course> PutCourseToApprovedAsync(int courseId);

    }
}