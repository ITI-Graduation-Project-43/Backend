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

        public async Task<IQueryable<Category>> GetByTypeAsync(CategoryType type)
        {
            var Query = await _context.Categories
                        .Include(category => category.Parent)
                        .ThenInclude(parentCategory => parentCategory.Parent)
                        .Where(category => category.Type == type)
                        .ToListAsync();

            return Query.AsQueryable();
        }

        public async Task<IQueryable<Category>> GetByParentIdAsync(int parentId)
        {
            var Query = await _context.Categories
                        .Include(category => category.Parent)
                        .ThenInclude(parentCategory => parentCategory.Parent)
                        .Where(category => category.ParentId == parentId)
                        .ToListAsync();

            return Query.AsQueryable();
        }
    }
}