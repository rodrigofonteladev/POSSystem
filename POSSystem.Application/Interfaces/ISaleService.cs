using POSSystem.Application.DTOs.Sales;

namespace POSSystem.Application.Interfaces
{
    public interface ISaleService
    {
        Task<CreateSaleResponseDto> CreateSaleAsync(CreateSaleDto model, string userId);
    }
}