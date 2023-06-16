using MindMission.Application.Repository_Interfaces;
using MindMission.Application.Service_Interfaces;
using MindMission.Domain.Models;
using System.Linq.Expressions;

namespace MindMission.Application.Services_Classes
{
    public class ChapterService : IChapterService
    {
        private readonly IChapterRepository _context;

        public ChapterService(IChapterRepository context)
        {
            _context = context;
        }

        public Task<IQueryable<Chapter>> GetAllAsync()
        {
            return _context.GetAllAsync();
        }

        public async Task<IEnumerable<Chapter>> GetAllAsync(params Expression<Func<Chapter, object>>[] IncludeProperties)
        {
            return await _context.GetAllAsync(IncludeProperties);
        }

        public Task<Chapter> GetByIdAsync(int id)
        {
            return _context.GetByIdAsync(id);
        }

        public Task<Chapter> GetByIdAsync(int id, params Expression<Func<Chapter, object>>[] IncludeProperties)
        {
            return _context.GetByIdAsync(id, IncludeProperties);
        }

        public Task<Chapter> AddAsync(Chapter entity)
        {
            return _context.AddAsync(entity);
        }

        public Task UpdateAsync(Chapter entity)
        {
            return _context.UpdateAsync(entity);
        }

        public Task DeleteAsync(int id)
        {
            return _context.DeleteAsync(id);
        }
        public Task SoftDeleteAsync(int id)
        {
            return _context.SoftDeleteAsync(id);
        }

        public Task<IQueryable<Chapter>> GetByCourseIdAsync(int courseId)
        {
            return _context.GetByCourseIdAsync(courseId);
        }

    }
}