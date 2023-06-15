using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MindMission.API.EmailSettings;
using MindMission.Application.DTOs;
using MindMission.Application.Interfaces.Repository;
using MindMission.Application.Interfaces.Services;
using MindMission.Domain.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MindMission.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository UserManager;
        private readonly IConfiguration Configuration;
        private readonly IMailService MailService;

        public IStudentService StudentService { get; }

        public UserService()
        {
        }

        public UserService(IUserRepository _UserManager, IConfiguration _Configuration, IMailService _MailService)
        {
            UserManager = _UserManager;
            Configuration = _Configuration;
            MailService = _MailService;
        }

        public async Task<IdentityResult> RegistrationStudentAsync(User user, string FirstName, string LasName)
         {
            return await UserManager.RegistrationStudentAsync(user, FirstName, LasName);
        }

        public async Task<IdentityResult> RegistrationInstructorAsync(User user, string FirstName, string LasName)
        {
            return await UserManager.RegistrationInstructorAsync(user, FirstName, LasName);
        }

        public async Task<SuccessLoginDto?> LoginAsync(string Email, string Password)
        {
            var User = await UserManager.LoginAsync(Email, Password);

            if (User != null)
            {
                List<Claim> Claims = new List<Claim>()
                {
                    new Claim("Id", User.Id),
                    new Claim("Email", User.Email),
                    new Claim("Role", User.Role),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

                JwtSecurityToken JwtSecurityToken = new JwtSecurityToken(
                    //issuer: Configuration["JWT:ValidIssuers"],
                    //audience: Configuration["JWT:ValidAudiences"],
                    claims: Claims,
                    expires: DateTime.Now.AddDays(1),
                    signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JWT:SecretKey"])), SecurityAlgorithms.HmacSha256)
                );

                var CreatedToken = new JwtSecurityTokenHandler().WriteToken(JwtSecurityToken);
                User.Token = CreatedToken;
            }
            return User;
        }

        public async Task<IdentityResult> ChangeEmailAsync(string OldEmail, string NewEmail, string Password)
        {
            return await UserManager.ChangeEmailAsync(OldEmail, NewEmail, Password);
        }

        public async Task<IdentityResult> ChangePasswordAsync(string Email, string CurrentPassword, string NewPassword)
        {
            return await UserManager.ChangePasswordAsync(Email, CurrentPassword, NewPassword);
        }

        public async Task<string?> ForgetPasswordAsync(string Email)
        {
            var Result = await UserManager.ForgetPasswordAsync(Email);
            if (Result != null)
            {
                MailData mailData = new MailData()
                {
                    EmailTo = Email,
                    EmailToName = Email,
                    EmailSubject = "Reset Your Password",
                    EmailBody = $"Click to the following link to reset your password \n {Configuration["Server:URL"]}/ResetPassword?Email={Email}&Token={Result}",
                };
                if (MailService.SendMail(mailData))
                {
                    return "If your email is found, you will receive a link to reset your password";
                }
            }
            return "If your email is found, you will receive a link to reset your password";
        }

        public async Task<IdentityResult> ResetPasswordAsync(string Email, string Token, string NewPassword)
        {
            return await UserManager.ResetPasswordAsync(Email, Token, NewPassword);
        }

        public async Task<bool> ValidateTokenAsync(string Email, string Token)
        {
            return await UserManager.ValidateTokenAsync(Email, Token);
        }

        public async Task<IdentityResult> DeactivateUserAsync(string Email, string Password)
        {
            return await UserManager.DeactivateUserAsync(Email, Password);
        }

        public async Task<IdentityResult> DeleteUserAsync(string Email, string Password)
        {
            return await UserManager.DeleteUserAsync(Email, Password);
        }

        public async Task<IdentityResult> BlockUserAsync(string Email, bool Blocking)
        {
            return await UserManager.BlockUserAsync(Email, Blocking);
        }
    }
}