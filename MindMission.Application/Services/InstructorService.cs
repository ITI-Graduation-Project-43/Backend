using MindMission.Application.Repository_Interfaces;
using MindMission.Application.Service_Interfaces;
using MindMission.Application.Services.Base;
using MindMission.Domain.Models;
using System.Linq.Expressions;

namespace MindMission.Application.Services
{
    public class InstructorService : Service<Instructor, string>, IInstructorService
    {
        private readonly IInstructorRepository _context;

        public InstructorService(IInstructorRepository context) : base(context)
        {
            _context = context;
        }


        public async Task<IQueryable<Instructor>> GetTopRatedInstructorsAsync(int topNumber)
        {
            return await _context.GetTopRatedInstructorsAsync(topNumber);
        }

    }
}