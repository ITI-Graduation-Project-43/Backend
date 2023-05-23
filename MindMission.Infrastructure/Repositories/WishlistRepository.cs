using MindMission.Domain.Models;
using MindMission.Infrastructure.Context;
using MindMission.Application.Repository_Interfaces;

namespace MindMission.Infrastructure.Repositories
{
    public class WishlistRepository : Repository <Wishlist,int>, IWishlistRepository
    {
        private readonly MindMissionDbContext _context;
        public WishlistRepository(MindMissionDbContext context) : base(context) 
        {
            _context = context;
        }
    }
}
