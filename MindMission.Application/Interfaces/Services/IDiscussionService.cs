using MindMission.Application.Repository_Interfaces;
using MindMission.Domain.Models;

namespace MindMission.Application.Service_Interfaces
{
    public interface IDiscussionService : IRepository<Discussion, int>
    {
        Task<IEnumerable<Discussion>> GetAllDiscussionByLessonIdAsync(int id);
    }
}