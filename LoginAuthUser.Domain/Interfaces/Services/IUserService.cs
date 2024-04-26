using ComandaPro.Domain.Dtos.Default;
using LoginAuthUser.Domain.Dtos.User;
using LoginAuthUser.Domain.Filters;

namespace LoginAuthUser.Domain.Interfaces.Services;

public interface IUserService
{
    Task<ICollection<UserResponseDto>> GetAll(UserFilter filter);
    Task<UserResponseDto> GetById(int id);
    Task<DefaultServiceResponseDto> Update(UpdateUserDto updateUserDto, int id);
    Task<DefaultServiceResponseDto> UpdatePassword(UpdateUserPasswordDto updateUserPasswordDto, int id);
    Task<DefaultServiceResponseDto> Delete(int id);
    Task<DefaultServiceResponseDto> ActivateAsync(ActivateUserDto activateUserDto);
}
