using ComandaPro.Domain.Dtos.Default;
using ComandaPro.Domain.Utilities;
using LoginAuthUser.Domain.Dtos.Auth;

namespace LoginAuthUser.Domain.Interfaces.Services;

public interface IAuthService
{
    Task<DefaultServiceResponseDto> RegisterAsync(RegisterDto registerDto, string role = StaticUserRoles.USER);
    Task<LoginResponseDto> LoginAsync(LoginDto loginDto);
    Task<DefaultServiceResponseDto> RevokeAsync(string userName);
    Task<LoginResponseDto> RefreshTokenAsync(string accessToken, string refreshToken, string userName);
}
