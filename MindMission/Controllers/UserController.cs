using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MindMission.API.Utilities;
using MindMission.Application.DTO_Classes;
using MindMission.Application.Service_Interfaces;
using MindMission.Application.Services;
using MindMission.Application.Services_Classes;
using MindMission.Domain.Models;

namespace MindMission.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService UserService;

        public UserController(IUserService _UserService) 
        {
            UserService = _UserService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers() 
        {
            var Users = await UserService.GetAllAsync();

            ResponseObject<User> AllUsers = new ResponseObject<User>();
            AllUsers.ReturnedResponse(true, "All Users", Users, 3, 10, Users.Count());

            return Ok(AllUsers);
        }

        [HttpPost]
        public async Task<IActionResult> AddNewUser(UserDTO UserDTO)
        {
            if (UserDTO != null)
            {
                User user = new User();
                user.Email = UserDTO.Email;
                user.PasswordHash = UserDTO.Password;
                user.IsBlocked = UserDTO.IsBlocked;
                user.IsDeactivated = UserDTO.IsDeactivated;
                var CreatedUser = await UserService.AddAsync(user);
                return Ok(CreatedUser);
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
