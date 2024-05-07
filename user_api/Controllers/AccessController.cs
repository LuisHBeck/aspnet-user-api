using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace user_api.Controllers;

[ApiController]
[Route("access")]
public class AccessController : ControllerBase
{
    [HttpGet]
    [Authorize(Policy = "MinimalAge")]
    public IActionResult Get()
    {
        return Ok("granted access");
    }
}