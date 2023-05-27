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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace MindMission.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository UserManager;
        private readonly IConfiguration Configuration;
        private readonly IMailService MailService;

        public UserService()
        {
        }

        public UserService(IUserRepository _UserManager, IConfiguration _Configuration, IMailService _MailService) 
        {
            UserManager = _UserManager;
            Configuration = _Configuration;
            MailService = _MailService;
        }

        public async Task<IdentityResult> Registration(User user)
         {
            var CreatedUser = await UserManager.Registration(user);
            if (CreatedUser.Succeeded)
            {
                return IdentityResult.Success;
            }
            return CreatedUser;
        }

        public async Task<SuccessLoginDto?> Login(string Email, string Password)
        {
            var User = await UserManager.Login(Email, Password);

            if(User != null)
            {
                List<Claim> Claims = new List<Claim>() 
                {
                    new Claim("Id", User.Id),
                    new Claim("Email", User.Email),
                    new Claim("Role", "Admin"),
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

        public async Task<IdentityResult> ChangeEmail(string OldEmail, string NewEmail, string Password)
        {
            return await UserManager.ChangeEmail(OldEmail, NewEmail, Password);
        }

        public async Task<IdentityResult> ChangePassword(string Email, string CurrentPassword, string NewPassword)
        {
            return await UserManager.ChangePassword(Email, CurrentPassword, NewPassword);
        }

        public async Task<string?> ForgetPassword(string Email)
        {
            var Result = await UserManager.ForgetPassword(Email);
            if(Result != null)
            {
                MailData mailData = new MailData()
                {
                    EmailTo = Email,
                    EmailToName = Email,
                    EmailSubject = "Reset Your Password",
                    EmailBody = $"Click to the following link to reset your password \n {Configuration["Server:URL"]}/ResetPassword?Email={Email}&Token={Result}",
                };
                if(MailService.SendMail(mailData))
                {
                    return "If your email is found, you will receive a link to reset your password";
                }
            }
            return Result;
        }

        public async Task<IdentityResult> ResetPassword(string Email, string Token, string NewPassword)
        {
            return await UserManager.ResetPassword(Email, Token, NewPassword);
        }
    }
}
