using MindMission.Domain.Common;

namespace MindMission.Application.Repository_Interfaces
{
    public interface IRepository<TClass, TDataType> where TClass : class, IEntity<TDataType>, new()
    {
        Task<IEnumerable<TClass>> GetAllAsync();
        Task<TClass> GetByIdAsync(TDataType id);
        Task<TClass> AddAsync(TClass entity);
        Task UpdateAsync(TClass entity);
        Task DeleteAsync(TDataType id);
    }
}
