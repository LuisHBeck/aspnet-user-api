using Microsoft.AspNetCore.Authorization;

namespace user_api.Authorization;

public class MinimalAge : IAuthorizationRequirement
{
    public int Age { get; set; }

    public MinimalAge(int age)
    {
        this.Age = age;
    }
}