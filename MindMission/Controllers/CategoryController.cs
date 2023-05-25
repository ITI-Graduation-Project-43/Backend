using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using MindMission.API.Controllers.Base;
using MindMission.API.Utilities;
using MindMission.Application.DTOs;
using MindMission.Application.Mapping;
using MindMission.Application.Service_Interfaces;
using MindMission.Application.Services;
using MindMission.Domain.Enums;
using MindMission.Domain.Models;

namespace MindMission.API.Controllers
{
    // TODO: Refactor code and add Try catch block, comments
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : BaseController<Category, CategoryDto,int>
    {
        private readonly ICategoryService _categoryService;
        private readonly CategoryMappingService _categoryMappingService;


        public CategoryController(ICategoryService categoryService, CategoryMappingService categoryMappingService) : base(categoryMappingService)
        {
            _categoryService = categoryService ?? throw new ArgumentNullException(nameof(categoryService));
            _categoryMappingService = categoryMappingService ?? throw new ArgumentNullException(nameof(categoryMappingService));
        }




        #region Get
        // GET: api/Category
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryDto>>> GetAllCategories([FromQuery] PaginationDto pagination)
        {
            return await GetEntitiesResponse(_categoryService.GetAllAsync, pagination, "Categories");
        }


        // GET: api/Category/{categoryId}
        [HttpGet("{Id}")]
        public async Task<ActionResult<CategoryDto>> GetCategoryById(int Id)
        {
            return await GetEntityResponse(() => _categoryService.GetByIdAsync(Id), "Category");
        }


        // GET: api/Category/Type/{type}

        [HttpGet("Type/{type}")]
        public async Task<ActionResult<IEnumerable<CategoryDto>>> GetCategoriesByType(CategoryType type, [FromQuery] PaginationDto pagination)
        {
            return await GetEntitiesResponse(() => _categoryService.GetByTypeAsync(type), pagination, "Categories");
        }


        // GET: api/Category/Parent/{parentId}

        [HttpGet("Parent/{parentId}")]
        public async Task<ActionResult<IEnumerable<CategoryDto>>> GetCategoriesByParentId(int parentId, [FromQuery] PaginationDto pagination)
        {
            return await GetEntitiesResponse(() => _categoryService.GetByParentIdAsync(parentId), pagination, "Categories");
        }
        #endregion

        #region Add 



        // POST: api/Category
        [HttpPost]
        public async Task<ActionResult<CategoryDto>> AddCategory([FromBody] CategoryDto categoryDTO)
        {
            if (!IsValidCategoryType(categoryDTO.Type))
            {
                return BadRequest("Invalid category type.");
            }

            if (categoryDTO.Type == CategoryType.SubCategory)
            {
                if (!await IsValidParentCategoryIdAsync(categoryDTO.ParentCategoryId))
                {
                    return BadRequest("Invalid parent category.");
                }
            }
            else if (categoryDTO.Type == CategoryType.Topic && !await IsValidParentSubCategoryIdAsync(categoryDTO.ParentSubCategoryId))
            {
                return BadRequest("Invalid parent subcategory.");
            }
            return await AddEntityResponse(_categoryService.AddAsync, categoryDTO, "Category", nameof(GetCategoryById));
        }



        // POST: api/Category
        [HttpPost("Category")]
        public async Task<ActionResult<CategoryDto>> AddCategoryType([FromBody] CategoryDto categoryDTO)
        {
            if (categoryDTO.Type != CategoryType.Category)
            {
                return BadRequest("Invalid category type.");
            }

            return await AddEntityResponse(_categoryService.AddAsync, categoryDTO, "Category", nameof(GetCategoryById));
        }

        // POST: api/SubCategory
        [HttpPost("SubCategory")]
        public async Task<ActionResult<CategoryDto>> AddSubCategory([FromBody] CategoryDto categoryDTO)
        {
            if (categoryDTO.Type != CategoryType.SubCategory)
            {
                return BadRequest("Invalid category type.");
            }

            // Validate ParentCategoryId
            var parentCategory = await _categoryService.GetByIdAsync(categoryDTO.ParentCategoryId.Value);
            if (parentCategory == null || parentCategory.Type != CategoryType.Category)
            {
                return BadRequest("Invalid parent category.");
            }

            return await AddEntityResponse(_categoryService.AddAsync, categoryDTO, "Category", nameof(GetCategoryById));
        }

        // POST: api/Topic
        [HttpPost("Topic")]
        public async Task<ActionResult<CategoryDto>> AddTopic([FromBody] CategoryDto categoryDTO)
        {
            if (categoryDTO.Type != CategoryType.Topic)
            {
                return BadRequest("Invalid category type.");
            }

            // Validate ParentSubCategoryId
            var parentSubCategory = await _categoryService.GetByIdAsync(categoryDTO.ParentSubCategoryId.Value);
            if (parentSubCategory == null || parentSubCategory.Type != CategoryType.SubCategory)
            {
                return BadRequest("Invalid parent subcategory.");
            }

            return await AddEntityResponse(_categoryService.AddAsync, categoryDTO, "Category", nameof(GetCategoryById));
        }
        #endregion

        #region Delete
        // DELETE: api/Category/{categoryId}
        [HttpDelete("{categoryId}")]
        public async Task<IActionResult> DeleteCategory(int categoryId)
        {
            return await DeleteEntityResponse(_categoryService.GetByIdAsync, _categoryService.DeleteAsync, categoryId);
        }
        #endregion

        #region Edit Patch/Put 

        // PUT: api/Category/{categoryId}
        [HttpPut("{categoryId}")]
        public async Task<IActionResult> UpdateCategory(int categoryId, [FromBody] CategoryDto categoryDTO)
        {
            if (categoryId != categoryDTO.Id)
            {
                return BadRequest();
            }

            var category = await _categoryService.GetByIdAsync(categoryId);

            if (category == null)
            {
                return NotFound();
            }

            if (categoryDTO.Type != category.Type)
            {
                return BadRequest("Invalid category type.");
            }

            if (categoryDTO.Type == CategoryType.SubCategory && categoryDTO.ParentCategoryId != category.ParentId)
            {
                return BadRequest("Invalid parent category.");
            }

            if (categoryDTO.Type == CategoryType.Topic && categoryDTO.ParentSubCategoryId != category.ParentId)
            {
                return BadRequest("Invalid parent subcategory.");
            }

            category.Name = categoryDTO.Name;
            category.Approved = categoryDTO.Approved;
            category.UpdatedAt = DateTime.Now;

            await _categoryService.UpdateAsync(category);

            return NoContent();
        }


        // PUT: api/Category/{categoryId}
        [HttpPut("hh/{categoryId}")]
        public async Task<IActionResult> UpdateCategory2(int categoryId, [FromBody] CategoryDto categoryDTO)
        {
            return await UpdateEntityResponse(_categoryService.GetByIdAsync, _categoryService.UpdateAsync, categoryId, categoryDTO, "Category");

        }

        // PATCH: api/Category/{categoryId}
        [HttpPatch("{categoryId}")]
        public async Task<IActionResult> PatchCategory(int categoryId, [FromBody] JsonPatchDocument<CategoryDto> patchDocument)
        {
            if (patchDocument == null)
            {
                return BadRequest();
            }
            var category = await _categoryService.GetByIdAsync(categoryId);

            if (category == null)
            {
                return NotFound();
            }

            var categoryDTO = await MapCategoryToDTO(category);

            patchDocument.ApplyTo(
                categoryDTO,
                error =>
                {
                    ModelState.AddModelError("JsonPatch", error.ErrorMessage);
                });


            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (categoryDTO.Type != category.Type)
            {
                return BadRequest("Invalid category type.");
            }

            if (categoryDTO.Type == CategoryType.SubCategory && categoryDTO.ParentCategoryId != category.ParentId)
            {
                return BadRequest("Invalid parent category.");
            }

            if (categoryDTO.Type == CategoryType.Topic && categoryDTO.ParentSubCategoryId != category.ParentId)
            {
                return BadRequest("Invalid parent subcategory.");
            }

            category.Name = categoryDTO.Name;
            category.Approved = categoryDTO.Approved;
            category.UpdatedAt = DateTime.Now;

            await _categoryService.UpdateAsync(category);

            return NoContent();
        }
        #endregion

        #region Helper Methods

        private async Task<CategoryDto> MapCategoryToDTO(Category category)
        {
            var categoryDTO = new CategoryDto
            {
                Id = category.Id,
                Name = category.Name,
                Type = category.Type,
                Approved = category.Approved,
                CreatedAt = category.CreatedAt,
                UpdatedAt = category.UpdatedAt,
                ParentCategoryId = null,
                ParentSubCategoryId = null,
                ParentCategoryName = null,
                ParentSubCategoryName = null
            };

            if (category.Parent != null)
            {
                if (category.Parent.Type == CategoryType.Category)
                {
                    categoryDTO.ParentCategoryId = category.Parent.Id;
                    categoryDTO.ParentCategoryName = category.Parent.Name;
                }
                else if (category.Parent.Type == CategoryType.SubCategory && category.Parent.Parent != null)
                {
                    var parentSubCategory = category.Parent.Parent;
                    categoryDTO.ParentCategoryId = parentSubCategory.Id;
                    categoryDTO.ParentSubCategoryId = category.Parent.Id;
                    categoryDTO.ParentCategoryName = parentSubCategory.Name;
                    categoryDTO.ParentSubCategoryName = category.Parent.Name;
                }
            }

            return categoryDTO;

        }

        private static int? GetParentId(CategoryDto categoryDTO)
        {
            if (categoryDTO.Type == CategoryType.SubCategory)
            {
                return categoryDTO.ParentCategoryId;
            }
            else if (categoryDTO.Type == CategoryType.Topic)
            {
                return categoryDTO.ParentSubCategoryId;
            }

            return null;
        }

        private static bool IsValidCategoryType(CategoryType type)
        { return type == CategoryType.Category || type == CategoryType.SubCategory || type == CategoryType.Topic; }

        private async Task<bool> IsValidParentCategoryIdAsync(int? parentId)
        {
            if (parentId.HasValue)
            {
                var parentCategory = await _categoryService.GetByIdAsync(parentId.Value);
                return parentCategory != null && parentCategory.Type == CategoryType.Category;
            }

            return false;
        }

        private async Task<bool> IsValidParentSubCategoryIdAsync(int? parentSubCategoryId)
        {
            if (parentSubCategoryId.HasValue)
            {
                var parentSubCategory = await _categoryService.GetByIdAsync(parentSubCategoryId.Value);
                return parentSubCategory != null && parentSubCategory.Type == CategoryType.SubCategory;
            }

            return false;
        }
        #endregion
    }
}
