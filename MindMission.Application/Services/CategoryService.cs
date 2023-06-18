using MindMission.Application.Repository_Interfaces;
using MindMission.Application.Service_Interfaces;
using MindMission.Application.Services.Base;
using MindMission.Domain.Enums;
using MindMission.Domain.Models;

namespace MindMission.Application.Services
{
    public class CategoryService : Service<Category, int>, ICategoryService
    {
        private readonly ICategoryRepository _context;

        public CategoryService(ICategoryRepository context) : base(context)
        {
            _context = context;
        }


        public Task<IQueryable<Category>> GetByTypeAsync(CategoryType type)
        {
            return _context.GetByTypeAsync(type);
        }

        public Task<IQueryable<Category>> GetByParentIdAsync(int parentId)
        {
            return _context.GetByParentIdAsync(parentId);
        }

        public Task<Category> GetParentCategoryById(int parentId)
        {
            return _context.GetParentCategoryById(parentId);
        }
    }
}