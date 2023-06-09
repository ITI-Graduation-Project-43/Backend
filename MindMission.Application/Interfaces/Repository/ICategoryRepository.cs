using MindMission.Domain.Enums;
using MindMission.Domain.Models;

namespace MindMission.Application.Repository_Interfaces
{
    public interface ICategoryRepository : IRepository<Category, int>
    {
        Task<IQueryable<Category>> GetByTypeAsync(CategoryType type);
        Task<IQueryable<Category>> GetByParentIdAsync(int parentId);
    }
}