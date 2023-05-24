using MindMission.Application.Repository_Interfaces;
using MindMission.Application.Service_Interfaces;
using MindMission.Domain.Models;
using System.Linq.Expressions;

namespace MindMission.Application.Services
{
    public class WishlistService : IWishlistService
    {
        private readonly IWishlistRepository _context;
        public WishlistService(IWishlistRepository context)
        {
            _context = context;
        }

        public Task<Wishlist> AddAsync(Wishlist entity)
        {
            return _context.AddAsync(entity);
        }

        public Task DeleteAsync(int id)
        {
            return _context.DeleteAsync(id);
        }

        public Task<IEnumerable<Wishlist>> GetAllAsync()
        {
            return _context.GetAllAsync();
        }

        public async Task<IEnumerable<Wishlist>> GetAllAsync(params Expression<Func<Wishlist, object>>[] IncludeProperties)
        {
            return await _context.GetAllAsync(IncludeProperties);
        }

        public Task<IEnumerable<Wishlist>> GetAllByCourseIdAsync(int courseId)
        {
            return _context.GetAllByCourseIdAsync(courseId);
        }

        public Task<IEnumerable<Wishlist>> GetAllByStudentIdAsync(string studentId)
        {
            return _context.GetAllByStudentIdAsync(studentId);
        }

        public Task<Wishlist> GetByIdAsync(int id)
        {
            return _context.GetByIdAsync(id);
        }

        public Task UpdateAsync(Wishlist entity)
        {
            return _context.UpdateAsync(entity);
        }
    }
}
