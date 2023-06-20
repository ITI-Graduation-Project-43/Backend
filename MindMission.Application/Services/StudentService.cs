using MindMission.Application.Interfaces.Repository;
using MindMission.Application.Interfaces.Services;
using MindMission.Application.Services.Base;
using MindMission.Domain.Models;
using System.Linq.Expressions;

namespace MindMission.Application.Services
{
    public class StudentService : Service<Student, string>, IStudentService
    {

        private readonly IStudentRepository _context;

        public StudentService(IStudentRepository context) : base(context)
        {
            _context = context;
        }



        public Task<IQueryable<Student>> GetRecentStudentEnrollmentAsync(int recentNumber, int courseId)
        {
            return _context.GetRecentStudentEnrollmentAsync(recentNumber, courseId);
        }
    }
}