using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using MindMission.Application.DTOs;
using MindMission.Application.Interfaces.Repository;
using MindMission.Domain.Models;
using Org.BouncyCastle.Asn1.Ocsp;
using System.Net;
using System.Net.Mail;
using System.Security.Policy;
using System.Text;
using System.Web;

namespace MindMission.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<User> UserManager;

        public UserRepository(UserManager<User> _UserManager)
        {
            UserManager = _UserManager;
        }

        public async Task<IdentityResult> Registration(User user)
        {
            return await UserManager.CreateAsync(user, user.PasswordHash);
        }

        public async Task<SuccessLoginDto?> Login(string Email, string Password)
        {
            var User = await UserManager.FindByEmailAsync(Email);
            if (User != null)
            {
                var Result = await UserManager.CheckPasswordAsync(User, Password);
                if (Result)
                {
                    SuccessLoginDto SuccessDto = new SuccessLoginDto() {Id = User.Id, Email = User.Email};
                    return SuccessDto;
                }
            }
            return null;
        }

        public async Task<IdentityResult> ChangeEmail(string OldEmail, string NewEmail, string Password)
        {
            var User = await UserManager.FindByEmailAsync(OldEmail);
            if (User != null)
            {
                var Result = await UserManager.CheckPasswordAsync(User, Password);
                if (Result)
                {
                    string Token = await UserManager.GenerateChangeEmailTokenAsync(User, NewEmail);
                    await UserManager.ChangeEmailAsync(User, NewEmail, Token);
                    return IdentityResult.Success;
                }
            }
            return IdentityResult.Failed();
        }

        public async Task<IdentityResult> ChangePassword(string Email, string CurrentPassword, string NewPassword)
        {
            var User = await UserManager.FindByEmailAsync(Email);
            if (User != null)
            {
                return await UserManager.ChangePasswordAsync(User, CurrentPassword, NewPassword);
            }
            return IdentityResult.Failed();
        }

        public async Task<string?> ForgetPassword(string Email)
        {
            var User = await UserManager.FindByEmailAsync(Email);
            if (User != null)
            {
                string ResetToken = await UserManager.GeneratePasswordResetTokenAsync(User);
                var EncodingResetToken = Encoding.UTF8.GetBytes(ResetToken);
                var ValidEncodingResetToken = WebEncoders.Base64UrlEncode(EncodingResetToken); // To prevent special characters and make URL that will be generated valid
                return ValidEncodingResetToken;
            }
            return null;
        }

        public async Task<IdentityResult> ResetPassword(string Email, string Token, string NewPassword)
        {
            var User = await UserManager.FindByEmailAsync(Email);
            if (User != null)
            {
                var DecodingResetToken = WebEncoders.Base64UrlDecode(Token);
                var ValidToken = Encoding.UTF8.GetString(DecodingResetToken);
                var Result = await UserManager.ResetPasswordAsync(User, ValidToken, NewPassword);
                if(Result.Succeeded)
                {
                    return Result;
                }
                return Result;
            }
            return IdentityResult.Failed();
        }

    }
}
