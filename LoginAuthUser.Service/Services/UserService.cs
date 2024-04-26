using AutoMapper;
using ComandaPro.CrossCutting.Notifications;
using ComandaPro.Domain.Dtos.Default;
using ComandaPro.Domain.Extensions;
using ComandaPro.Service.Services;
using LoginAuthUser.Domain.Dtos.User;
using LoginAuthUser.Domain.Entities;
using LoginAuthUser.Domain.Filters;
using LoginAuthUser.Domain.Interfaces.Repositories;
using LoginAuthUser.Domain.Interfaces.Services;
using LoginAuthUser.Service.Validators.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

namespace LoginAuthUser.Service.Services;

public class UserService : BaseService, IUserService
{
    private readonly IConfiguration _configuration;
    private readonly IUserRepository _userRepository;
    private readonly UserManager<User> _userManager;
    private readonly IMapper _mapper;
    private readonly NotificationContext _notificationContext;

    public UserService(
        IConfiguration configuration,
        IUserRepository userRepository,
        UserManager<User> userManager,
        IMapper mapper,
        NotificationContext notificationContext)
    {
        _configuration = configuration;
        _userRepository = userRepository;
        _userManager = userManager;
        _mapper = mapper;
        _notificationContext = notificationContext;
    }

    public async Task<ICollection<UserResponseDto>> GetAll(UserFilter filter)
    {
        var users = (await _userRepository
            .Select())
            .AsQueryable()
            .OrderByDescending(u => u.CreatedAt)
            .ApplyFilter(filter);

        if (filter.FirstName is not null)
            users = users.Where(u => u.FirstName == filter.FirstName);

        if (filter.LastName is not null)
            users = users.Where(u => u.LastName == filter.LastName);

        if (filter.Document is not null)
            users = users.Where(u => u.Document == filter.Document);

        if (filter.Active is not null)
            users = users.Where(u => u.Active == filter.Active);

        if (filter.Approved is not null)
            users = users.Where(u => u.Approved == filter.Approved);

        var response = _mapper.Map<List<UserResponseDto>>(users);

        return response;
    }

    public async Task<UserResponseDto> GetById(int id)
    {
        var user = await _userRepository.Select(id);
        var response = _mapper.Map<UserResponseDto>(user);
        return response;
    }

    public async Task<DefaultServiceResponseDto> Update(UpdateUserDto updateUserDto, int id)
    {
        var validationResult = Validate(updateUserDto, Activator.CreateInstance<UpdateUserValidator>());
        if (!validationResult.IsValid) { _notificationContext.AddNotifications(validationResult.Errors); return default; }

        var existsUser = await _userManager.FindByNameAsync(updateUserDto.Username);
        if (existsUser is not null && existsUser.Id != id) { _notificationContext.AddNotification(StaticNotifications.UsernameAlreadyExists); return default; }

        var user = await _userManager.FindByIdAsync(id.ToString());
        _mapper.Map(updateUserDto, user);
        var updateUserResult = await _userManager.UpdateAsync(user);

        if (!updateUserResult.Succeeded)
        {
            var errors = updateUserResult.Errors.Select(t => new Notification(t.Code, t.Description));
            _notificationContext.AddNotifications(errors);
            return default;
        }

        return new DefaultServiceResponseDto
        {
            Success = true,
            Message = StaticNotifications.UserEdited.Message
        };
    }

    public async Task<DefaultServiceResponseDto> UpdatePassword(UpdateUserPasswordDto updateUserPasswordDto, int id)
    {
        var validationResult = Validate(updateUserPasswordDto, Activator.CreateInstance<UpdateUserPasswordValidator>());
        if (!validationResult.IsValid) { _notificationContext.AddNotifications(validationResult.Errors); return default; }

        var user = await _userManager.FindByIdAsync(id.ToString());
        var changePasswordResult = await _userManager.ChangePasswordAsync(user, updateUserPasswordDto.CurrentPassword, updateUserPasswordDto.NewPassword);

        if (!changePasswordResult.Succeeded)
        {
            var errors = changePasswordResult.Errors.Select(t => new Notification(t.Code, t.Description));
            _notificationContext.AddNotifications(errors);
            return default;
        }

        return new DefaultServiceResponseDto
        {
            Success = true,
            Message = StaticNotifications.PasswordChanged.Message
        };
    }

    public async Task<DefaultServiceResponseDto> Delete(int id)
    {
        var user = await _userManager.FindByIdAsync(id.ToString());
        var deleteUserResult = await _userManager.DeleteAsync(user);

        if (!deleteUserResult.Succeeded)
        {
            var errors = deleteUserResult.Errors.Select(t => new Notification(t.Code, t.Description));
            _notificationContext.AddNotifications(errors);
            return default;
        }

        return new DefaultServiceResponseDto
        {
            Success = true,
            Message = StaticNotifications.UserDeleted.Message
        };
    }

    public async Task<DefaultServiceResponseDto> ActivateAsync(ActivateUserDto activateUserDto)
    {
        var user = await _userManager.FindByIdAsync(activateUserDto.Id.ToString());
        if (user == null)
        {
            _notificationContext.AddNotification(StaticNotifications.UserNotFound);
            return default;
        }
        user.Active = activateUserDto.Active;
        var activateUserResult = await _userManager.UpdateAsync(user);
        if (!activateUserResult.Succeeded)
        {
            var errors = activateUserResult.Errors.Select(t => new Notification(t.Code, t.Description));
            _notificationContext.AddNotifications(errors);
            return default;
        }
        return new DefaultServiceResponseDto
        {
            Success = true,
            Message = StaticNotifications.UserActivated.Message
        };
    }

}
