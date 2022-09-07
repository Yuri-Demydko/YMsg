using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using YMsg.Models.ResponseModels;

namespace YMsg.Controllers.Ping;

[ApiController]
public class PingController:ControllerBase
{
    [HttpGet("/ping")]
    [AllowAnonymous]
    public IActionResult Ping()
    {
        return Ok(new PingResponse()
        {
            Message = "YMsg - Hello",
            CurrentServerDateTime = DateTime.Now
        });
    }
    
}