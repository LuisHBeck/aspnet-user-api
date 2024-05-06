using Microsoft.AspNetCore.Mvc;
using user_api.Data.Dto.User;
using user_api.Services;

namespace user_api.Controllers;

[ApiController]
[Route("users")]
public class UserController : ControllerBase
{
    private UserService _userService;

    public UserController(UserService userService)
    {
        this._userService = userService;
    }

    // REGISTRATION
    [HttpPost("registration")]
    public async Task<IActionResult> RegisterUser(CreateUserDto userDto)
    {
        await _userService.Register(userDto);
        return Ok("successfully registered user");
    }

    // LOGIN
    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginUserDto userDto)
    {
        await _userService.Login(userDto);
        return Ok("User authenticated");
    }
}