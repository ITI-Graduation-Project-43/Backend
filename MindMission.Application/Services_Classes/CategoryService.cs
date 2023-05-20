using MindMission.Application.Repository_Interfaces;
using MindMission.Application.Service_Interfaces;
using MindMission.Domain.Models;

namespace MindMission.Application.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _context;
        public CategoryService(ICategoryRepository context)
        {
            _context = context;
        }
        public Task<IEnumerable<Category>> GetAllAsync()
        {
            return _context.GetAllAsync();
        }

        public Task<Category> GetByIdAsync(int id)
        {
            return _context.GetByIdAsync(id);
        }

        public Task<Category> AddAsync(Category entity)
        {
            return _context.AddAsync(entity);
        }

        public Task UpdateAsync(Category entity)
        {
            return _context.UpdateAsync(entity);
        }

        public Task DeleteAsync(int id)
        {
            return _context.DeleteAsync(id);
        }
    }
}
