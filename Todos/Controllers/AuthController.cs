using Microsoft.AspNetCore.Mvc;
using Todos.Dtos;
using Todos.Entities;
using Todos.Models;
using Todos.Services;

namespace Todos.Controllers;

[ApiController]
[Route("/api/auth")]
public class AuthController : ControllerBase
{
    private readonly IJwtTokenHandler _tokenHandler;
    private readonly IUserService _userService;

    public AuthController(IJwtTokenHandler tokenHandler, IUserService userService)
    {
        _tokenHandler = tokenHandler ?? throw new ArgumentNullException(nameof(tokenHandler));
        _userService = userService ?? throw new ArgumentNullException(nameof(userService));
    }

    [HttpPost("register")]
    public async Task<ActionResult<UserResponseDto>> Register(UserCreatedDto user)
    {
        var registeredUser =
            await _userService.CreateUser(new User(user.Username, user.Password, user.FirstName, user.LastName));
        return new UserResponseDto(registeredUser.Username, registeredUser.FirstName, registeredUser.LastName);
    }

    [HttpPost("login")]
    public async Task<ActionResult<JwtToken>> Login(UserLoginDto userLoginDto)
    {
        var obtainedToken = await _tokenHandler.GenerateToken(userLoginDto.Username, userLoginDto.Password);
        if (string.IsNullOrWhiteSpace(obtainedToken.Token))
        {
            return Forbid();
        }
        return Ok(obtainedToken);
    }
}