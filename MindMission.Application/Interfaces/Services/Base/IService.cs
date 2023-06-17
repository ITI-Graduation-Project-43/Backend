using MindMission.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MindMission.Application.Interfaces.Services.Base
{
    public interface IService<TClass, TDataType> where TClass : class, IEntity<TDataType>, new()
    {
        Task<IQueryable<TClass>> GetAllAsync();
        Task<IEnumerable<TClass>> GetAllAsync(params Expression<Func<TClass, object>>[] includeProperties);
        Task<TClass> GetByIdAsync(TDataType id);
        Task<TClass> GetByIdAsync(TDataType id, params Expression<Func<TClass, object>>[] includeProperties);
        Task<TClass> AddAsync(TClass entity);
        Task<TClass> UpdateAsync(TClass entity);
        Task<TClass> UpdatePartialAsync(TDataType id, TClass entity);
        Task DeleteAsync(TDataType id);
        Task SoftDeleteAsync(TDataType id);
    }

}
