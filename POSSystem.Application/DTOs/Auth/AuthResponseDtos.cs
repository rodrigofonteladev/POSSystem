namespace POSSystem.Application.DTOs.Auth
{
    public record AccessTokenDto(string Token, DateTime Expires);
    public record RefreshTokenDto(string Token, DateTime Expires);
    public record RefreshTokenRequestDto(string AccessToken, string RefreshToken);
    public record LoginRequestDto(string Email, string Password);
    public record RegisterRequestDto(string Email, string UserName, string Password);
    public record AuthResponseDto(
        string? AccessToken,
        string? RefreshToken,
        DateTime? Expiration,
        bool IsSuccess = false,
        string? Message = null,
        bool IsLockedOut = false
    );
    public record ResponseDto(
        bool IsSuccess = false,
        string? Message = null,
        List<string>? Errors = null
    );
}