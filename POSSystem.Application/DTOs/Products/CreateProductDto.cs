using System.ComponentModel.DataAnnotations;

namespace POSSystem.Application.DTOs.Products
{
    public class CreateProductDto
    {
        [Required(ErrorMessage = "The product name is required")]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = "The product price is required")]
        [Range(0.01, 99999999.99, ErrorMessage = "The price must be greater than 0")]
        public decimal? Price { get; set; }

        [Required(ErrorMessage = "The product stock is required")]
        [Range(0, 999, ErrorMessage = "The stock cannot be negative")]
        public int? Stock { get; set; }

        [Required(ErrorMessage = "The category is required")]
        public int? CategoryId { get; set; }
    }
}