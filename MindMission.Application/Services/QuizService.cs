using MindMission.Application.Repository_Interfaces;
using MindMission.Application.Service_Interfaces;
using MindMission.Domain.Models;
using System.Linq.Expressions;

namespace MindMission.Application.Services_Classes
{
    public class QuizService : IQuizService
    {
        private readonly IQuizRepository _context;

        public QuizService(IQuizRepository context)
        {
            _context = context;
        }

        public Task<IEnumerable<Quiz>> GetAllAsync()
        {
            return _context.GetAllAsync();
        }

        public Task<Quiz> GetByIdAsync(int id)
        {
            return _context.GetByIdAsync(id);
        }

        public Task<Quiz> AddAsync(Quiz entity)
        {
            return _context.AddAsync(entity);
        }

        public Task UpdateAsync(Quiz entity)
        {
            return _context.UpdateAsync(entity);
        }

        public Task DeleteAsync(int id)
        {
            return _context.DeleteAsync(id);
        }

        public async Task<IEnumerable<Quiz>> GetAllAsync(params Expression<Func<Quiz, object>>[] IncludeProperties)
        {
            return await _context.GetAllAsync(IncludeProperties);
        }

        public Task<Quiz> GetByIdAsync(int id, params Expression<Func<Quiz, object>>[] IncludeProperties)
        {
            return _context.GetByIdAsync(id, IncludeProperties);
        }
    }
}