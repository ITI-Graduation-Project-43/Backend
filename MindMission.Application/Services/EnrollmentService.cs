using MindMission.Application.DTOs;
using MindMission.Application.Interfaces.Repository.Base;
using MindMission.Application.Repository_Interfaces;
using MindMission.Application.Service_Interfaces;
using MindMission.Application.Services.Base;
using MindMission.Domain.Common;
using MindMission.Domain.Models;
using System.Linq.Expressions;

namespace MindMission.Application.Services
{
    public class EnrollmentService : Service<Enrollment, int>, IEnrollmentService
    {
        private readonly IEnrollmentRepository _context;
        private readonly IWishlistService _wishlistService;

        public EnrollmentService(IEnrollmentRepository context, IWishlistService wishlistService) : base(context)
        {
            _context = context;
            _wishlistService = wishlistService;
        }


        public override async Task<Enrollment> AddAsync(Enrollment enrollment)
        {
            using var transaction = await _context.Context.Database.BeginTransactionAsync();
            try
            {
                var wishlistItem = await _wishlistService.GetByCourseStudentAsync(enrollment.CourseId, enrollment.StudentId);

                if (wishlistItem != null)
                {
                    await _wishlistService.SoftDeleteAsync(wishlistItem.Id);
                }

                var entity = await _context.AddAsync(enrollment);
                await _context.Context.SaveChangesAsync();

                await transaction.CommitAsync();

                return entity;
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public Task<int> SuccessfulLearners()
        {
            return _context.SuccessfulLearners();
        }

        public Task<IQueryable<Enrollment>> GetAllByCourseIdAsync(int courseId)
        {
            return _context.GetAllByCourseIdAsync(courseId);
        }

        public Task<IQueryable<Enrollment>> GetAllByStudentIdAsync(string studentId)
        {
            return _context.GetAllByStudentIdAsync(studentId);
        }

        public async Task<EnrollmentDto> GetByStudentAndCourseAsync(string StudentId, int courseId)
        {
            return await _context.GetByStudentAndCourseAsync(StudentId, courseId);
        }
    }
}