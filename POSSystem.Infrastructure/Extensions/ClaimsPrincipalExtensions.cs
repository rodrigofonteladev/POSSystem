using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;

namespace POSSystem.Infrastructure.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static string GetUserId(this ClaimsPrincipal user)
        {
            var userId = user.FindFirst(JwtRegisteredClaimNames.Sub)?.Value
                 ?? user.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            return userId ?? throw new Exception("User ID not found in token");
        }
    }
}