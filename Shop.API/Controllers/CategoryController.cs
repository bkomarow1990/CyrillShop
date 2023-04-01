﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shop.Application.DTO.Category;
using Shop.Application.Interfaces;

namespace Shop.API.Controllers
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

        [HttpGet("get-all")]
        public async Task<ActionResult<List<CategoryDto>>> GetAll()
        {
            return Ok(await _categoryService.GetAllAsync());
        }

        [HttpGet("get-all-categories-with-subcategories")]
        public async Task<ActionResult<List<CategoryWithSubCategoriesDto>>> GetAllCategoriesWithSubCategories()
        {
            return Ok(await _categoryService.GetAllWithSubCategoriesAsync());
        }

        [HttpDelete("delete-category")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Delete(Guid categoryId)
        {
            await _categoryService.DeleteCategory(categoryId);
            return Ok();
        }
        [HttpPost("create-category")]
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult<Guid?>> CreateCategory(AddCategoryDto categoryDto)
        {
            return Ok(await _categoryService.CreateCategory(categoryDto));
        }
    }
}
