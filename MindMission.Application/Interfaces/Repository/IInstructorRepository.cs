using MindMission.Domain.Enums;
using MindMission.Domain.Models;

namespace MindMission.Application.Repository_Interfaces
{
    public interface IInstructorRepository : IRepository<Instructor, string>
    {
        public Task<IQueryable<Instructor>> GetTopInstructorsAsync();
    }
}
