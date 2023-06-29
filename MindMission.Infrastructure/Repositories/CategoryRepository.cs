using Microsoft.EntityFrameworkCore;
using MindMission.Application.Repository_Interfaces;
using MindMission.Domain.Enums;
using MindMission.Domain.Models;
using MindMission.Infrastructure.Context;
using MindMission.Infrastructure.Repositories.Base;

namespace MindMission.Infrastructure.Repositories
{
    public class CategoryRepository : Repository<Category, int>, ICategoryRepository
    {
        private readonly MindMissionDbContext _context;

        public CategoryRepository(MindMissionDbContext context) : base(context)
        {
            _context = context;
        }

        public IQueryable<Category> GetByTypeAsync(CategoryType type, int pageNumber, int pageSize)
        {
            var Query = _context.Categories
                        .Include(category => category.Parent)
                        .ThenInclude(parentCategory => parentCategory.Parent)
                        .Where(category => category.Type == type && !category.IsDeleted)
                        .Skip((pageNumber - 1) * pageSize)
                                        .Take(pageSize);

            return Query.AsQueryable();
        }

        public IQueryable<Category> GetByParentIdAsync(int parentId, int pageNumber, int pageSize)
        {
            var Query = _context.Categories
                        .Include(category => category.Parent)
                        .ThenInclude(parentCategory => parentCategory.Parent)
                        .Where(category => category.ParentId == parentId && !category.IsDeleted)
                        .Skip((pageNumber - 1) * pageSize)
                                        .Take(pageSize);

            return Query.AsQueryable();
        }


        public async Task<Category> GetParentCategoryById(int parentId)
        {
            return await _context.Categories
                .Where(category => category.Type == 0 && category.Id == parentId && !category.IsDeleted)
                .FirstOrDefaultAsync()
                ?? throw new KeyNotFoundException($"No entity with id {parentId} found.");
        }
    }
}