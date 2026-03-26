namespace POSSystem.Application.DTOs.Sales
{
    public class SaleDto
    {
        public int Id { get; set; }
        public DateTime SaleDate { get; set; } = DateTime.UtcNow;
        public decimal Total { get; set; }
        public string UserName { get; set; } = null!;
    }
}