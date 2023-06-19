using MindMission.Application.Repository_Interfaces;
using MindMission.Application.Service.Interfaces;
using MindMission.Application.Services.Base;
using MindMission.Domain.Models;

namespace MindMission.Application.Services_Classes
{
    public class QuizService : Service<Quiz, int>, IQuizService
    {

        public QuizService(IQuizRepository context) : base(context)
        {
        }

    }
}