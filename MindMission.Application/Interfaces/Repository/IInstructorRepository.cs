using MindMission.Application.Interfaces.Repository.Base;
using MindMission.Domain.Models;

namespace MindMission.Application.Repository_Interfaces
{
    public interface IInstructorRepository : IRepository<Instructor, string>
    {
        IQueryable<Instructor> GetTopRatedInstructorsAsync(int topNumber, int pageNumber, int pageSize);

        Task<int> GetTotalTopRatedInstructorsAsync();


    }
}