using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using MindMission.API.Controllers.Base;
using MindMission.API.EmailSettings;
using MindMission.API.Utilities;
using MindMission.Application.DTOs;
using MindMission.Application.Factories;
using MindMission.Application.Interfaces.Services;
using MindMission.Application.Mapping;
using MindMission.Application.Service_Interfaces;
using MindMission.Application.Services;
using MindMission.Domain.DTOs;
using MindMission.Domain.Models;
using System.ComponentModel.DataAnnotations;

namespace MindMission.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService UserService;
        private readonly IMappingService<User, UserDto> UserMappingService;
        public UserController(IUserService _UserService, IMappingService<User, UserDto> _UserMappingService)
        {
            UserService = _UserService;
            UserMappingService = _UserMappingService;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Registration(UserDto UserDto)
        {
            if (ModelState.IsValid)
            {
                var Result = await UserService.Registration(UserMappingService.MapDtoToEntity(UserDto));
                if (Result.Succeeded)
                {
                    return Ok(ResponseObjectFactory.CreateResponseObject(true, "Succeeded registration", new List<UserDto>()));
                }
                return BadRequest(ResponseObjectFactory.CreateResponseObject(false, ModelStateErrors.BadRequestError(ModelState), new List<UserDto>()));            }
            else
            {
                return BadRequest(ResponseObjectFactory.CreateResponseObject(false, ModelStateErrors.BadRequestError(ModelState), new List<UserDto>()));
            }
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginDto LoginDto)
        {
            if (ModelState.IsValid)
            {
                var User = await UserService.Login(LoginDto.Email, LoginDto.Password);
                if (User != null)
                {
                    var Info = new List<SuccessLoginDto>();
                    Info.Add(User);
                    return Ok(ResponseObjectFactory.CreateResponseObject(true, "Login Succeeded", Info));
                }
                return Unauthorized(ResponseObjectFactory.CreateResponseObject(false, "Login Failed, Your email or password incorrect", new List<SuccessLoginDto>()));
            }
            return BadRequest(ResponseObjectFactory.CreateResponseObject(false, ModelStateErrors.BadRequestError(ModelState), new List<UserDto>())); }

        [HttpPost("Change/Email")]
        public async Task<IActionResult> ChangeEmail(ChangeEmailDto ChangeEmailDto)
        {
            if (ModelState.IsValid)
            {
                var Result = await UserService.ChangeEmail(ChangeEmailDto.OldEmail, ChangeEmailDto.NewEmail, ChangeEmailDto.Password);
                if (Result.Succeeded)
                {
                    return Ok(ResponseObjectFactory.CreateResponseObject(true, "Email has been changed successfully", new List<ChangeEmailDto>()));
                }
                return Unauthorized(ResponseObjectFactory.CreateResponseObject(false, "Your email or password incorrect", new List<ChangeEmailDto>()));
            }
            else
            {
                return BadRequest(ResponseObjectFactory.CreateResponseObject(false, ModelStateErrors.BadRequestError(ModelState), new List<UserDto>()));
            }

        }

        [HttpPost("Change/Password")]
        public async Task<IActionResult> ChangePassword(ChangePasswordDto ChangePasswordDto)
        {
            if (ModelState.IsValid)
            {
                var Result = await UserService.ChangePassword(ChangePasswordDto.Email, ChangePasswordDto.CurrentPassword, ChangePasswordDto.NewPassword);
                if (Result.Succeeded)
                {
                    return Ok(ResponseObjectFactory.CreateResponseObject(true, "Password has been changed successfully", new List<ChangePasswordDto>()));
                }
                return Unauthorized(ResponseObjectFactory.CreateResponseObject(false, "Your email or password incorrect", new List<ChangeEmailDto>()));
            }

            return BadRequest(ResponseObjectFactory.CreateResponseObject(false, ModelStateErrors.BadRequestError(ModelState), new List<UserDto>()));

        }

        [HttpPost]
        [Route("ForgetPassword")]
        public async Task<IActionResult> ForgetPassword([FromBody][EmailAddress] string Email)
        {
            if (Email != null)
            {
                if (ModelState.IsValid)
                {
                    var Result = await UserService.ForgetPassword(Email);
                    return Ok(ResponseObjectFactory.CreateResponseObject(true, Result, new List<string>()));
                }
                return BadRequest(ResponseObjectFactory.CreateResponseObject(false, ModelStateErrors.BadRequestError(ModelState), new List<UserDto>()));
            }
            return BadRequest(ResponseObjectFactory.CreateResponseObject(false, "The email is required", new List<string>()));
        }

        [HttpPost]
        [Route("ResetPassword")]
        public async Task<IActionResult> ConfirmResetPassword([FromForm] ResetPasswordDto ResetPasswordDto)
        {
            if (ResetPasswordDto != null)
            {
                if (ModelState.IsValid)
                {
                    var Result = await UserService.ResetPassword(ResetPasswordDto.Email, ResetPasswordDto.Token, ResetPasswordDto.Password);
                    if(Result.Succeeded)
                    {
                        return Ok(ResponseObjectFactory.CreateResponseObject(true, "Password has been reset successfully", new List<string>()));
                    }
                    string Errors = string.Empty;
                    foreach(var item in Result.Errors)
                    {
                        Errors += item.Description + ", ";
                    }
                    return BadRequest(ResponseObjectFactory.CreateResponseObject(false, Errors.Substring(0, Errors.Length - 2), new List<UserDto>()));

                }
                return BadRequest(ResponseObjectFactory.CreateResponseObject(false, ModelStateErrors.BadRequestError(ModelState), new List<UserDto>()));
            }
            return BadRequest(ResponseObjectFactory.CreateResponseObject(false, "All fields are required", new List<ResetPasswordDto>()));
        }

    }
}
