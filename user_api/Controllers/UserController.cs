using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using user_api.Data;
using user_api.Data.Dto.User;
using user_api.Models;
using user_api.Services;

namespace user_api.Controllers;

[ApiController]
[Route("users")]
public class UserController : ControllerBase
{
    private RegisterService _registerService;

    public UserController(RegisterService registerService)
    {
        this._registerService = registerService;
    }

    [HttpPost]
    public async Task<IActionResult> RegisterUser(CreateUserDto userDto)
    {
        await _registerService.Register(userDto);
        return Ok("successfully registered user");
    }
}