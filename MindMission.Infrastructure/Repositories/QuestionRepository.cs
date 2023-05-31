using MindMission.Application.Repository_Interfaces;
using MindMission.Domain.Models;
using MindMission.Infrastructure.Context;
using MindMission.Infrastructure.Repositories.Base;

namespace MindMission.Infrastructure.Repositories
{
    public class QuestionRepository : Repository<Question, int>, IQuestionRepository
    {
        public QuestionRepository(MindMissionDbContext context) : base(context)
        {
        }
    }
}