using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Identity;
using POSSystem.Application.DTOs.Auth;
using POSSystem.Application.Interfaces;
using POSSystem.Domain.Entities;

namespace POSSystem.Infrastructure.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ITokenService _tokenService;
        private readonly IUnitOfWork _unitOfWork;

        public AuthService(
            UserManager<ApplicationUser> userManager,
            ITokenService tokenService,
            IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _unitOfWork = unitOfWork;
        }

        public async Task<AuthResponseDto> LoginAsync(LoginRequestDto dto)
        {
            var user = await _userManager.FindByEmailAsync(dto.Email);
            if (user == null)
            {
                return new AuthResponseDto(null, null, null, false, "Invalid credentials");
            }
            if (await _userManager.IsLockedOutAsync(user))
            {
                return new AuthResponseDto(null, null, null, false, "Account temporarily blocked", true);
            }
            if (!await _userManager.CheckPasswordAsync(user, dto.Password))
            {
                await _userManager.AccessFailedAsync(user);

                if (await _userManager.IsLockedOutAsync(user))
                {
                    return new AuthResponseDto(null, null, null, false, "Account temporarily blocked", true);
                }

                return new AuthResponseDto(null, null, null, false, "Invalid credentials");
            }

            await _userManager.ResetAccessFailedCountAsync(user);

            var roles = await _userManager.GetRolesAsync(user);
            var accessToken = _tokenService.GenerateAccessToken(user, roles);
            var refreshToken = _tokenService.GenerateRefreshToken();

            _unitOfWork.RefreshTokenRepository.Create(new RefreshToken
            {
                Token = refreshToken.Token,
                Expires = refreshToken.Expires,
                UserId = user.Id
            });
            await _unitOfWork.CompleteAsync();

            return new AuthResponseDto(
                accessToken.Token,
                refreshToken.Token,
                accessToken.Expires,
                true,
                "Successful login"
            );
        }

        public async Task<ResponseDto> RegisterAsync(RegisterRequestDto dto)
        {
            using var transaction = await _unitOfWork.BeginTransactionAsync();

            try
            {
                var user = new ApplicationUser
                {
                    Email = dto.Email,
                    UserName = dto.UserName
                };

                var createResult = await _userManager.CreateAsync(user, dto.Password);
                if (!createResult.Succeeded)
                {
                    return new ResponseDto(
                        false,
                        "Error creating user",
                        createResult.Errors.Select(e => e.Description).ToList()
                    );
                }

                var assignResult = await _userManager.AddToRoleAsync(user, "User");
                if (!assignResult.Succeeded)
                {
                    await transaction.RollbackAsync();
                    return new ResponseDto(
                        false,
                        "Error assigning role to user",
                        assignResult.Errors.Select(e => e.Description).ToList()
                    );
                }

                await transaction.CommitAsync();
                return new ResponseDto(true, "User created successfully");
            }
            catch
            {
                await transaction.RollbackAsync();
                return new ResponseDto(false, "An unexpected error ocurred");
            }
        }

        public async Task<AuthResponseDto> RefreshTokenAsync(RefreshTokenRequestDto dto, string ipAddress)
        {
            var claimsPrincipal = _tokenService.GetPrincipalFromExpiredToken(dto.AccessToken);
            if (claimsPrincipal == null)
                return new AuthResponseDto(null, null, null, false, "Invalid token");

            var userId = claimsPrincipal.FindFirst(JwtRegisteredClaimNames.Sub)?.Value;
            if (string.IsNullOrEmpty(userId))
                return new AuthResponseDto(null, null, null, false, "Invalid token claims");

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return new AuthResponseDto(null, null, null, false, "User not found");

            var refreshToken = await _unitOfWork.RefreshTokenRepository
                .GetAsync(rt => rt.Token == dto.RefreshToken && rt.UserId == userId);

            if (refreshToken == null || !refreshToken.IsActive)
                return new AuthResponseDto(null, null, null, false, "Invalid session. Please log in again");

            refreshToken.Revoke = DateTime.UtcNow;
            refreshToken.RevokeIpAddress = ipAddress;

            var roles = await _userManager.GetRolesAsync(user);
            var newAccessToken = _tokenService.GenerateAccessToken(user, roles);
            var newRefreshToken = _tokenService.GenerateRefreshToken();

            refreshToken.ReplacedByToken = newRefreshToken.Token;

            _unitOfWork.RefreshTokenRepository.Create(new RefreshToken
            {
                Token = newRefreshToken.Token,
                Expires = newRefreshToken.Expires,
                UserId = user.Id
            });
            await _unitOfWork.CompleteAsync();

            return new AuthResponseDto(newAccessToken.Token, newRefreshToken.Token, newRefreshToken.Expires, true, "Renewed session");
        }
    }
}