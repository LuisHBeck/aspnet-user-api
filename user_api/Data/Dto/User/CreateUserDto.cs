using System.ComponentModel.DataAnnotations;

namespace user_api.Data.Dto.User;

public class CreateUserDto
{
    [Required]
    public string Username { get; set; }

    [Required]
    public string BirthDate { get; set; }

    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }

    [Required]
    [Compare("Password")]
    public string PasswordConfirmation { get; set; }
}