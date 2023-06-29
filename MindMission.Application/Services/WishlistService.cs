using MindMission.Application.Repository_Interfaces;
using MindMission.Application.Service_Interfaces;
using MindMission.Application.Services.Base;
using MindMission.Domain.Models;
using System.Linq.Expressions;

namespace MindMission.Application.Services
{
    public class WishlistService : Service<Wishlist, int>, IWishlistService
    {
        private readonly IWishlistRepository _context;

        public WishlistService(IWishlistRepository context) : base(context)
        {
            _context = context;
        }



        public IQueryable<Wishlist> GetAllByCourseIdAsync(int courseId, int pageNumber, int pageSize)
        {
            return _context.GetAllByCourseIdAsync(courseId, pageNumber, pageSize);
        }

        public Task<Wishlist> GetByCourseStudentAsync(int courseId, string studentId)
        {
            return _context.GetByCourseStudentAsync(courseId, studentId);
        }

        public IQueryable<Wishlist> GetAllByStudentIdAsync(string studentId, int pageNumber, int pageSize)
        {
            return _context.GetAllByStudentIdAsync(studentId, pageNumber, pageSize);
        }


    }
}