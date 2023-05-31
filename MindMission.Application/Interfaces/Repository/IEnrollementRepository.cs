using MindMission.Domain.Models;

namespace MindMission.Application.Repository_Interfaces
{
    public interface IEnrollmentRepository : IRepository<Enrollment, int>
    {
        Task<IEnumerable<Enrollment>> GetAllByStudentIdAsync(string StudentId);

        Task<IEnumerable<Enrollment>> GetAllByCourseIdAsync(int CourseId);
    }
}