using Microsoft.AspNetCore.Mvc;
using MindMission.Application.DTOs;
using MindMission.Application.Interfaces.Repository;
using MindMission.Application.Repository_Interfaces;
using MindMission.Application.Responses;
using MindMission.Domain.Models;
using System.Formats.Asn1;

namespace MindMission.Application.Interfaces.Services
{
    public interface IUserService : IUserRepository
    {
    }
}
