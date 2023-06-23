using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using MindMission.Application.DTOs;
using MindMission.Application.Interfaces.Repository;
using MindMission.Domain.Models;
using MindMission.Infrastructure.Context;
using System.Text;

namespace MindMission.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<User> UserManager;
        private readonly MindMissionDbContext Context;

        public UserRepository(UserManager<User> _UserManager, MindMissionDbContext _Context)
        {
            UserManager = _UserManager;
            Context = _Context;
        }

        public async Task<IdentityResult> RegistrationStudentAsync(User user, string _FirstName, string _LastName)
        {
            user.Id = Guid.NewGuid().ToString();
            user.Students.Add(new Student() { Id = user.Id, FirstName = _FirstName, LastName = _LastName, ProfilePicture = "https://mindmission.blob.core.windows.net/photos/42b273dd-9a89-49ee-b693-81a8c87c4e43.jpg" });
            var Result = await UserManager.CreateAsync(user, user.PasswordHash);
            if (Result.Succeeded)
            {
                try
                {
                    var RoleResult = await UserManager.AddToRoleAsync(user, "Student");
                    if (RoleResult.Succeeded)
                    {
                        return IdentityResult.Success;
                    }
                }
                catch (Exception ex)
                {
                    await UserManager.DeleteAsync(user);
                    return IdentityResult.Failed(new IdentityError() { Code = "Role Error", Description = ex.Message });
                }
            }
            return Result;
        }

        public async Task<IdentityResult> RegistrationInstructorAsync(User user, string _FirstName, string _LastName)
        {
            user.Id = Guid.NewGuid().ToString();
            user.Instructors.Add(new Instructor() { Id = user.Id, FirstName = _FirstName, LastName = _LastName, ProfilePicture = "https://mindmission.blob.core.windows.net/photos/42b273dd-9a89-49ee-b693-81a8c87c4e43.jpg" });
            var Result = await UserManager.CreateAsync(user, user.PasswordHash);
            if (Result.Succeeded)
            {
                try
                {
                    var RoleResult = await UserManager.AddToRoleAsync(user, "Instructor");
                    if (RoleResult.Succeeded)
                    {
                        return IdentityResult.Success;
                    }
                }
                catch (Exception ex)
                {
                    await UserManager.DeleteAsync(user);
                    return IdentityResult.Failed(new IdentityError() { Code = "Role Error", Description = ex.Message });
                }
            }
            return Result;
        }

        public async Task<IdentityResult> RegistrationAdminAsync(User user, string _FirstName, string _LastName, List<int> PermissionIds)
        {
            user.Id = Guid.NewGuid().ToString();
            var AdminPermissions = new List<AdminPermission>();
            foreach(var permission in PermissionIds)
            {
                AdminPermissions.Add(new AdminPermission() { Id = user.Id, PermissionId = permission }); 
            }
            user.Admins.Add(new Admin() { Id = user.Id, FirstName = _FirstName, LastName = _LastName, ProfilePicture = "https://mindmission.blob.core.windows.net/photos/42b273dd-9a89-49ee-b693-81a8c87c4e43.jpg", AdminPermissions = AdminPermissions});
            var Result = await UserManager.CreateAsync(user, user.PasswordHash);
            try
            {
                if (Result.Succeeded)
                {
                    var RoleResult = await UserManager.AddToRoleAsync(user, "Admin");
                    if (RoleResult.Succeeded)
                    {
                        return IdentityResult.Success;
                    }
                }
                return Result;
            }
            catch (Exception ex)
            {
                await UserManager.DeleteAsync(user);
                return IdentityResult.Failed(new IdentityError() { Code = "Role Error", Description = ex.Message });
            }
        }

        public async Task<bool> ChangeEmailFoundAsync(string Email)
        {
            var User = await UserManager.FindByEmailAsync(Email);
            if (User != null)
            {
                return true;
            }
            return false;
        }
        
        public async Task<SuccessLoginDto?> LoginAsync(string Email, string Password)
        {
            var User = Context.Users.AsSplitQuery().Include(e => e.Admins).Include(e => e.Students).Include(e => e.Instructors).Where(e => e.Email == Email).FirstOrDefault();

            if (User != null)
            {
                if (!User.IsBlocked)
                {
                    var Result = await UserManager.CheckPasswordAsync(User, Password);
                    if (Result)
                    {  
                        var role = await UserManager.GetRolesAsync(User);
                        string FullName = string.Empty;
                        if (role[0] == "Student")
                        {
                            var Student = User.Students.Where(e => e.Id == User.Id).FirstOrDefault();
                            FullName = Student?.FirstName + " " + Student?.LastName;
                        }
                        else if(role[0] == "Instructor")
                        {
                            var Instructor = User.Instructors.Where(e => e.Id == User.Id).FirstOrDefault();
                            FullName = Instructor?.FirstName + " " + Instructor?.LastName;
                        }
                        else
                        {
                            var Admin = User.Admins.Where(e => e.Id == User.Id).FirstOrDefault();
                            FullName = Admin?.FirstName + " " + Admin?.LastName;
                        }
                        User.IsActive = true;
                        User.IsDeactivated = false;
                        await UserManager.UpdateAsync(User);
                        SuccessLoginDto SuccessDto = new SuccessLoginDto() { Id = User.Id, Email = User.Email, Role = role[0], FullName = FullName};
                        return SuccessDto;
                    }
                }
                return new SuccessLoginDto() { Id = "blocked" };
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
                    User.Email += "," + User.Id;
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