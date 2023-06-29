using MindMission.Application.DTOs;
using MindMission.Application.Interfaces.Repository.Base;
using MindMission.Domain.Models;

namespace MindMission.Application.Repository_Interfaces
{
    public interface IEnrollmentRepository : IRepository<Enrollment, int>
    {
        IQueryable<Enrollment> GetAllByStudentIdAsync(string studentId, int pageNumber, int pageSize);
        IQueryable<Enrollment> GetAllByCourseIdAsync(int courseId, int pageNumber, int pageSize);
        Task<EnrollmentDto> GetByStudentAndCourseAsync(string StudentId, int courseId);
        Task<int> SuccessfulLearners();
    }
}