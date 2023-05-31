using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using MindMission.Application.DTOs;
using MindMission.Application.Interfaces.Repository;
using MindMission.Application.Interfaces.Services;
using MindMission.Domain.Models;
using System.Text;

namespace MindMission.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<User> UserManager;
        private readonly IStudentService StudentService;

        public UserRepository(UserManager<User> _UserManager, IStudentService _StudentService)
        {
            UserManager = _UserManager;
            StudentService = _StudentService;
        }

        public async Task<IdentityResult> RegistrationAsync(User user, string FirstName, string LasName)
        {
            var Result = await UserManager.CreateAsync(user, user.PasswordHash);
            if (Result.Succeeded)
            {
                var NewUser = await UserManager.FindByEmailAsync(user.Email);
                Student Std = new Student();
                Std.Id = NewUser.Id;
                Std.FirstName = FirstName;
                Std.LastName = LasName;
                var NewStudent = await StudentService.AddAsync(Std);
                if (NewStudent != null)
                {
                    return IdentityResult.Success;
                }
                return IdentityResult.Failed(new IdentityError() { Code = "Error", Description = "Something wrong during add new student" });
            }
            return Result;
        }

        public async Task<SuccessLoginDto?> LoginAsync(string Email, string Password)
        {
            var User = await UserManager.FindByEmailAsync(Email);
            if (User != null)
            {
                if (!User.IsBlocked)
                {
                    var Result = await UserManager.CheckPasswordAsync(User, Password);
                    if (Result)
                    {
                        User.IsActive = true;
                        User.IsDeactivated = false;
                        await UserManager.UpdateAsync(User);
                        SuccessLoginDto SuccessDto = new SuccessLoginDto() { Id = User.Id, Email = User.Email };
                        return SuccessDto;
                    }
                }
            }
            return null;
        } //No message at blocking

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
                if (ResetToken.IsCompleted)
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
            return IdentityResult.Failed(new IdentityError() { Code = "Email Not Found", Description = "This email is not found" });
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

        public async Task<IdentityResult> DeactivateUserAsync(string Email, string Password)
        {
            var User = await UserManager.FindByEmailAsync(Email);
            if (User != null)
            {
                var Result = await UserManager.CheckPasswordAsync(User, Password);
                if (Result)
                {
                    User.IsDeactivated = true;
                    User.IsActive = false;
                    return await UserManager.UpdateAsync(User);
                }
            }
            return IdentityResult.Failed(new IdentityError() { Code = "Access Field", Description = "Your email or password incorrect." });
        }

        public async Task<IdentityResult> DeleteUserAsync(string Email, string Password)
        {
            var User = await UserManager.FindByEmailAsync(Email);
            if (User != null)
            {
                var Result = await UserManager.CheckPasswordAsync(User, Password);
                if (Result)
                {
                    User.IsDeleted = true;
                    User.Email = null;
                    User.NormalizedEmail = null;
                    User.EmailConfirmed = false;
                    return await UserManager.UpdateAsync(User);
                }
            }
            return IdentityResult.Failed(new IdentityError() { Code = "Access Field", Description = "Your email or password incorrect." });
        }

        public async Task<IdentityResult> BlockUserAsync(string Email, bool Blocking)
        {
            var User = await UserManager.FindByEmailAsync(Email);
            if (User != null)
            {
                User.IsBlocked = Blocking;
                return await UserManager.UpdateAsync(User);
            }
            return IdentityResult.Failed(new IdentityError() { Code = "This email is not found", Description = "This email is not found" });
        }
    }
}