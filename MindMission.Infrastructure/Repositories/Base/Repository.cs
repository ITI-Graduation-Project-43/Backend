using Microsoft.EntityFrameworkCore;
using MindMission.Application.Interfaces.Repository.Base;
using MindMission.Domain.Common;
using MindMission.Domain.Constants;
using MindMission.Infrastructure.Context;
using System.Linq.Expressions;

namespace MindMission.Infrastructure.Repositories.Base
{
    public class Repository<TClass, TDataType> : IRepository<TClass, TDataType> where TClass : class, IEntity<TDataType>, ISoftDeletable, new()
    {
        private readonly MindMissionDbContext _context;
        private readonly DbSet<TClass> _dbSet;
        public DbContext Context => _context;

        public Repository(MindMissionDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<TClass>();
        }

        public async Task<IQueryable<TClass>> GetAllAsync()
        {
            var Query = await _dbSet.Where(x => !x.IsDeleted).ToListAsync();
            return Query.AsQueryable();
        }

        public async Task<IEnumerable<TClass>> GetAllAsync(params Expression<Func<TClass, object>>[] IncludeProperties)
        {
            IQueryable<TClass> Query = _dbSet.Where(x => !x.IsDeleted);
            Query = IncludeProperties.Aggregate(Query, (current, includeProperty) => current.Include(includeProperty));
            return await Query.ToListAsync();
        }

        public async Task<TClass> GetByIdAsync(TDataType id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            var entity = await _dbSet.Where(x => !x.IsDeleted && x.Id.Equals(id)).FirstOrDefaultAsync();

            return entity;
        }

        public async Task<TClass> GetByIdAsync(TDataType id, params Expression<Func<TClass, object>>[] IncludeProperties)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            IQueryable<TClass> Query = _dbSet.Where(x => !x.IsDeleted);
            Query = IncludeProperties.Aggregate(Query, (current, includeProperty) => current.Include(includeProperty));
            var entity = await Query.FirstOrDefaultAsync(q => q.Id.Equals(id));

            return entity;
        }

        public async Task<TClass> AddAsync(TClass entity)
        {
            _dbSet.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
        public async Task<IEnumerable<TClass>> BulkAddAsync(IEnumerable<TClass> entities)
        {
            _context.Set<TClass>().AddRange(entities);
            await _context.SaveChangesAsync();
            return entities;
        }
        public async Task<TClass> UpdateAsync(TClass entity)
        {
            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
        public async Task<TClass> UpdatePartialAsync(TDataType id, TClass entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return entity;
        }


        public async Task DeleteAsync(TDataType id)
        {
            _dbSet.Remove(await GetByIdAsync(id));
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(TClass entity)
        {
            if (entity != null)
            {
                _dbSet.Remove(entity);
                await _context.SaveChangesAsync();
            }

        }
        public async Task SoftDeleteAsync(TDataType id)
        {
            var entity = await _context.Set<TClass>().FindAsync(id);
            if (entity != null)
            {
                entity.IsDeleted = true;
                _context.Entry(entity).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }

        }

        public async Task SoftDeleteAsync(TClass entity)
        {

            if (entity != null)
            {
                entity.IsDeleted = true;
                _context.Entry(entity).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }

        }

    }
}