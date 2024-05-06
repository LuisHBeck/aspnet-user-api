using Microsoft.AspNetCore.Identity;

namespace user_api.Models;

public class User : IdentityUser
{
    public string BirthDate { get; set; }

    public User() : base()
    {
        
    }
}