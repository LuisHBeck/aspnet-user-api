using Microsoft.AspNetCore.Mvc;

namespace user_api.Controllers;

[ApiController]
[Route("users")]
public class UserController : ControllerBase
{
    [HttpPost]
    public IActionResult RegisterUser()
    {
        throw new NotImplementedException();
    }
}