using MindMission.Application.DTOs;
using MindMission.Application.Repository_Interfaces;
using MindMission.Application.Service_Interfaces;
using MindMission.Domain.Models;
using System.Linq.Expressions;

namespace MindMission.Application.Services_Classes
{
    public class LessonService : ILessonService
    {
        private readonly ILessonRepository _context;

        public LessonService(ILessonRepository context)
        {
            _context = context;
        }

        public Task<IQueryable<Lesson>> GetAllAsync()
        {
            return _context.GetAllAsync();
        }

        public Task<Lesson> GetByIdAsync(int id)
        {
            return _context.GetByIdAsync(id);
        }

        public Task<Lesson> AddAsync(Lesson entity)
        {
            return _context.AddAsync(entity);
        }

        public Task UpdateAsync(Lesson entity)
        {
            return _context.UpdateAsync(entity);
        }

        public Task DeleteAsync(int id)
        {
            return _context.DeleteAsync(id);
        }

        public async Task<IEnumerable<Lesson>> GetAllAsync(params Expression<Func<Lesson, object>>[] IncludeProperties)
        {
            return await _context.GetAllAsync(IncludeProperties);
        }

        public Task<Lesson> GetByIdAsync(int id, params Expression<Func<Lesson, object>>[] IncludeProperties)
        {
            return _context.GetByIdAsync(id, IncludeProperties);
        }
    }
}
