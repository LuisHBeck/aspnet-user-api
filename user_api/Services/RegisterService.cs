using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using user_api.Data.Dto.User;
using user_api.Models;

namespace user_api.Services;

public class RegisterService
{
    private UserManager<User> _userManager;
    private IMapper _mapper;

    public RegisterService(
        UserManager<User> userManager,
        IMapper mapper
    )
    {
        this._userManager = userManager;
        this._mapper = mapper;
    }

    public async Task Register(CreateUserDto userDto)
    {
        User user = _mapper.Map<User>(userDto);
        IdentityResult result = await _userManager.CreateAsync(user, userDto.Password);
        if(!result.Succeeded) throw new ApplicationException("failed to register user");
    }
}