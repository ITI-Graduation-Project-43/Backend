﻿using MindMission.Application.Repository_Interfaces;
using MindMission.Application.Service_Interfaces;
using MindMission.Domain.Models;
using System.Linq.Expressions;

namespace MindMission.Application.Services
{
    public class InstructorService : IInstructorService
    {
        private readonly IInstructorRepository _context;

        public InstructorService(IInstructorRepository context)
        {
            _context = context;
        }

        public Task<IQueryable<Instructor>> GetAllAsync()
        {
            return _context.GetAllAsync();
        }
        public async Task<IEnumerable<Instructor>> GetAllAsync(params Expression<Func<Instructor, object>>[] IncludeProperties)
        {
            return await _context.GetAllAsync(IncludeProperties);
        }
        public Task<Instructor> GetByIdAsync(string id)
        {
            return _context.GetByIdAsync(id);
        }
        public Task<Instructor> GetByIdAsync(string id, params Expression<Func<Instructor, object>>[] IncludeProperties)
        {
            return _context.GetByIdAsync(id, IncludeProperties);
        }

        public Task<Instructor> AddAsync(Instructor entity)
        {
            return _context.AddAsync(entity);
        }

        public Task UpdateAsync(Instructor entity)
        {
            return _context.UpdateAsync(entity);
        }

        public Task DeleteAsync(string id)
        {
            return _context.DeleteAsync(id);
        }

        public async Task<IQueryable<Instructor>> GetTopInstructorsAsync(int topNumber)
        {
            return await _context.GetTopRatedInstructorsAsync(topNumber);
        }

    }
}