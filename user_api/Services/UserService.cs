using AutoMapper;
using Microsoft.AspNetCore.Identity;
using user_api.Data.Dto.User;
using user_api.Models;

namespace user_api.Services;

public class UserService
{
    private UserManager<User> _userManager;
    private SignInManager<User> _signInManager;
    private TokenService _tokenService;
    private IMapper _mapper;

    public UserService(
        UserManager<User> userManager,
        SignInManager<User> signInManager,
        TokenService tokenService,
        IMapper mapper
    )
    {
        this._userManager = userManager;
        this._signInManager = signInManager;
        this._tokenService = tokenService;
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
    public async Task<string> Login(LoginUserDto userDto)
    {
        var result = await _signInManager.PasswordSignInAsync(
            userDto.Username, userDto.Password, false, false
        );
        if(!result.Succeeded) throw new ApplicationException("failed to login");

        User? user = _signInManager
            .UserManager
            .Users
            .FirstOrDefault(
                user => user.NormalizedUserName == 
                userDto.Username.ToUpper()
            );

        string token = _tokenService.GenerateToken(user);
        return token;
    }
}