namespace POSSystem.Application.DTOs.Sales
{
    public class CreateSaleResponseDto
    {
        public bool IsSuccess { get; set; } = false;
        public string? Message { get; set; }
    }
}