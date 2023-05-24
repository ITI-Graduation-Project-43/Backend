using MindMission.Application.Interfaces.Repository;
using MindMission.Domain.Models;
using MindMission.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindMission.Infrastructure.Repositories
{
    public class StudentRepository: Repository<Student, string>,IStudentRepository
    {
        private readonly MindMissionDbContext _context;
        public StudentRepository(MindMissionDbContext context) : base(context)
        {
            _context = context;

        }
    }
}
