using Microsoft.EntityFrameworkCore;
using MindMission.Domain.Common;
using MindMission.Domain.Models;
using System.Linq.Expressions;

namespace MindMission.Application.Interfaces.Repository.Base
{
    public interface IRepository<TClass, TDataType> where TClass : class, IEntity<TDataType>, new()
    {
        DbContext Context { get; }

        Task<IQueryable<TClass>> GetAllAsync();
        Task<IEnumerable<TClass>> GetAllAsync(params Expression<Func<TClass, object>>[] IncludeProperties);
        Task<TClass> GetByIdAsync(TDataType id);
        Task<TClass> GetByIdAsync(TDataType id, params Expression<Func<TClass, object>>[] IncludeProperties);

        Task<TClass> AddAsync(TClass entity);

        Task<TClass> UpdateAsync(TClass entity);
        Task<TClass> UpdatePartialAsync(TDataType id, TClass entity);
        Task DeleteAsync(TDataType id);
        Task DeleteAsync(TClass entity);

        Task SoftDeleteAsync(TDataType id);
        Task SoftDeleteAsync(TClass entity);

    }
}