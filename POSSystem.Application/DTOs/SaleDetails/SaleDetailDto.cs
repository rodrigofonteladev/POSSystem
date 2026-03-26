namespace POSSystem.Application.DTOs.SaleDetails
{
    public class SaleDetailDto
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; } = null!;
    }
}