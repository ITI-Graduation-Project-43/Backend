using MindMission.Domain.Models;

namespace MindMission.Application.Repository_Interfaces
{
    public interface IEnrollmentRepository : IRepository<Enrollment, int>
    {
        Task<IQueryable<Enrollment>> GetAllByStudentIdAsync(string StudentId);
        Task<IQueryable<Enrollment>> GetAllByCourseIdAsync(int CourseId);
    }
}