using MindMission.Application.DTOs;
using MindMission.Application.Interfaces.Repository;
using MindMission.Application.Repository_Interfaces;
using MindMission.Application.Service_Interfaces;
using MindMission.Application.Services.Base;
using MindMission.Domain.Models;
using System.Linq.Expressions;

namespace MindMission.Application.Services
{
    public class CourseService : Service<Course, int>, ICourseService
    {
        private readonly ICourseRepository _context;

        public CourseService(ICourseRepository context) : base(context)
        {
            _context = context;
        }

        #region Get

        public async Task<int> GetCourseNumber()
        {
            return await _context.GetCourseNumber();
        }

        public async Task<Course> GetByNameAsync(string name)
        {
            return await _context.GetByNameAsync(name);
        }

        public async Task<IQueryable<Course>> GetAllByCategoryAsync(int categoryId)
        {
            return await _context.GetAllByCategoryAsync(categoryId);
        }

        public async Task<IQueryable<Course>> GetRelatedCoursesAsync(int courseId)
        {
            return await _context.GetRelatedCoursesAsync(courseId);
        }

        public async Task<IQueryable<Course>> GetAllByInstructorAsync(string instructorId)
        {
            return await _context.GetAllByInstructorAsync(instructorId);
        }

        public async Task<IQueryable<Course>> GetInstructorOtherCourses(string instructorId, int courseId)
        {
            return await _context.GetInstructorOtherCourses(instructorId, courseId);

        }
        public async Task<IQueryable<Course>> GetTopRatedCoursesAsync(int topNumber)
        {
            return await _context.GetTopRatedCoursesAsync(topNumber);
        }

        public async Task<IQueryable<Course>> GetRecentCoursesAsync(int recentNumber)
        {
            return await _context.GetRecentCoursesAsync(recentNumber);
        }

        public async Task<StudentCourseDto> GetCourseByIdWithStudentsAsync(int courseId, int studentsNumber)
        {
            return await _context.GetCourseByIdWithStudentsAsync(courseId, studentsNumber);
        }

        public async Task<IQueryable<StudentCourseDto>> GetRelatedCoursesWithStudentsAsync(int courseId, int studentsNumber)
        {
            return await _context.GetRelatedCoursesWithStudentsAsync(courseId, studentsNumber);
        }

        public async Task<IQueryable<StudentCourseDto>> GetInstructorOtherWithStudentsCourses(string instructorId, int courseId, int studentsNumber)
        {
            return await _context.GetInstructorOtherWithStudentsCourses(instructorId, courseId, studentsNumber);
        }

        public async Task<Course> GetFeatureThisWeekCourse(int categoryId)
        {
            return await _context.GetFeatureThisWeekCourse(categoryId);
        }
        #endregion



        public async Task<Course> AddCourseAsync(Course course)
        {
            return await _context.AddCourseAsync(course);
        }
        public async Task<Course> UpdateCourseAsync(int id, Course course)
        {
            return await _context.UpdateCourseAsync(id, course);
        }

        public async Task<IQueryable<Course>> GetApprovedCoursesByInstructorAsync(string instructorId)
        {
            return await _context.GetApprovedCoursesByInstructorAsync(instructorId);
        }

        public async Task<IQueryable<Course>> GetNonApprovedCoursesByInstructorAsync(string instructorId)
        {
            return await _context.GetNonApprovedCoursesByInstructorAsync(instructorId);
        }

        public async Task<Course> PutCourseToApprovedAsync(int courseId)
        {
            return await _context.PutCourseToApprovedAsync(courseId);
        }

        public Task<IQueryable<Course>> GetNonApprovedCoursesAsync()
        {
            return _context.GetNonApprovedCoursesAsync();
        }
    }
}

