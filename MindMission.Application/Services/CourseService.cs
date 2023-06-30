using MindMission.Application.DTOs;
using MindMission.Application.Repository_Interfaces;
using MindMission.Application.Service_Interfaces;
using MindMission.Application.Services.Base;
using MindMission.Domain.Models;

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

        public async Task<decimal> GetAvgRateCourses()
        {
            return await _context.GetAvgRateCourses();
        }

        public IQueryable<Course> GetAllByCategoryAsync(int categoryId, int pageNumber, int pageSize)
        {
            return _context.GetAllByCategoryAsync(categoryId, pageNumber, pageSize);
        }

        public Task<IQueryable<Course>> GetRelatedCoursesAsync(int courseId, int pageNumber, int pageSize)
        {
            return _context.GetRelatedCoursesAsync(courseId, pageNumber, pageSize);
        }

        public IQueryable<Course> GetAllByInstructorAsync(string instructorId, int pageNumber, int pageSize)
        {
            return _context.GetAllByInstructorAsync(instructorId, pageNumber, pageSize);
        }
        public Task<IQueryable<Course>> GetAllByInstructorAsync(string instructorId)
        {
            return _context.GetAllByInstructorAsync(instructorId);
        }
        public IQueryable<Course> GetInstructorOtherCourses(string instructorId, int courseId, int pageNumber, int pageSize)
        {
            return _context.GetInstructorOtherCourses(instructorId, courseId, pageNumber, pageSize);

        }
        public IQueryable<Course> GetTopRatedCoursesAsync(int topNumber, int pageNumber, int pageSize)
        {
            return _context.GetTopRatedCoursesAsync(topNumber, pageNumber, pageSize);
        }

        public IQueryable<Course> GetRecentCoursesAsync(int recentNumber, int pageNumber, int pageSize)
        {
            return _context.GetRecentCoursesAsync(recentNumber, pageNumber, pageSize);
        }

        public async Task<StudentCourseDto> GetCourseByIdWithStudentsAsync(int courseId, int studentsNumber)
        {
            return await _context.GetCourseByIdWithStudentsAsync(courseId, studentsNumber);
        }

        public Task<IQueryable<StudentCourseDto>> GetRelatedCoursesWithStudentsAsync(int courseId, int studentsNumber, int pageNumber, int pageSize)
        {
            return _context.GetRelatedCoursesWithStudentsAsync(courseId, studentsNumber, pageNumber, pageSize);
        }

        public IQueryable<StudentCourseDto> GetInstructorOtherWithStudentsCourses(string instructorId, int courseId, int studentsNumber, int pageNumber, int pageSize)
        {
            return _context.GetInstructorOtherWithStudentsCourses(instructorId, courseId, studentsNumber, pageNumber, pageSize);
        }

        public async Task<Course> GetFeatureThisWeekCourse(int categoryId)
        {
            return await _context.GetFeatureThisWeekCourse(categoryId);
        }

        public async Task<int> GetCourseNumberByCourseId(int courseId)
        {
            return await _context.GetCourseNumberByCourseId(courseId);
        }
        public async Task<int> GetCourseRelatedNumber(int courseId)
        {
            return await _context.GetCourseRelatedNumber(courseId);
        }
        public async Task<int> GetCourseNumberByCategoryId(int category)
        {
            return await _context.GetCourseNumberByCategoryId(category);
        }
        public async Task<int> GetCourseNumberByCourseIdAndInstructorId(int courseId, string instructorId)
        {
            return await _context.GetCourseNumberByCourseIdAndInstructorId(courseId, instructorId);
        }

        public async Task<int> GetCourseNumberByInstructorId(string instructorId)
        {
            return await _context.GetCourseNumberByInstructorId(instructorId);
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

        public IQueryable<Course> GetApprovedCoursesByInstructorAsync(string instructorId, int pageNumber, int pageSize)
        {
            return _context.GetApprovedCoursesByInstructorAsync(instructorId, pageNumber, pageSize);
        }

        public IQueryable<Course> GetNonApprovedCoursesByInstructorAsync(string instructorId, int pageNumber, int pageSize)
        {
            return _context.GetNonApprovedCoursesByInstructorAsync(instructorId, pageNumber, pageSize);
        }

        public async Task<Course> PutCourseToApprovedAsync(int courseId)
        {
            return await _context.PutCourseToApprovedAsync(courseId);
        }

        public IQueryable<Course> GetNonApprovedCoursesAsync(int pageNumber, int pageSize)
        {
            return _context.GetNonApprovedCoursesAsync(pageNumber, pageSize);
        }
    }
}

