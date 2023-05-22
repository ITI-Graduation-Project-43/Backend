using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        private readonly UserManager<User> UserManager;

        public UserController(UserManager<User> _UserManager) 
        {
            UserManager = _UserManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var Users = UserManager.Users.ToListAsync();

            ResponseObject<User> AllUsers = new ResponseObject<User>();
            AllUsers.ReturnedResponse(true, "All Users", Users.Result, 3, 10, Users.Result.Count());

            return Ok(AllUsers);
        }

        [HttpPost]
        public async Task<IActionResult> AddNewUser(UserDTO UserDTO)
        {
            if (UserDTO != null)
            {
                User user = new User();
                user.UserName = UserDTO.UserName;
                user.Email = UserDTO.Email;
                user.PasswordHash = UserDTO.Password;
                user.IsBlocked = UserDTO.IsBlocked;
                user.IsDeactivated = UserDTO.IsDeactivated;
                //var CreatedUser = await UserService.AddAsync(user);
                var CreatedUser = await UserManager.CreateAsync(user);
                return Ok(CreatedUser);
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
