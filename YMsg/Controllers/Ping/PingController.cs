using Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using YMsg.Models.ResponseModels;

namespace YMsg.Controllers.Ping;

[ApiController]
public class PingController:ControllerBase
{
    private readonly AppDbContext _context;

    public PingController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet("/ping")]
    [AllowAnonymous]
    public async Task<IActionResult> Ping()
    {
        var dbUpdate = await _context.CacheDbSchemaUpdates.OrderByDescending(r=>r.CreatedAt).FirstAsync();
        
        return Ok(new PingResponse()
        {
            Message = "YMsg - Hello",
            CurrentServerDateTime = DateTime.Now,
            CacheDbVersion = dbUpdate.Version
        });
    }
    
}