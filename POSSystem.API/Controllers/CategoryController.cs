using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using POSSystem.Application.DTOs.Categories;
using POSSystem.Application.Interfaces;
using POSSystem.Domain.Entities;

namespace POSSystem.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(string? name, string? orderBy)
        {
            var categories = await _unitOfWork.CategoryRepository.GetAllAsync(
                filter: c => (string.IsNullOrEmpty(name) || c.Name.Contains(name, StringComparison.OrdinalIgnoreCase)),
                orderBy: q => orderBy == "desc" ? q.OrderByDescending(c => c.Name) : q.OrderBy(c => c.Name),
                includes: q => q.Include(c => c.Products)
            );

            var categoriesDto = categories.Adapt<IEnumerable<CategoryDto>>();
            return Ok(categoriesDto);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var category = await _unitOfWork.CategoryRepository.GetByIdAsync(id);
            if (category == null) return NotFound(new { message = "Category not found" });
            return Ok(category);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Create(CreateCategoryDto dto)
        {
            var category = dto.Adapt<Category>();
            _unitOfWork.CategoryRepository.Create(category);

            var result = await _unitOfWork.CompleteAsync();
            if (result <= 0) return BadRequest(new { message = "Error saving the category" });

            return Ok(new { message = "Category created successfully" });
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, UpdateCategoryDto dto)
        {
            var category = await _unitOfWork.CategoryRepository.GetByIdAsync(id);
            if (category == null) return NotFound(new { message = "Category not found" });

            dto.Adapt(category);

            _unitOfWork.CategoryRepository.Update(category);
            await _unitOfWork.CompleteAsync();

            return Ok(new { message = "Category updated successfully" });
        }
    }
}