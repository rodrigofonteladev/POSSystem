using System.Security.Claims;
using POSSystem.Application.DTOs.Auth;
using POSSystem.Domain.Entities;

namespace POSSystem.Application.Interfaces
{
    public interface ITokenService
    {
        AccessTokenDto GenerateAccessToken(ApplicationUser user, IEnumerable<string> roles);
        RefreshTokenDto GenerateRefreshToken();
        ClaimsPrincipal? GetPrincipalFromExpiredToken(string token);
    }
}