﻿using MindMission.Application.Repository_Interfaces;
using MindMission.Application.Service_Interfaces;
using MindMission.Domain.Models;
using System.Linq.Expressions;

namespace MindMission.Application.Services
{
    public class DiscussionService : IDiscussionService
    {
        private readonly IDiscussionRepository _context;

        public DiscussionService(IDiscussionRepository context)
        {
            _context = context;
        }

        public Task<IQueryable<Discussion>> GetAllAsync()
        {
            return _context.GetAllAsync();
        }

        public Task<IQueryable<Discussion>> GetAllDiscussionByLessonIdAsync(int lessonId)
        {
            return _context.GetAllDiscussionByLessonIdAsync(lessonId);
        }

        public Task<Discussion> GetByIdAsync(int id)
        {
            return _context.GetByIdAsync(id);
        }

        public Task<Discussion> AddAsync(Discussion entity)
        {
            return _context.AddAsync(entity);
        }

        public Task<Discussion> UpdateAsync(Discussion entity)
        {
            return _context.UpdateAsync(entity);
        }
        public async Task<Discussion> UpdatePartialAsync(int id, Discussion entity)
        {
            return await _context.UpdatePartialAsync(id, entity);
        }
        public Task DeleteAsync(int id)
        {
            return _context.DeleteAsync(id);
        }
        public Task SoftDeleteAsync(int id)
        {
            return _context.SoftDeleteAsync(id);
        }
        public async Task<IEnumerable<Discussion>> GetAllAsync(params Expression<Func<Discussion, object>>[] IncludeProperties)
        {
            return await _context.GetAllAsync(IncludeProperties);
        }

        public Task<Discussion> GetByIdAsync(int id, params Expression<Func<Discussion, object>>[] IncludeProperties)
        {
            return _context.GetByIdAsync(id, IncludeProperties);
        }

        public async Task<IQueryable<Discussion>> GetAllDiscussionByParentIdAsync(int parentId)
        {
            return await _context.GetAllDiscussionByParentIdAsync(parentId);
        }


    }
}