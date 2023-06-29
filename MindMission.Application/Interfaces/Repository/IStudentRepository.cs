using MindMission.Application.Interfaces.Repository.Base;
using MindMission.Domain.Models;

namespace MindMission.Application.Interfaces.Repository
{
    public interface IStudentRepository : IRepository<Student, string>
    {
        IQueryable<Student> GetRecentStudentEnrollmentAsync(int recentNumber, int courseId);

    }
}