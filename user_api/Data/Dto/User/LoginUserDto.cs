using System.ComponentModel.DataAnnotations;

namespace user_api.Data.Dto.User;

public class LoginUserDto
{
    [Required]
    public string Username { get; set; }

    [Required]
    public string Password { get; set; }
}