﻿using MindMission.Domain.Models;

namespace MindMission.Application.Repository_Interfaces
{
    public interface IInstructorRepository : IRepository<Instructor, string>
    {
        Task<IQueryable<Instructor>> GetTopInstructorsAsync();
    }
}