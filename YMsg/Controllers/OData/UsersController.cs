using AutoMapper;
using Common.Constants;
using Entities;
using Entities.DataTransferObjects;
using Entities.DbModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Deltas;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.EntityFrameworkCore;
using Z.EntityFramework.Plus;

namespace YMsg.Controllers.OData;

[Authorize(Roles = UserRoles.Admin)]
public class UsersController : BaseODataController<User>
{
    public UsersController(ILogger<User> logger, UserManager<User> userManager, RoleManager<IdentityRole> roleManager,
        IConfiguration configuration, IMapper mapper, AppDbContext context) : base(logger, userManager, roleManager,
        configuration, mapper, context)
    {
    }
    

    [EnableQuery]
    [HttpGet("Users")]
    public IActionResult Get()
    {
        return Ok(_context.Users
            .IncludeOptimized(r => r.MessagesFrom)
            .IncludeOptimized(r => r.MessagesTo));
    }

    [EnableQuery]
    [HttpGet("Users({key})")]
    public async Task<IActionResult> Get(Guid key)
    {
        var item = await _context.Users
           .IncludeOptimized(r => r.MessagesFrom
               .Select(r=>r.UserTo)
           )
            .IncludeOptimized(r => r.MessagesTo
               .Select(r=>r.UserFrom))
           .FirstOrDefaultAsync(r => r.Id == key.ToString());

        return item != null ? Ok(item) : NotFound();
    }


    [HttpDelete("Users({key})")]
    public async Task<IActionResult> Delete(Guid key)
    {
        var item = await _context.Users
            .FirstOrDefaultAsync(r => r.Id == key.ToString());

        if (item == null)
        {
            return NotFound();
        }
        
        _context.Remove(item);
        await _context.SaveChangesAsync();
        return NoContent();

    }
}