using Microsoft.EntityFrameworkCore;
using MindMission.Application.Interfaces.Repository.Base;
using MindMission.Application.Interfaces.Services.Base;
using MindMission.Domain.Common;
using System.Formats.Asn1;
using System.Linq.Expressions;


namespace MindMission.Application.Services.Base
{
    public class Service<TEntity, TDataType> : IService<TEntity, TDataType> where TEntity : class, IEntity<TDataType>, ISoftDeletable, new()
    {
        protected readonly IRepository<TEntity, TDataType> _baseContext;

        public DbContext Context => throw new NotImplementedException();

        public Service(IRepository<TEntity, TDataType> context)
        {
            _baseContext = context;
        }
        public async Task<int> GetTotalCountAsync()
        {
            return await _baseContext.GetTotalCountAsync();
        }
        public Task<IQueryable<TEntity>> GetAllAsync()
        {
            return _baseContext.GetAllAsync();
        }
        public Task<IEnumerable<TEntity>> GetAllAsync(params Expression<Func<TEntity, object>>[] IncludeProperties)
        {
            return _baseContext.GetAllAsync(IncludeProperties);
        }

        public IQueryable<TEntity> GetAllAsync(int pageNumber, int pageSize)
        {

            return _baseContext.GetAllAsync(pageNumber, pageSize);
        }
        public IQueryable<TEntity> GetAllAsync(int pageNumber, int pageSize, params Expression<Func<TEntity, object>>[] IncludeProperties)
        {

            return _baseContext.GetAllAsync(pageNumber, pageSize, IncludeProperties);
        }

        public Task<TEntity> GetByIdAsync(TDataType id)
        {
            return _baseContext.GetByIdAsync(id);
        }
        public Task<TEntity> GetByIdAsync(TDataType id, params Expression<Func<TEntity, object>>[] IncludeProperties)
        {
            return _baseContext.GetByIdAsync(id, IncludeProperties);
        }

        public virtual Task<TEntity> AddAsync(TEntity entity)
        {
            return _baseContext.AddAsync(entity);
        }
        public Task<IEnumerable<TEntity>> BulkAddAsync(IEnumerable<TEntity> entities)
        {
            return _baseContext.BulkAddAsync(entities);

        }

        public Task<TEntity> UpdateAsync(TEntity entity)
        {
            return _baseContext.UpdateAsync(entity);
        }

        public Task<TEntity> UpdatePartialAsync(TDataType id, TEntity entity)
        {
            return _baseContext.UpdatePartialAsync(id, entity);
        }

        public virtual Task DeleteAsync(TDataType id)
        {
            return _baseContext.DeleteAsync(id);
        }
        public virtual Task DeleteAsync(TEntity entity)
        {
            return _baseContext.DeleteAsync(entity);
        }
        public virtual Task SoftDeleteAsync(TDataType id)
        {
            return _baseContext.SoftDeleteAsync(id);
        }


        public virtual Task SoftDeleteAsync(TEntity entity)
        {
            return _baseContext.SoftDeleteAsync(entity);
        }


    }

}
