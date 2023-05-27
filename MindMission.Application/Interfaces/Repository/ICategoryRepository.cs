using MindMission.Domain.Enums;
using MindMission.Domain.Models;

namespace MindMission.Application.Repository_Interfaces
{
    public interface ICategoryRepository : IRepository<Category, int>
    {
        Task<IEnumerable<Category>> GetByTypeAsync(CategoryType type);
        Task<IEnumerable<Category>> GetByParentIdAsync(int parentId);

    }
}
