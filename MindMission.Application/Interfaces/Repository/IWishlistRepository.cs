using MindMission.Domain.Models;

namespace MindMission.Application.Repository_Interfaces
{
    public interface IWishlistRepository : IRepository<Wishlist, int>
    {
        Task<IEnumerable<Wishlist>> GetAllByStudentIdAsync(string StudentId);
        Task<IEnumerable<Wishlist>> GetAllByCourseIdAsync(int CourseId);
    }
}
