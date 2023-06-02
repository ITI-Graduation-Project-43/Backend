using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using MindMission.Application.DTOs;
using MindMission.Application.Repository_Interfaces;
using MindMission.Domain.Common;
using MindMission.Infrastructure.Context;
using System.Linq.Expressions;

namespace MindMission.Infrastructure.Repositories.Base
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

        public async Task<IQueryable<TClass>> GetAllAsync()
        {
            return await Task.FromResult(_dbSet);
        }

        public async Task<IEnumerable<TClass>> GetAllAsync(params Expression<Func<TClass, object>>[] IncludeProperties)
        {
            IQueryable<TClass> Query = _dbSet;
            Query = IncludeProperties.Aggregate(Query, (current, includeProperty) => current.Include(includeProperty));
            return await Query.ToListAsync();
        }

        public async Task<TClass> GetByIdAsync(TDataType id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            var entity = await _dbSet.FindAsync(id);

            return entity ?? throw new KeyNotFoundException($"No entity with id {id} found.");
        }

        public async Task<TClass> GetByIdAsync(TDataType id, params Expression<Func<TClass, object>>[] IncludeProperties)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            IQueryable<TClass> Query = _dbSet;
            Query = IncludeProperties.Aggregate(Query, (current, includeProperty) => current.Include(includeProperty));
            var entity = await Query.FirstOrDefaultAsync(q => q.Id.Equals(id));
            return entity ?? throw new KeyNotFoundException($"No entity with id {id} found.");
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