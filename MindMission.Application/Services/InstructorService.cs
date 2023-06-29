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


        public IQueryable<Instructor> GetTopRatedInstructorsAsync(int topNumber, int pageNumber, int pageSize)
        {
            return _context.GetTopRatedInstructorsAsync(topNumber, pageNumber, pageSize);
        }

        public async Task<int> GetTotalTopRatedInstructorsAsync()
        {
            return await _context.GetTotalTopRatedInstructorsAsync();
        }

    }
}