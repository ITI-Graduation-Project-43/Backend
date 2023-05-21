using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using MindMission.API.Utilities;
using MindMission.Application.DTO;
using MindMission.Application.Service_Interfaces;
using MindMission.Domain.Enums;
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



        #region Get
        // GET: api/Category
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var categories = await _categoryService.GetAllAsync();
            var categoryTasks = categories.Select(category => MapCategoryToDTO(category));
            var categoryDTOs = await Task.WhenAll(categoryTasks);

            var response = new ResponseObject<CategoryDTO>
            {
                Success = true,
                Message = "All Categories",
                Items = categoryDTOs.ToList(),
                PageNumber = 1,
                ItemsPerPage = 10,
                TotalPages = categoryDTOs.Length
            };

            return Ok(response);
        }


        // GET: api/Category/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var category = await _categoryService.GetByIdAsync(id);

            if (category == null)
            {
                return NotFound();
            }

            var categoryDTO = await MapCategoryToDTO(category);

            var response = new ResponseObject<CategoryDTO>
            {
                Success = true,
                Message = "Category found",
                Items = new List<CategoryDTO> { categoryDTO },
                PageNumber = 1,
                ItemsPerPage = 1,
                TotalPages = 1
            };

            return Ok(response);
        }


        // GET: api/Category/Type/{type}

        [HttpGet("Type/{type}")]
        public async Task<IActionResult> GetByType(CategoryType type)
        {
            var categories = await _categoryService.GetByTypeAsync(type);

            ResponseObject<Category> categoriesByType = new();
            categoriesByType.ReturnedResponse(true, $"All Categories of Type: {type}", categories, 3, 10, categories.Count());

            return Ok(categoriesByType);
        }

        // GET: api/Category/Parent/{parentId}

        [HttpGet("Parent/{parentId}")]
        public async Task<IActionResult> GetByParentId(int parentId)
        {
            var categories = await _categoryService.GetByParentIdAsync(parentId);

            ResponseObject<Category> categoriesByParentId = new();
            categoriesByParentId.ReturnedResponse(true, $"All Categories with ParentId: {parentId}", categories, 3, 10, categories.Count());

            return Ok(categoriesByParentId);
        }
        #endregion

        #region Add 


        // POST: api/Category
        [HttpPost]
        public async Task<ActionResult<CategoryDTO>> AddCategoryGeneral([FromBody] CategoryDTO categoryDTO)
        {
            if (categoryDTO.Type == CategoryType.Category)
            {
                return await AddCategoryAsync(categoryDTO);
            }
            else if (categoryDTO.Type == CategoryType.SubCategory)
            {
                // Validate ParentCategoryId
                var parentCategory = await _categoryService.GetByIdAsync(categoryDTO.ParentCategoryId.Value);
                if (parentCategory == null || parentCategory.Type != CategoryType.Category)
                {
                    return BadRequest("Invalid parent category.");
                }

                return await AddCategoryAsync(categoryDTO);
            }
            else if (categoryDTO.Type == CategoryType.Topic)
            {
                // Validate ParentSubCategoryId
                var parentSubCategory = await _categoryService.GetByIdAsync(categoryDTO.ParentSubCategoryId.Value);
                if (parentSubCategory == null || parentSubCategory.Type != CategoryType.SubCategory)
                {
                    return BadRequest("Invalid parent subcategory.");
                }

                return await AddCategoryAsync(categoryDTO);
            }
            else
            {
                return BadRequest("Invalid category type.");
            }
        }

        // POST: api/Category
        [HttpPost("Category")]
        public async Task<ActionResult<CategoryDTO>> AddCategory([FromBody] CategoryDTO categoryDTO)
        {
            if (categoryDTO.Type != CategoryType.Category)
            {
                return BadRequest("Invalid category type.");
            }

            return await AddCategoryAsync(categoryDTO);
        }

        // POST: api/SubCategory
        [HttpPost("SubCategory")]
        public async Task<ActionResult<CategoryDTO>> AddSubCategory([FromBody] CategoryDTO categoryDTO)
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

            return await AddCategoryAsync(categoryDTO);
        }

        // POST: api/Topic
        [HttpPost("Topic")]
        public async Task<ActionResult<CategoryDTO>> AddTopic([FromBody] CategoryDTO categoryDTO)
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

            return await AddCategoryAsync(categoryDTO);
        }



        private async Task<CategoryDTO> AddCategoryAsync(CategoryDTO categoryDTO)
        {
            var category = new Category
            {
                Name = categoryDTO.Name,
                Type = categoryDTO.Type,
                Approved = categoryDTO.Approved,
                CreatedAt = categoryDTO.CreatedAt,
                UpdatedAt = categoryDTO.UpdatedAt,
                ParentId = categoryDTO.Type switch
                {
                    CategoryType.SubCategory => categoryDTO.ParentCategoryId,
                    CategoryType.Topic => categoryDTO.ParentSubCategoryId,
                    _ => null
                }
            };

            var createdCategory = await _categoryService.AddAsync(category);

            var createdCategoryDTO = await MapCategoryToDTO(createdCategory);

            return createdCategoryDTO;
        }

        #endregion

        #region Edit Patch/Put 

        // PUT: api/Category/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(int id, [FromBody] CategoryDTO categoryDTO)
        {
            if (id != categoryDTO.Id)
            {
                return BadRequest();
            }

            var category = await _categoryService.GetByIdAsync(id);

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

        // PATCH: api/Category/{id}
        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchCategory(int id, [FromBody] JsonPatchDocument<CategoryDTO> patchDocument)
        {
            var category = await _categoryService.GetByIdAsync(id);

            if (category == null)
            {
                return NotFound();
            }

            var categoryDTO = await MapCategoryToDTO(category);

            patchDocument.ApplyTo(categoryDTO, error =>
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

        #region Delete
        // DELETE: api/Category/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var category = await _categoryService.GetByIdAsync(id);

            if (category == null)
            {
                return NotFound();
            }

            await _categoryService.DeleteAsync(id);

            return NoContent();
        }

        #endregion


        #region Mapper
        private async Task<CategoryDTO> MapCategoryToDTO(Category category)
        {
            var categoryDTO = new CategoryDTO
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

            if (category.ParentId.HasValue)
            {
                var parentCategory = await _categoryService.GetByIdAsync(category.ParentId.Value);

                if (parentCategory != null)
                {
                    if (parentCategory.Type == CategoryType.Category)
                    {
                        categoryDTO.ParentCategoryId = parentCategory.Id;
                        categoryDTO.ParentCategoryName = parentCategory.Name;
                    }
                    else if (parentCategory.Type == CategoryType.SubCategory)
                    {
                        var parentSubCategory = await _categoryService.GetByIdAsync(parentCategory.ParentId.Value);
                        categoryDTO.ParentCategoryId = parentSubCategory.Id;
                        categoryDTO.ParentSubCategoryId = parentCategory.Id;
                        categoryDTO.ParentCategoryName = parentSubCategory.Name;
                        categoryDTO.ParentSubCategoryName = parentCategory.Name;
                    }
                }
            }


            return categoryDTO;
        }


        #endregion

    }
}
