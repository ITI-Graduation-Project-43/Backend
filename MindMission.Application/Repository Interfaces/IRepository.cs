using MindMission.Domain;

namespace MindMission.Application.Repository_Interfaces
{
    public interface IRepository<T> where T : class, IEntity, new()
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        Task<T> AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(int id);
    }
}
