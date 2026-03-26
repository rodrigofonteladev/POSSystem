using POSSystem.Application.DTOs.SaleDetails;

namespace POSSystem.Application.DTOs.Sales
{
    public class CreateSaleDto
    {
        public decimal Total { get; set; }
        public ICollection<CreateSaleDetailDto> Details { get; set; } = new List<CreateSaleDetailDto>();
    }
}