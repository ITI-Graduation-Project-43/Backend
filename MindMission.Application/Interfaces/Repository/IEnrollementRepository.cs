using MindMission.Application.Interfaces.Repository.Base;
using MindMission.Domain.Models;

namespace MindMission.Application.Repository_Interfaces
{
    public interface IEnrollmentRepository : IRepository<Enrollment, int>
    {
        Task<IQueryable<Enrollment>> GetAllByStudentIdAsync(string studentId);
        Task<IQueryable<Enrollment>> GetAllByCourseIdAsync(int courseId);
    }
}