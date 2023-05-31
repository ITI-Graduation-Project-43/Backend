﻿using Microsoft.EntityFrameworkCore;
using MindMission.Application.Repository_Interfaces;
using MindMission.Domain.Models;
using MindMission.Infrastructure.Context;
using MindMission.Infrastructure.Repositories.Base;

namespace MindMission.Infrastructure.Repositories
{
    public class InstructorRepository : Repository<Instructor, string>, IInstructorRepository
    {
        private readonly MindMissionDbContext _context;

        public InstructorRepository(MindMissionDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IQueryable<Instructor>> GetTopInstructorsAsync()
        {
            var topInstructors = await _context.Instructors
           .OrderByDescending(i => i.NoOfRatings * i.AvgRating)
           .Take(10)
           .ToListAsync();
         return topInstructors.AsQueryable();
        }
    }
}