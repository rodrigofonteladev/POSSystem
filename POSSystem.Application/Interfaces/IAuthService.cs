using POSSystem.Application.DTOs.Auth;

namespace POSSystem.Application.Interfaces
{
    public interface IAuthService
    {
        Task<AuthResponseDto> LoginAsync(LoginRequestDto dto);
        Task<ResponseDto> RegisterAsync(RegisterRequestDto dto);
        Task<AuthResponseDto> RefreshTokenAsync(RefreshTokenRequestDto dto, string ipAddress);
    }
}