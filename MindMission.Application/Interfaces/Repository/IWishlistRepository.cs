using MindMission.Application.Interfaces.Repository.Base;
using MindMission.Domain.Models;

namespace MindMission.Application.Repository_Interfaces
{
    public interface IWishlistRepository : IRepository<Wishlist, int>
    {
        IQueryable<Wishlist> GetAllByStudentIdAsync(string studentId, int pageNumber, int pageSize);
        IQueryable<Wishlist> GetAllByCourseIdAsync(int courseId, int pageNumber, int pageSize);
        Task<Wishlist> GetByCourseStudentAsync(int courseId, string studentId);

    }
}