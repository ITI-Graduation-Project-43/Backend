using Microsoft.EntityFrameworkCore;
using MindMission.Application.Repository_Interfaces;
using MindMission.Domain;

namespace MindMission.Infrastructure
{
    public class Repository<T> : IRepository<T> where T : class, IEntity, new()
    {
        private readonly MindMissionDbContext _context;
        private readonly DbSet<T> _dbSet;

        public Repository(MindMissionDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<T> AddAsync(T entity)
        {
            _dbSet.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task UpdateAsync(T entity)
        {
            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            _dbSet.Remove(await GetByIdAsync(id));
            await _context.SaveChangesAsync();
        }

    }
}
