using Microsoft.EntityFrameworkCore;
using MindMission.Application.Repository_Interfaces;
using MindMission.Domain.Models;
using MindMission.Infrastructure.Context;
using MindMission.Infrastructure.Repositories.Base;

namespace MindMission.Infrastructure.Repositories
{
    public class WishlistRepository : Repository<Wishlist, int>, IWishlistRepository
    {
        private readonly MindMissionDbContext _context;
        private readonly DbSet<Wishlist> _dbSet;

        public WishlistRepository(MindMissionDbContext context) : base(context)
        {
            _context = context;
            _dbSet = _context.Set<Wishlist>();
        }

        public async Task<IQueryable<Wishlist>> GetAllByCourseIdAsync(int courseId)
        {
            var wishlists = await _dbSet.Where(w => w.CourseId == courseId && !w.IsDeleted).ToListAsync();
            return wishlists.AsQueryable();
        }

        public async Task<Wishlist> GetByCourseStudentAsync(int courseId, string studentId)
        {
            var wishlists = await _dbSet.Where(w => w.CourseId == courseId && w.StudentId == studentId && !w.IsDeleted).FirstOrDefaultAsync();
            return wishlists;
        }

        public async Task<IQueryable<Wishlist>> GetAllByStudentIdAsync(string studentId)
        {
            var wishlists = await _dbSet.Where(w => w.StudentId == studentId && !w.IsDeleted).ToListAsync();
            return wishlists.AsQueryable();
        }
    }
}