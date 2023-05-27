using Microsoft.AspNetCore.Identity;
using MindMission.Application.DTOs;
using MindMission.Domain.Models;

namespace MindMission.Application.Interfaces.Repository
{
    public interface IUserRepository
    {
        Task<IdentityResult> Registration(User User);
        Task<SuccessLoginDto?> Login(string Email, string Password);
        Task<IdentityResult> ChangeEmail(string OldEmail, string NewEmail, string Password);
        Task<IdentityResult> ChangePassword(string Email, string CurrentPassword, string NewPassword);
        Task<string?> ForgetPassword(string Email);
        Task<IdentityResult> ResetPassword(string Email, string Token, string NewPassword);
    }
}
