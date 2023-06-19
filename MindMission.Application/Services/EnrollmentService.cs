using MindMission.Application.Repository_Interfaces;
using MindMission.Application.Service_Interfaces;
using MindMission.Application.Services.Base;
using MindMission.Domain.Models;
using System.Linq.Expressions;

namespace MindMission.Application.Services
{
    public class EnrollmentService : Service<Enrollment, int>, IEnrollmentService
    {
        private readonly IEnrollmentRepository _context;

        public EnrollmentService(IEnrollmentRepository context) : base(context)
        {
            _context = context;
        }



        public Task<IQueryable<Enrollment>> GetAllByCourseIdAsync(int courseId)
        {
            return _context.GetAllByCourseIdAsync(courseId);
        }

        public Task<IQueryable<Enrollment>> GetAllByStudentIdAsync(string studentId)
        {
            return _context.GetAllByStudentIdAsync(studentId);
        }

    }
}