using AutoMapper;
using user_api.Data.Dto.User;
using user_api.Models;

namespace user_api.Profiles;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<CreateUserDto, User>();
    }   
}