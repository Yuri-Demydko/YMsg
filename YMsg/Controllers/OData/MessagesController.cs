using AutoMapper;
using Common.Constants;
using Entities;
using Entities.DbModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.EntityFrameworkCore;
using Z.EntityFramework.Plus;

namespace YMsg.Controllers.OData;

[Authorize(Roles = UserRoles.Admin)]
public class MessagesController : BaseODataController<Message>
{
    public MessagesController(ILogger<Message> logger, UserManager<User> userManager, RoleManager<IdentityRole> roleManager,
        IConfiguration configuration, IMapper mapper, AppDbContext context) : base(logger, userManager, roleManager,
        configuration, mapper, context)
    {
    }

    [EnableQuery]
    [HttpGet("Messages")]
    public IActionResult Get()
    {
        return Ok(_context.Messages.IgnoreAutoIncludes()
            .IncludeOptimized(r => r.UserTo)
            .IncludeOptimized(r => r.UserFrom));
    }

    [EnableQuery]
    [HttpGet("Messages({key})")]
    public async Task<IActionResult> Get(Guid key)
    {
        var userId = (await GetUserModelAsync()).Id;
        var item = await _context.Messages
           .IncludeOptimized(r => r.UserTo)
           .IncludeOptimized(r => r.UserFrom)
           .FirstOrDefaultAsync(r => r.Id == key&&
                                     r.UserFromId==userId||
                                     r.UserToId==userId);

        return item != null ? Ok(item) : NotFound();
    }

    [HttpPost("Messages")]
    public async Task<IActionResult> Post([FromBody]Message item)
    {
        if (item.UserToId == null)
        {
            return BadRequest();
        }

        var userId = (await GetUserModelAsync()).Id;
        item.UserFromId = userId;
        await _context.Messages.AddAsync(item);
        await _context.SaveChangesAsync();
        return Ok(item);
    }

    [HttpDelete("Messages({key})")]
    public async Task<IActionResult> Delete(Guid key)
    {
        var item = await _context.Messages
            .FirstOrDefaultAsync(r => r.Id == key);

        if (item == null)
        {
            return NotFound();
        }
        
        _context.Remove(item);
        await _context.SaveChangesAsync();
        return NoContent();

    }
}