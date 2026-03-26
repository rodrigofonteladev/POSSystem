using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using POSSystem.Application.DTOs.Products;
using POSSystem.Application.Interfaces;
using POSSystem.Domain.Entities;

namespace POSSystem.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(string? name, string? orderBy)
        {
            var products = await _unitOfWork.ProductRepository.GetAllAsync(
                filter: p => (string.IsNullOrEmpty(name) || p.Name.Contains(name, StringComparison.OrdinalIgnoreCase)),
                orderBy: q => orderBy == "desc" ? q.OrderByDescending(p => p.Name) : q.OrderBy(p => p.Name),
                includes: q => q.Include(p => p.Category)
            );

            var productsDto = products.Adapt<IEnumerable<ProductDto>>();
            return Ok(productsDto);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var product = await _unitOfWork.ProductRepository.GetByIdAsync(id);
            if (product == null) return NotFound(new { message = "Product not found" });
            return Ok(product);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Create(CreateProductDto dto)
        {
            var product = dto.Adapt<Product>();
            _unitOfWork.ProductRepository.Create(product);

            var result = await _unitOfWork.CompleteAsync();
            if (result <= 0) return BadRequest(new { message = "Error saving the product" });

            return Ok(new { message = "Product created successfully" });
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, UpdateProductDto dto)
        {
            var product = await _unitOfWork.ProductRepository.GetByIdAsync(id);
            if (product == null) return NotFound(new { message = "Product not found" });

            dto.Adapt(product);

            _unitOfWork.ProductRepository.Update(product);
            await _unitOfWork.CompleteAsync();

            return Ok(new { message = "Product updated successfully" });
        }
    }
}