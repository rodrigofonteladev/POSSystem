using POSSystem.Domain.Enums;

namespace POSSystem.Application.DTOs.CashBoxes
{
    public class OpenCashBoxDto
    {
        public decimal InitialAmount { get; set; }
        public CashBoxStatus Status { get; set; } = CashBoxStatus.Open;
    }
}