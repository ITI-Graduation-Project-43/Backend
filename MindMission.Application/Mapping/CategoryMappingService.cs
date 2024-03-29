﻿using MindMission.Application.DTOs;
using MindMission.Application.Mapping.Base;
using MindMission.Domain.Enums;
using MindMission.Domain.Models;

namespace MindMission.Application.Mapping
{
    public class CategoryMappingService : IMappingService<Category, CategoryDto>
    {
        public CategoryMappingService()
        {
        }

        public async Task<CategoryDto> MapEntityToDto(Category category)
        {
            var categoryDTO = new CategoryDto
            {
                Id = category.Id,
                Name = category.Name,
                Type = category.Type,
                Approved = category.Approved,
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

        public Category MapDtoToEntity(CategoryDto categoryDTO)
        {
            return new Category
            {
                Id = categoryDTO.Id,
                Name = categoryDTO.Name,
                Type = categoryDTO.Type,
                ParentId = categoryDTO.ParentSubCategoryId ?? categoryDTO.ParentCategoryId,
                Approved = categoryDTO.Approved,
                UpdatedAt = categoryDTO.UpdatedAt
            };
        }
    }
}