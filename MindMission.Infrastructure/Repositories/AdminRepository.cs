using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using MindMission.Application.DTOs;
using MindMission.Application.Repository_Interfaces;
using MindMission.Domain.Models;
using MindMission.Infrastructure.Context;
using MindMission.Infrastructure.Repositories.Base;

namespace MindMission.Infrastructure.Repositories
{
    public class AdminRepository : Repository<Admin, int>, IAdminRepository
    {
        private readonly MindMissionDbContext _context;

        public AdminRepository(MindMissionDbContext context) : base(context)
        {
            _context = context;
        }

    }
}