using MindMission.Application.Interfaces.Repository;
using MindMission.Application.Interfaces.Services.Base;
using MindMission.Domain.Models;

namespace MindMission.Application.Interfaces.Services
{
    public interface IStudentService : IService<Student, string>, IStudentRepository
    {
    }
}