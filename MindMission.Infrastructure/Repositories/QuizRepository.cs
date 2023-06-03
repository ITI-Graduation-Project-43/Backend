﻿using MindMission.Application.Repository_Interfaces;
using MindMission.Domain.Models;
using MindMission.Infrastructure.Context;
using MindMission.Infrastructure.Repositories.Base;

namespace MindMission.Infrastructure.Repositories
{
    public class QuizRepository : Repository<Quiz, int>, IQuizRepository
    {
        private readonly MindMissionDbContext _context;
        public QuizRepository(MindMissionDbContext context) : base(context)
        {
        }
    }
}