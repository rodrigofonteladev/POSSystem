using POSSystem.Domain.Enums;

namespace POSSystem.Domain.Entities
{
    public class CashBox
    {
        public int Id { get; set; }
        public DateTime StartTime { get; set; }
        public decimal InitialAmount { get; set; }

        public DateTime? EndTime { get; set; }
        public decimal? TotalSales { get; set; }
        public decimal? FinalAmount { get; set; }
        public decimal? Difference { get; set; }

        public CashBoxStatus Status { get; set; }

        public string UserId { get; set; } = null!;
        public ApplicationUser ApplicationUser { get; set; } = null!;
        public ICollection<Sale> Sales { get; set; } = new List<Sale>();
    }
}