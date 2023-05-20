using Microsoft.EntityFrameworkCore;
using MindMission.Application.Repository_Interfaces;
using MindMission.Domain;
using MindMission.Domain.Models;

namespace MindMission.Infrastructure
{
    public class Repository<TClass, TDataType> : IRepository<TClass, TDataType> where TClass : class, IEntity<TDataType>, new()
    {
        private readonly MindMissionDbContext _context;
        private readonly DbSet<TClass> _dbSet;

        public Repository(MindMissionDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<TClass>();
        }

        public async Task<IEnumerable<TClass>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<TClass> GetByIdAsync(TDataType id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<TClass> AddAsync(TClass entity)
        {
            _dbSet.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task UpdateAsync(TClass entity)
        {
            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(TDataType id)
        {
            _dbSet.Remove(await GetByIdAsync(id));
            await _context.SaveChangesAsync();
        }

    }
}
