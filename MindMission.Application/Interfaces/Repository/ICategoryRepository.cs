using MindMission.Application.Interfaces.Repository.Base;
using MindMission.Domain.Enums;
using MindMission.Domain.Models;

namespace MindMission.Application.Repository_Interfaces
{
    public interface ICategoryRepository : IRepository<Category, int>
    {
        IQueryable<Category> GetByTypeAsync(CategoryType type, int pageNumber, int pageSize);
        IQueryable<Category> GetByParentIdAsync(int parentId, int pageNumber, int pageSize);
        Task<Category> GetParentCategoryById(int parentId);
    }
}