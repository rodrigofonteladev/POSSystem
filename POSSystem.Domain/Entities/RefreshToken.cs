namespace POSSystem.Domain.Entities
{
    public class RefreshToken
    {
        public int Id { get; set; }
        public string Token { get; set; } = null!;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime Expires { get; set; }
        public DateTime? Revoke { get; set; }
        public string? RevokeIpAddress { get; set; }
        public string? ReplacedByToken { get; set; }
        public bool IsExpires => DateTime.UtcNow >= Expires;
        public bool IsActive => Revoke == null && !IsExpires;

        public string UserId { get; set; } = null!;
        public ApplicationUser User { get; set; } = null!;

    }
}