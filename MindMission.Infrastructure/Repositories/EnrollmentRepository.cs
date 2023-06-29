using Microsoft.EntityFrameworkCore;
using MindMission.Application.DTOs;
using MindMission.Application.Repository_Interfaces;
using MindMission.Domain.Models;
using MindMission.Infrastructure.Context;
using MindMission.Infrastructure.Repositories.Base;
using System.Drawing.Printing;

namespace MindMission.Infrastructure.Repositories
{
    public class EnrollmentRepository : Repository<Enrollment, int>, IEnrollmentRepository
    {
        private readonly MindMissionDbContext _context;
        private readonly DbSet<Enrollment> _dbSet;

        public EnrollmentRepository(MindMissionDbContext context) : base(context)
        {
            _context = context;
            _dbSet = _context.Set<Enrollment>();
        }

        public IQueryable<Enrollment> GetAllByCourseIdAsync(int courseId, int pageNumber, int pageSize)
        {
            var Enrollments =  _dbSet.AsSplitQuery().Include(e => e.Student).Include(e => e.Course).ThenInclude(c => c.Instructor).Include(e => e.Course).ThenInclude(c => c.Category).Where(w => w.CourseId == courseId && !w.IsDeleted)
                                    .Skip((pageNumber - 1) * pageSize).Take(pageSize);
            return Enrollments.AsQueryable();
        }

        public IQueryable<Enrollment> GetAllByStudentIdAsync(string StudentId, int pageNumber, int pageSize)
        {
            var Enrollments = _dbSet.AsSplitQuery().Include(e => e.Student).Include(e => e.Course).ThenInclude(c => c.Instructor).Include(e => e.Course).ThenInclude(c => c.Category).Where(w => w.StudentId == StudentId && !w.IsDeleted)
                                     .Skip((pageNumber - 1) * pageSize).Take(pageSize);
            return Enrollments.AsQueryable();
        }

        public async Task<EnrollmentDto> GetByStudentAndCourseAsync(string StudentId, int courseId)
        {
            return await _dbSet
                .Include(e => e.Course)
                    .ThenInclude(c => c.Instructor)
                .Include(e => e.Course)
                    .ThenInclude(c => c.Category)
                .Include(e => e.Student)
                .Where(w => w.StudentId == StudentId && w.CourseId == courseId && !w.IsDeleted)
                .Select(e => new EnrollmentDto
                {
                    Id = e.Id,
                    EnrollmentDate = e.EnrollmentDate,
                    CourseId = e.Course.Id,
                    CourseTitle = e.Course.Title,
                    CoursePrice = e.Course.Price,
                    CourseDescription = e.Course.Description,
                    CourseImageUrl = e.Course.ImageUrl,
                    StudentId = e.Student.Id,
                    StudentName = e.Student.FullName,
                    CourseAvgReview = e.Course.AvgReview,
                    CategoryName = e.Course.Category.Name,
                    InstructorId = e.Course.Instructor.Id,
                    InstructorName = e.Course.Instructor.FullName,
                    InstructorProfilePicture = e.Course.Instructor.ProfilePicture,
                    CourseNoOfEnrollment = e.Course.Enrollments.Count()
                })
                .FirstOrDefaultAsync() ?? throw new NullReferenceException("Enrollment not found");

        }

        public async Task<int> SuccessfulLearners()
        {
            return _dbSet.Count();
        }
    }
}