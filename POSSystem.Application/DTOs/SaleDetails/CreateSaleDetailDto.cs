namespace POSSystem.Application.DTOs.SaleDetails
{
    public class CreateSaleDetailDto
    {
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public int ProductId { get; set; }
    }
}