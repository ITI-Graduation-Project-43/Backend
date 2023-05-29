using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MindMission.Application.DTOs;
using MindMission.Application.Interfaces.Repository;
using MindMission.Domain.Models;
using Org.BouncyCastle.Asn1.Ocsp;
using Stripe;
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

        public async Task<IdentityResult> RegistrationAsync(User user)
        {
            return await UserManager.CreateAsync(user, user.PasswordHash);
        }

        public async Task<SuccessLoginDto?> LoginAsync(string Email, string Password)
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

        public async Task<IdentityResult> ChangeEmailAsync(string OldEmail, string NewEmail, string Password)
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

        public async Task<IdentityResult> ChangePasswordAsync(string Email, string CurrentPassword, string NewPassword)
        {
            var User = await UserManager.FindByEmailAsync(Email);
            if (User != null)
            {
                return await UserManager.ChangePasswordAsync(User, CurrentPassword, NewPassword);
            }
            return IdentityResult.Failed();
        }

        public async Task<string?> ForgetPasswordAsync(string Email)
        {
            var User = await UserManager.FindByEmailAsync(Email);
            if (User != null)
            {
                var ResetToken = UserManager.GeneratePasswordResetTokenAsync(User);
                if(ResetToken.IsCompleted)
                {
                    var EncodingResetToken = Encoding.UTF8.GetBytes(ResetToken.Result);
                    var ValidEncodingResetToken = WebEncoders.Base64UrlEncode(EncodingResetToken); // To prevent special characters and make URL that will be generated valid
                    return ValidEncodingResetToken;
                }
            }
            return null;
        }

        public async Task<IdentityResult> ResetPasswordAsync(string Email, string Token, string NewPassword)
        {
            var User = await UserManager.FindByEmailAsync(Email);
            if (User != null)
            {
                var DecodingResetToken = WebEncoders.Base64UrlDecode(Token);
                var ValidToken = Encoding.UTF8.GetString(DecodingResetToken);
                var Result = await UserManager.ResetPasswordAsync(User, ValidToken, NewPassword);
                return Result;
            }
            return IdentityResult.Failed(new IdentityError() { Code="Email Not Found", Description = "This email is not found"});
        }

        public async Task<bool> ValidateTokenAsync(string Email, string Token)
        {
            var User = await UserManager.FindByEmailAsync(Email);
            if (User != null)
            {
                var DecodingResetToken = WebEncoders.Base64UrlDecode(Token);
                var ValidToken = Encoding.UTF8.GetString(DecodingResetToken);
                return await UserManager.VerifyUserTokenAsync(User, "Default", "ResetPassword", ValidToken);
            }
            return false;
        }
    }
}
