using Microsoft.AspNetCore.Identity;
using MindMission.Application.DTOs;
using MindMission.Domain.Models;

namespace MindMission.Application.Interfaces.Repository
{
    public interface IUserRepository
    {
        Task<IdentityResult> RegistrationAsync(User User, string FirstName, string LasName);
        Task<SuccessLoginDto?> LoginAsync(string Email, string Password);
        Task<IdentityResult> ChangeEmailAsync(string OldEmail, string NewEmail, string Password);
        Task<IdentityResult> ChangePasswordAsync(string Email, string CurrentPassword, string NewPassword);
        Task<string?> ForgetPasswordAsync(string Email);
        Task<IdentityResult> ResetPasswordAsync(string Email, string Token, string NewPassword);
        Task<bool> ValidateTokenAsync(string Email, string Token);
        Task<IdentityResult> DeactivateUserAsync(string Email, string Password);
        Task<IdentityResult> DeleteUserAsync(string Email, string Password);
        Task<IdentityResult> BlockUserAsync(string Email, bool Blocking);


    }
}
