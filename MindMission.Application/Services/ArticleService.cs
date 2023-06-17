using MindMission.Application.Interfaces.Repository;
using MindMission.Application.Interfaces.Services;
using MindMission.Application.Repository_Interfaces;
using MindMission.Domain.Models;
using System.Linq.Expressions;

namespace MindMission.Application.Services
{
    public class ArticleService : IArticleService
    {
        private readonly IArticleRepository _context;

        public ArticleService(IArticleRepository context)
        {
            _context = context;
        }


        public async Task<IQueryable<Article>> GetAllAsync()
        {
            return await _context.GetAllAsync();
        }

        public async Task<IEnumerable<Article>> GetAllAsync(params Expression<Func<Article, object>>[] includeProperties)
        {
            return await _context.GetAllAsync(includeProperties);
        }

        public async Task<Article> GetByIdAsync(int id)
        {
            return await _context.GetByIdAsync(id);
        }

        public async Task<Article> GetByIdAsync(int id, params Expression<Func<Article, object>>[] includeProperties)
        {
            return await _context.GetByIdAsync(id, includeProperties);
        }

        public async Task<Article> AddAsync(Article entity)
        {
            return await _context.AddAsync(entity);
        }

        public async Task<Article> UpdateAsync(Article entity)
        {
            return await _context.UpdateAsync(entity);
        }
        public async Task<Article> UpdatePartialAsync(int id, Article entity)
        {
            return await _context.UpdatePartialAsync(id, entity);
        }

        public Task DeleteAsync(int id)
        {
            return _context.DeleteAsync(id);
        }
        public Task SoftDeleteAsync(int id)
        {
            return _context.SoftDeleteAsync(id);
        }
    }
}