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


        public IQueryable<Category> GetByTypeAsync(CategoryType type, int pageNumber, int pageSize)
        {
            return _context.GetByTypeAsync(type, pageNumber, pageSize);
        }

        public IQueryable<Category> GetByParentIdAsync(int parentId, int pageNumber, int pageSize)
        {
            return _context.GetByParentIdAsync(parentId, pageNumber, pageSize);
        }

        public Task<Category> GetParentCategoryById(int parentId)
        {
            return _context.GetParentCategoryById(parentId);
        }
    }
}