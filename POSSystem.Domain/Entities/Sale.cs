namespace POSSystem.Domain.Entities
{
    public class Sale
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public decimal Total { get; set; }

        public string UserId { get; set; } = null!;
        public int CashBoxId { get; set; }

        public ApplicationUser User { get; set; } = null!;
        public ICollection<SaleDetail> SaleDetails { get; set; } = new List<SaleDetail>();
        public CashBox CashBox { get; set; } = null!;
    }
}