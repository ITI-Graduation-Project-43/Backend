using Microsoft.EntityFrameworkCore;
using MindMission.Application.DTOs;
using MindMission.Application.Repository_Interfaces;
using MindMission.Domain.Models;
using MindMission.Infrastructure.Context;
using MindMission.Infrastructure.Repositories.Base;

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

        public async Task<IQueryable<Enrollment>> GetAllByCourseIdAsync(int courseId)
        {
            var Enrollments = await _dbSet.Where(w => w.CourseId == courseId && !w.IsDeleted).ToListAsync();
            return Enrollments.AsQueryable();
        }

        public async Task<IQueryable<Enrollment>> GetAllByStudentIdAsync(string StudentId)
        {
            var Enrollments = await _dbSet.Where(w => w.StudentId == StudentId && !w.IsDeleted).ToListAsync();
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
    }
}