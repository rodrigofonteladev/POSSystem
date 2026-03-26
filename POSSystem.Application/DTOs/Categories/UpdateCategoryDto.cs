using System.ComponentModel.DataAnnotations;

namespace POSSystem.Application.DTOs.Categories
{
    public class UpdateCategoryDto
    {
        [Required(ErrorMessage = "The category name is required")]
        public string Name { get; set; } = null!;
    }
}