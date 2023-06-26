using Microsoft.AspNetCore.Mvc;
using MindMission.API.Utilities;
using MindMission.Application.DTOs;
using MindMission.Application.DTOs.AdminDtos;
using MindMission.Application.DTOs.UserDtos;
using MindMission.Application.Factories;
using MindMission.Application.Interfaces.Services;
using MindMission.Application.Mapping.Base;
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

        [HttpPost("Register/Student")]
        public async Task<IActionResult> RegistrationStudentAsync(UserDto UserDto)
        {
            if (ModelState.IsValid)
            {
                var Result = await UserService.RegistrationStudentAsync(UserMappingService.MapDtoToEntity(UserDto), UserDto.FirstName, UserDto.LastName);
                if (Result.Succeeded)
                {
                    return Ok(ResponseObjectFactory.CreateResponseObject(true, "Registration Succeeded", new List<UserDto>()));
                }

                string Errors = string.Empty;
                foreach (var Error in Result.Errors)
                {
                    Errors += Error.Description.Substring(0, Error.Description.Length - 1) + ", ";
                }
                return BadRequest(ResponseObjectFactory.CreateResponseObject(false, Errors.Substring(0, Errors.Length - 2), new List<UserDto>()));
            }
            else
            {
                return BadRequest(ResponseObjectFactory.CreateResponseObject(false, ModelStateErrors.BadRequestError(ModelState), new List<UserDto>()));
            }
        }

        [HttpPost("Register/Students")]
        public async Task<IActionResult> RegistrationStudentsAsync(List<UserDto> userDtos)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ResponseObjectFactory.CreateResponseObject(false, ModelStateErrors.BadRequestError(ModelState), new List<UserDto>()));
            }

            var successfulRegistrations = new List<UserDto>();
            var failedRegistrations = new List<UserDto>();
            var errors = new List<string>();

            foreach (var userDto in userDtos)
            {
                var result = await UserService.RegistrationStudentAsync(UserMappingService.MapDtoToEntity(userDto), userDto.FirstName, userDto.LastName);

                if (result.Succeeded)
                {
                    successfulRegistrations.Add(userDto);
                }
                else
                {
                    failedRegistrations.Add(userDto);
                    var errorMessages = result.Errors.Select(error => error.Description.TrimEnd(','));
                    errors.AddRange(errorMessages);
                }
            }

            if (failedRegistrations.Count > 0)
            {
                var errorMessage = string.Join(", ", errors);
                return BadRequest(ResponseObjectFactory.CreateResponseObject(false, errorMessage, failedRegistrations));
            }

            return Ok(ResponseObjectFactory.CreateResponseObject(true, "Registration succeeded", successfulRegistrations));
        }

        [HttpPost("Register/Instructor")]
        public async Task<IActionResult> RegistrationInstructorAsync(UserDto UserDto)
        {
            if (ModelState.IsValid)
            {
                var Result = await UserService.RegistrationInstructorAsync(UserMappingService.MapDtoToEntity(UserDto), UserDto.FirstName, UserDto.LastName);
                if (Result.Succeeded)
                {
                    return Ok(ResponseObjectFactory.CreateResponseObject(true, "Registration Succeeded", new List<UserDto>()));
                }

                string Errors = string.Empty;
                foreach (var Error in Result.Errors)
                {
                    Errors += Error.Description.Substring(0, Error.Description.Length - 1) + ", ";
                }
                return BadRequest(ResponseObjectFactory.CreateResponseObject(false, Errors.Substring(0, Errors.Length - 2), new List<UserDto>()));
            }
            else
            {
                return BadRequest(ResponseObjectFactory.CreateResponseObject(false, ModelStateErrors.BadRequestError(ModelState), new List<UserDto>()));
            }
        }

        [HttpPost("Register/Admin")]
        public async Task<IActionResult> RegistrationAdminAsync(AdminCreateDto AdminCreateDto)
        {
            if (ModelState.IsValid)
            {
                var Result = await UserService.RegistrationAdminAsync(UserMappingService.MapDtoToEntity(new UserDto() { Email = AdminCreateDto.Email, Password = AdminCreateDto.Password }), AdminCreateDto.FirstName, AdminCreateDto.LastName, AdminCreateDto.PermissionId);
                if (Result.Succeeded)
                {
                    return Ok(ResponseObjectFactory.CreateResponseObject(true, "Registration Succeeded", new List<UserDto>()));
                }

                string Errors = string.Empty;
                foreach (var Error in Result.Errors)
                {
                    Errors += Error.Description.Substring(0, Error.Description.Length - 1) + ", ";
                }
                return BadRequest(ResponseObjectFactory.CreateResponseObject(false, Errors.Substring(0, Errors.Length - 2), new List<UserDto>()));
            }
            else
            {
                return BadRequest(ResponseObjectFactory.CreateResponseObject(false, ModelStateErrors.BadRequestError(ModelState), new List<UserDto>()));
            }
        }

        [HttpPost("Login")]
        public async Task<IActionResult> LoginAsync(LoginDto LoginDto)
        {
            if (ModelState.IsValid)
            {
                var User = await UserService.LoginAsync(LoginDto.Email, LoginDto.Password);
                if (User != null)
                {
                    var Info = new List<SuccessLoginDto>();
                    if (User.Id != "blocked")
                    {
                        Info.Add(User);
                        return Ok(ResponseObjectFactory.CreateResponseObject(true, "Login Succeeded", Info));
                    }
                    return Ok(ResponseObjectFactory.CreateResponseObject(false, "Login Failed, You are blocked", new List<SuccessLoginDto>()));
                }
                return Unauthorized(ResponseObjectFactory.CreateResponseObject(false, "Login Failed, Your email or password incorrect", new List<SuccessLoginDto>()));
            }
            return BadRequest(ResponseObjectFactory.CreateResponseObject(false, ModelStateErrors.BadRequestError(ModelState), new List<UserDto>()));
        }

        [HttpPost("Change/Email")]
        public async Task<IActionResult> ChangeEmailAsync(ChangeEmailDto ChangeEmailDto)
        {
            if (ModelState.IsValid)
            {
                var Result = await UserService.ChangeEmailAsync(ChangeEmailDto.OldEmail, ChangeEmailDto.NewEmail, ChangeEmailDto.Password);
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

        [HttpPost("Email")]
        public async Task<IActionResult> ChangeEmailAsync([EmailAddress] string Email = null)
        {
            if (ModelState.IsValid || Email != null)
            {
                var Result = await UserService.ChangeEmailFoundAsync(Email);
                if (Result)
                {
                    return Ok(ResponseObjectFactory.CreateResponseObject(false, "This email is already used", new List<string>()));
                }
                return Ok(ResponseObjectFactory.CreateResponseObject(true, "This email is not found", new List<string>()));
            }
            return BadRequest(ResponseObjectFactory.CreateResponseObject(false, "Invalid Email", new List<string>()));
        }

        [HttpPost("Change/Password")]
        public async Task<IActionResult> ChangePasswordAsync(ChangePasswordDto ChangePasswordDto)
        {
            if (ModelState.IsValid)
            {
                var Result = await UserService.ChangePasswordAsync(ChangePasswordDto.Email, ChangePasswordDto.CurrentPassword, ChangePasswordDto.NewPassword);
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
        public async Task<IActionResult> ForgetPasswordAsync([EmailAddress] string Email)
        {
            if (Email != null)
            {
                if (ModelState.IsValid)
                {
                    var Result = await UserService.ForgetPasswordAsync(Email);
                    return Ok(ResponseObjectFactory.CreateResponseObject(true, Result, new List<string>()));
                }
                return BadRequest(ResponseObjectFactory.CreateResponseObject(false, ModelStateErrors.BadRequestError(ModelState), new List<UserDto>()));
            }
            return BadRequest(ResponseObjectFactory.CreateResponseObject(false, "The email is required", new List<string>()));
        }

        [HttpPost]
        [Route("ValidateToken")]
        public async Task<IActionResult> ValidateTokenAsync([EmailAddress] string Email, string Token)
        {
            if (Email != null && Token != null)
            {
                if (ModelState.IsValid)
                {
                    var Result = await UserService.ValidateTokenAsync(Email, Token);
                    if (Result)
                    {
                        return Ok(ResponseObjectFactory.CreateResponseObject(Result, "The token is valid", new List<string>()));
                    }
                    return Ok(ResponseObjectFactory.CreateResponseObject(Result, "The token is expired", new List<string>()));
                }
                return BadRequest(ResponseObjectFactory.CreateResponseObject(false, ModelStateErrors.BadRequestError(ModelState), new List<UserDto>()));
            }
            return BadRequest(ResponseObjectFactory.CreateResponseObject(false, "The email or token is missed", new List<string>()));
        }

        [HttpPost]
        [Route("ResetPassword")]
        public async Task<IActionResult> ConfirmResetPasswordAsync(ResetPasswordDto ResetPasswordDto)
        {
            if (ResetPasswordDto != null)
            {
                if (ModelState.IsValid)
                {
                    var Result = await UserService.ResetPasswordAsync(ResetPasswordDto.Email, ResetPasswordDto.Token, ResetPasswordDto.Password);
                    if (Result.Succeeded)
                    {
                        return Ok(ResponseObjectFactory.CreateResponseObject(true, "Password is reset successfully", new List<string>()));
                    }
                    string Errors = string.Empty;
                    foreach (var Error in Result.Errors)
                    {
                        Errors += Error.Description.Substring(0, Error.Description.Length - 1) + ", ";
                    }
                    return BadRequest(ResponseObjectFactory.CreateResponseObject(false, Errors.Substring(0, Errors.Length - 2), new List<UserDto>()));
                }
                return BadRequest(ResponseObjectFactory.CreateResponseObject(false, ModelStateErrors.BadRequestError(ModelState), new List<UserDto>()));
            }
            return BadRequest(ResponseObjectFactory.CreateResponseObject(false, "All fields are required", new List<ResetPasswordDto>()));
        }

        [HttpPost]
        [Route("Deactivate")]
        public async Task<IActionResult> DeactivateUserAsync(LoginDto LoginDto)
        {
            var Result = await UserService.DeactivateUserAsync(LoginDto.Email, LoginDto.Password);
            if (Result.Succeeded)
            {
                return Ok(ResponseObjectFactory.CreateResponseObject(true, "Your Account is Deactivated successfully", new List<string>()));
            }
            string Errors = string.Empty;
            foreach (var Error in Result.Errors)
            {
                Errors += Error.Description.Substring(0, Error.Description.Length - 1) + ", ";
            }
            return BadRequest(ResponseObjectFactory.CreateResponseObject(false, Errors.Substring(0, Errors.Length - 2), new List<string>()));
        }

        [HttpPost]
        [Route("Delete")]
        public async Task<IActionResult> DeleteUserAsync(LoginDto LoginDto)
        {
            var Result = await UserService.DeleteUserAsync(LoginDto.Email, LoginDto.Password);
            if (Result.Succeeded)
            {
                return Ok(ResponseObjectFactory.CreateResponseObject(true, "Your Account is Deleted successfully", new List<string>()));
            }
            string Errors = string.Empty;
            foreach (var Error in Result.Errors)
            {
                Errors += Error.Description.Substring(0, Error.Description.Length - 1) + ", ";
            }
            return BadRequest(ResponseObjectFactory.CreateResponseObject(false, Errors.Substring(0, Errors.Length - 2), new List<string>()));
        }

        [HttpPost]
        [Route("Block")]
        public async Task<IActionResult> BlockUserAsync(string Email, bool Blocking)
        {
            var Result = await UserService.BlockUserAsync(Email, Blocking);
            if (Result.Succeeded)
            {
                if (Blocking)
                {

                    return Ok(ResponseObjectFactory.CreateResponseObject(true, "This email is blocked successfully", new List<string>()));
                }
                else
                {
                    return Ok(ResponseObjectFactory.CreateResponseObject(true, "This email is unblocked successfully", new List<string>()));
                }
            }
            string Errors = string.Empty;
            foreach (var Error in Result.Errors)
            {
                Errors += Error.Description.Substring(0, Error.Description.Length - 1) + ", ";
            }
            return BadRequest(ResponseObjectFactory.CreateResponseObject(false, Errors.Substring(0, Errors.Length - 2), new List<string>()));
        }
    }
}