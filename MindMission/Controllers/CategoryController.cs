using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using MindMission.API.Controllers.Base;
using MindMission.Application.DTOs;
using MindMission.Application.Mapping;
using MindMission.Application.Service_Interfaces;
using MindMission.Domain.Enums;
using MindMission.Domain.Models;

namespace MindMission.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : BaseController<Category, CategoryDto, int>
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
            return await GetEntitiesResponseWithIncludePagination(
                _categoryService.GetAllAsync,
                _categoryService.GetTotalCountAsync,
                pagination,
                "Categories",
                category => category.Parent,
                category => category.Parent.Parent
            );
        }

        // GET: api/Category/{categoryId}
        [HttpGet("{Id}")]
        public async Task<ActionResult<CategoryDto>> GetCategoryById(int Id)
        {
            return await GetEntityResponseWithInclude(
                    () => _categoryService.GetByIdAsync(Id,
                        category => category.Parent,
                        category => category.Parent.Parent
                    ),
                    "Category"
                );
        }

        // GET: api/Category/Type/{type}

        [HttpGet("Type/{type}")]
        public async Task<ActionResult<IEnumerable<CategoryDto>>> GetCategoriesByType(CategoryType type, [FromQuery] PaginationDto pagination)
        {
            return await GetEntitiesResponsePagination(() => _categoryService.GetByTypeAsync(type, pagination.PageNumber, pagination.PageSize), _categoryService.GetTotalCountAsync, pagination, "Categories");
        }

        // GET: api/Category/Parent/{parentId}

        [HttpGet("ParentSubCategories/{parentId}")]
        public async Task<ActionResult<IEnumerable<CategoryDto>>> GetCategoriesByParentId(int parentId, [FromQuery] PaginationDto pagination)
        {
            return await GetEntitiesResponsePagination(() => _categoryService.GetByParentIdAsync(parentId, pagination.PageNumber, pagination.PageSize), _categoryService.GetTotalCountAsync, pagination, "Categories");
        }


        // GET: api/Category/Parent/{parentId}

        [HttpGet("Parent/{parentId}")]
        public async Task<ActionResult<CategoryDto>> GetParentCategoryById(int parentId, [FromQuery] PaginationDto pagination)
        {
            return await GetEntityResponse(() => _categoryService.GetParentCategoryById(parentId), "Categories");
        }

        #endregion Get

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
        [HttpPost(template: "Topic")]
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

        #endregion Add

        #region Delete

        // DELETE: api/Category/Delete/{categoryId}
        [HttpDelete("Delete/{categoryId}")]
        public async Task<IActionResult> DeleteCategory(int categoryId)
        {
            return await DeleteEntityResponse(_categoryService.GetByIdAsync, _categoryService.DeleteAsync, categoryId);
        }

        // DELETE: api/Category/{id}

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {

            var article = await _categoryService.GetByIdAsync(id);

            if (article == null)
                return NotFound(NotFoundResponse("Article"));
            await _categoryService.SoftDeleteAsync(id);
            return NoContent();
        }
        #endregion Delete

        #region Edit Patch/Put

        // PUT: api/Category/{categoryId}
        [HttpPut("{Id}")]
        public async Task<IActionResult> UpdateCategory(int Id, [FromBody] CategoryDto categoryDTO)
        {
            if (!IsValidCategoryType(categoryDTO.Type))
            {
                return BadRequest("Invalid category type.");
            }

            if (categoryDTO.Type == CategoryType.SubCategory && !await IsValidParentCategoryIdAsync(categoryDTO.ParentCategoryId))
            {
                return BadRequest("Invalid parent category.");
            }

            if (categoryDTO.Type == CategoryType.Topic && !await IsValidParentSubCategoryIdAsync(categoryDTO.ParentSubCategoryId))
            {
                return BadRequest("Invalid parent subcategory.");
            }
            categoryDTO.UpdatedAt = DateTime.Now;
            return await UpdateEntityResponse(_categoryService.GetByIdAsync, _categoryService.UpdateAsync, Id, categoryDTO, "Category");
        }

        // PATCH: api/Category/{categoryId}
        [HttpPatch("{categoryId}")]
        public async Task<IActionResult> PatchCategory(int categoryId, [FromBody] JsonPatchDocument<CategoryDto> patchDocument)
        {
            if (patchDocument == null)
            {
                return BadRequest();
            }

            return await PatchEntityResponse(_categoryService.GetByIdAsync, _categoryService.UpdateAsync, categoryId, "Category", patchDocument, (entity, dto) =>
            {
                entity = _categoryMappingService.MapDtoToEntity(dto);

                if (dto.Type != entity.Type)
                {
                    throw new ArgumentException("Invalid category type.");
                }
                if (dto.Type == CategoryType.SubCategory && dto.ParentCategoryId != entity.ParentId)
                {
                    throw new ArgumentException("Invalid parent category.");
                }
                if (dto.Type == CategoryType.Topic && dto.ParentSubCategoryId != entity.ParentId)
                {
                    throw new ArgumentException("Invalid parent subcategory.");
                }
                entity.Name = dto.Name;
                entity.Approved = dto.Approved;
                entity.UpdatedAt = DateTime.Now;
            });
        }

        #endregion Edit Patch/Put

        #region Helper Methods

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

        #endregion Helper Methods
    }
}