using MindMission.Application.Repository_Interfaces;
using MindMission.Domain.Enums;
using MindMission.Domain.Models;

namespace MindMission.Application.Service_Interfaces
{
    public interface ICategoryService : IRepository<Category, int>
    {
        Task<IEnumerable<Category>> GetByTypeAsync(CategoryType type);
        Task<IEnumerable<Category>> GetByParentIdAsync(int parentId);
    }
}
