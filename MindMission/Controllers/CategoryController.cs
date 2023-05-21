﻿using Microsoft.AspNetCore.JsonPatch;
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
        public async Task<IActionResult> GetAllCategories()
        {
            var categories = await _categoryService.GetAllAsync();
            var categoryDTOs = await Task.WhenAll(categories.Select(category => MapCategoryToDTO(category)));

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
        public async Task<IActionResult> GetCategoryById(int id)
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
        public async Task<IActionResult> GetCategoriesByType(CategoryType type)
        {
            var categories = await _categoryService.GetByTypeAsync(type);

            var response = new ResponseObject<Category>
            {
                Success = true,
                Message = $"All Categories of Type: {type}",
                Items = categories.ToList(),
                PageNumber = 1,
                ItemsPerPage = 10,
                TotalPages = categories.Count()
            };

            return Ok(response);
        }

        // GET: api/Category/Parent/{parentId}

        [HttpGet("Parent/{parentId}")]
        public async Task<IActionResult> GetCategoriesByParentId(int parentId)
        {
            var categories = await _categoryService.GetByParentIdAsync(parentId);

            var response = new ResponseObject<Category>
            {
                Success = true,
                Message = $"All Categories with ParentId: {parentId}",
                Items = categories.ToList(),
                PageNumber = 1,
                ItemsPerPage = 10,
                TotalPages = categories.Count()
            };

            return Ok(response);
        }
        #endregion

        #region Add 


        // POST: api/Category
        [HttpPost]
        public async Task<ActionResult<CategoryDTO>> AddCategory([FromBody] CategoryDTO categoryDTO)
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
            else if (categoryDTO.Type == CategoryType.Topic)
            {
                if (!await IsValidParentSubCategoryIdAsync(categoryDTO.ParentSubCategoryId))
                {
                    return BadRequest("Invalid parent subcategory.");
                }
            }

            var createdCategoryDTO = await AddCategoryAsync(categoryDTO);

            return CreatedAtAction(nameof(GetCategoryById), new { id = createdCategoryDTO.Id }, createdCategoryDTO);
        }


        // POST: api/Category
        [HttpPost("Category")]
        public async Task<ActionResult<CategoryDTO>> AddCategoryType([FromBody] CategoryDTO categoryDTO)
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

        #region Helper Methods

        private async Task<CategoryDTO> AddCategoryAsync(CategoryDTO categoryDTO)
        {
            var category = new Category
            {
                Name = categoryDTO.Name,
                Type = categoryDTO.Type,
                Approved = categoryDTO.Approved,
                CreatedAt = categoryDTO.CreatedAt,
                UpdatedAt = categoryDTO.UpdatedAt,
                ParentId = GetParentId(categoryDTO)
            };

            var createdCategory = await _categoryService.AddAsync(category);
            var createdCategoryDTO = await MapCategoryToDTO(createdCategory);

            return createdCategoryDTO;
        }

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

        private static int? GetParentId(CategoryDTO categoryDTO)
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
        {
            return type == CategoryType.Category || type == CategoryType.SubCategory || type == CategoryType.Topic;
        }

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
