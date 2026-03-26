using Mapster;
using POSSystem.Application.DTOs.Products;
using POSSystem.Domain.Entities;

namespace POSSystem.Application.Mappings
{
    public class MapsterConfig
    {
        public static void RegisterMappings()
        {
            TypeAdapterConfig<Product, ProductDto>
                .NewConfig()
                .Map(dest => dest.CategoryName, src => src.Category.Name);
        }
    }
}