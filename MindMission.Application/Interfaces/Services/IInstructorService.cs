﻿using MindMission.Application.Repository_Interfaces;
using MindMission.Domain.Models;

namespace MindMission.Application.Service_Interfaces
{
    public interface IInstructorService : IRepository<Instructor, string>
    {

    }
}
