using Microsoft.AspNetCore.Identity;

namespace POSSystem.Domain.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public bool IsActive { get; set; } = true;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public ICollection<Sale> Sales { get; set; } = new List<Sale>();
        public ICollection<RefreshToken> RefreshTokens { get; set; } = new List<RefreshToken>();
        public ICollection<CashBox> CashBoxes { get; set; } = new List<CashBox>();
    }
}