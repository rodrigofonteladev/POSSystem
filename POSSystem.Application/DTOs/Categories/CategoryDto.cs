using POSSystem.Application.DTOs.Products;

namespace POSSystem.Application.DTOs.Categories
{
    public class CategoryDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public ICollection<ProductDto> Products { get; set; } = new List<ProductDto>();
    }
}