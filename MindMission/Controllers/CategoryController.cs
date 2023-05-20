using Microsoft.AspNetCore.Mvc;
using MindMission.API.Utilities;
using MindMission.Application.Service_Interfaces;
using MindMission.Domain.Models;

namespace MindMission.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var category = await _categoryService.GetAllAsync();

            ResponseObject<Category> AllCategories = new ResponseObject<Category>();
            AllCategories.ReturnedResponse(true, "All Categories", category, 3, 10, category.Count());

            return Ok(AllCategories);
        }
    }
}
