using MindMission.Application.DTOs;
using MindMission.Domain.Models;

namespace MindMission.Application.Repository_Interfaces
{
    public interface IWishlistRepository : IRepository<Wishlist, int>
    {
        Task<IQueryable<Wishlist>> GetAllByStudentIdAsync(string StudentId);
        Task<IQueryable<Wishlist>> GetAllByCourseIdAsync(int CourseId);
    }
}