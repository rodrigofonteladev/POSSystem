using POSSystem.Application.DTOs.CashBoxes;

namespace POSSystem.Application.Interfaces
{
    public interface ICashBoxService
    {
        Task<CashBoxResponseDto> OpenAsync(OpenCashBoxDto dto, string userId);
        Task<CashBoxResponseDto> CloseAsync(CloseCashBoxDto dto, string userId);
    }
}