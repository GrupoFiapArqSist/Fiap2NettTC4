﻿using ComandaPro.Domain.Dtos.Default;
using ComandaPro.Domain.Extensions;
using LoginAuthUser.Domain.Dtos.User;
using LoginAuthUser.Domain.Filters;
using LoginAuthUser.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;

namespace LoginAuthUser.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class UserController : Controller
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    [SwaggerOperation(Summary = "Get all users")]
    [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(IList<UserResponseDto>))]
    [SwaggerResponse((int)HttpStatusCode.InternalServerError)]
    [SwaggerResponse((int)HttpStatusCode.Unauthorized)]
    [SwaggerResponse((int)HttpStatusCode.Forbidden)]
    public async Task<IActionResult> GetAll([FromQuery] UserFilter filter)
    {
        var users = await _userService.GetAll(filter);
        if (users is null)
            return NotFound();

        return Ok(users);
    }

    [HttpGet("{id}")]
    [SwaggerOperation(Summary = "Get user by id")]
    [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(UserResponseDto))]
    [SwaggerResponse((int)HttpStatusCode.NoContent)]
    [SwaggerResponse((int)HttpStatusCode.BadRequest, Type = typeof(IReadOnlyCollection<dynamic>))]
    [SwaggerResponse((int)HttpStatusCode.InternalServerError)]
    [SwaggerResponse((int)HttpStatusCode.Unauthorized)]
    [SwaggerResponse((int)HttpStatusCode.Forbidden)]
    public async Task<IActionResult> GetById(int id)
    {
        var user = await _userService.GetById(id);
        if (user is null)
            return NotFound();

        return Ok(user);
    }

    [HttpPut]
    [SwaggerOperation(Summary = "Update user")]
    [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(DefaultServiceResponseDto))]
    [SwaggerResponse((int)HttpStatusCode.BadRequest, Type = typeof(IReadOnlyCollection<dynamic>))]
    [SwaggerResponse((int)HttpStatusCode.InternalServerError)]
    [SwaggerResponse((int)HttpStatusCode.Unauthorized)]
    [SwaggerResponse((int)HttpStatusCode.Forbidden)]
    public async Task<IActionResult> Update([FromBody] UpdateUserDto updateUserDto)
    {
        var response = await _userService.Update(updateUserDto, this.GetUserIdLogged());
        return Ok(response);
    }

    [HttpPut("Password")]
    [Authorize]
    [SwaggerOperation(Summary = "Change password")]
    [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(DefaultServiceResponseDto))]
    [SwaggerResponse((int)HttpStatusCode.BadRequest, Type = typeof(IReadOnlyCollection<dynamic>))]
    [SwaggerResponse((int)HttpStatusCode.InternalServerError)]
    [SwaggerResponse((int)HttpStatusCode.Unauthorized)]
    public async Task<IActionResult> UpdatePassword([FromBody] UpdateUserPasswordDto updateUserPasswordDto)
    {
        var response = await _userService.UpdatePassword(updateUserPasswordDto, this.GetUserIdLogged());
        return Ok(response);
    }


    [HttpDelete]
    [SwaggerOperation(Summary = "Delete user")]
    [SwaggerResponse((int)HttpStatusCode.OK, Type = typeof(DefaultServiceResponseDto))]
    [SwaggerResponse((int)HttpStatusCode.InternalServerError)]
    [SwaggerResponse((int)HttpStatusCode.Unauthorized)]
    [SwaggerResponse((int)HttpStatusCode.Forbidden)]
    public async Task<IActionResult> Delete()
    {
        var response = await _userService.Delete(this.GetUserIdLogged());
        return Ok(response);
    }
}