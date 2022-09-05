using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace YMsg.Controllers.Ping;

[ApiController]
public class PingController:ControllerBase
{
    [HttpGet("/ping")]
    [AllowAnonymous]
    public IActionResult Ping()
    {
        return Ok(new
        {
            Message = "YMsg - Hello",
            Today = DateTime.Now,
            IP = Request?.HttpContext?.Connection?.RemoteIpAddress?.ToString()
        });
    }
    
}