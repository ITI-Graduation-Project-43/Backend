﻿using Microsoft.EntityFrameworkCore;
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

        public async Task<IQueryable<Wishlist>> GetAllByCourseIdAsync(int CourseId)
        {
            var wishlists = await _dbSet.Where(w => w.CourseId == CourseId && !w.IsDeleted).ToListAsync();
            return wishlists.AsQueryable();
        }

        public async Task<IQueryable<Wishlist>> GetAllByStudentIdAsync(string StudentId)
        {
            var wishlists = await _dbSet.Where(w => w.StudentId == StudentId && !w.IsDeleted).ToListAsync();
            return wishlists.AsQueryable();
        }
    }
}