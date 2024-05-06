using AutoMapper;
using Microsoft.AspNetCore.Identity;
using user_api.Data.Dto.User;
using user_api.Models;

namespace user_api.Services;

public class UserService
{
    private UserManager<User> _userManager;
    private SignInManager<User> _signInManager;
    private IMapper _mapper;

    public UserService(
        UserManager<User> userManager,
        SignInManager<User> signInManager,
        IMapper mapper
    )
    {
        this._userManager = userManager;
        this._signInManager = signInManager;
        this._mapper = mapper;
    }
    
    // REGISTRATION
    public async Task Register(CreateUserDto userDto)
    {
        User user = _mapper.Map<User>(userDto);
        IdentityResult result = await _userManager.CreateAsync(user, userDto.Password);
        if(!result.Succeeded) throw new ApplicationException("failed to register user");
    }

    // LOGIN
    public async Task Login(LoginUserDto userDto)
    {
        var result = await _signInManager.PasswordSignInAsync(
            userDto.Username, userDto.Password, false, false
        );
        if(!result.Succeeded) throw new ApplicationException("failed to login");
    }
}