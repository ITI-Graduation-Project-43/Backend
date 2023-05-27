using MindMission.Domain.Common;
using System.Linq.Expressions;

namespace MindMission.Application.Repository_Interfaces
{
    public interface IRepository<TClass, TDataType> where TClass : class, IEntity<TDataType>, new()
    {
        Task<IEnumerable<TClass>> GetAllAsync();
        Task<TClass> GetByIdAsync(TDataType id);
        Task<TClass> AddAsync(TClass entity);
        Task UpdateAsync(TClass entity);
        Task DeleteAsync(TDataType id);

        //Overloading methods to be able to "include" navigation properties
        Task<IEnumerable<TClass>> GetAllAsync(params Expression<Func<TClass, Object>>[] IncludeProperties);
        Task<TClass> GetByIdAsync(TDataType id, params Expression<Func<TClass, Object>>[] IncludeProperties);
    }
}
