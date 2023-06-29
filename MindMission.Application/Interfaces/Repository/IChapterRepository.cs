using MindMission.Application.Interfaces.Repository.Base;
using MindMission.Domain.Models;

namespace MindMission.Application.Repository_Interfaces
{
    public interface IChapterRepository : IRepository<Chapter, int>
    {
        IQueryable<Chapter> GetByCourseIdAsync(int courseId, int pageNumber, int pageSize);

    }
}