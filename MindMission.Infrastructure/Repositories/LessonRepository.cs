using MindMission.Application.Repository_Interfaces;
using MindMission.Domain.Models;
using MindMission.Infrastructure.Context;
using MindMission.Infrastructure.Repositories.Base;

namespace MindMission.Infrastructure.Repositories
{
    public class LessonRepository : Repository<Lesson, int>, ILessonRepository
    {
        public LessonRepository(MindMissionDbContext context) : base(context)
        {
        }
    }
}