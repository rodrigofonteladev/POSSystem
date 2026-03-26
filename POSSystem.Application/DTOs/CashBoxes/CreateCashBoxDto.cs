using POSSystem.Domain.Enums;

namespace POSSystem.Application.DTOs.CashBoxes
{
    public class CreateCashBoxDto
    {
        public string UserId { get; set; } = null!;
        public DateTime StartTime { get; set; }
        public decimal InitialAmount { get; set; }
        public CashBoxStatus Status { get; set; } = CashBoxStatus.Open;
    }
}