using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using user_api.Data;
using user_api.Data.Dto.User;
using user_api.Models;

namespace user_api.Controllers;

[ApiController]
[Route("users")]
public class UserController : ControllerBase
{
    private UserDbContext _context; 
    private IMapper _mapper;
    private UserManager<User> _userManager;

    public UserController(
        UserDbContext context, IMapper mapper, UserManager<User> userManager
    )
    {
        this._context = context;
        this._mapper = mapper;
        this._userManager = userManager;
    }

    [HttpPost]
    public async Task<IActionResult> RegisterUser(CreateUserDto userDto)
    {
        User user = _mapper.Map<User>(userDto);
        IdentityResult result = await _userManager.CreateAsync(user, userDto.Password);
        if(result.Succeeded) return Ok("successfully registered user");
        throw new ApplicationException("failed to register user");
    }
}