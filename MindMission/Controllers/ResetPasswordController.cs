using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MindMission.Application.Interfaces.Services;
using MindMission.Application.Services;
using MindMission.Domain.Models;

namespace MindMission.API.Controllers
{
    public class ResetPasswordController : Controller
    {
        private readonly IUserService UserService;

        public ResetPasswordController(IUserService _UserService)
        {
            UserService = _UserService;
        }

        [Route("ResetPassword")]
        [HttpGet]
        public async Task<IActionResult> ResetPasswordPage([FromQuery] string Email, [FromQuery] string Token)
        {
            ViewBag.Email = Email;
            ViewBag.Token = Token;
            if(await UserService.ValidateTokenAsync(Email, Token))
            {
                return View("ResetPassword");
            }
            return Content("Not Found");
        }
    }
}
