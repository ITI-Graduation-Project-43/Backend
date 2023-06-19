using MindMission.Application.Interfaces.Repository.Base;
using MindMission.Domain.Models;

namespace MindMission.Application.Repository_Interfaces
{
    public interface IWishlistRepository : IRepository<Wishlist, int>
    {
        Task<IQueryable<Wishlist>> GetAllByStudentIdAsync(string studentId);
        Task<IQueryable<Wishlist>> GetAllByCourseIdAsync(int courseId);
    }
}