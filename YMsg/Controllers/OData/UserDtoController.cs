using AutoMapper;
using Entities;
using Entities.DataTransferObjects;
using Entities.DbModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace YMsg.Controllers.OData;

public class UserDtoController : BaseODataController<UserDto>
{
    public UserDtoController(ILogger<UserDto> logger, UserManager<User> userManager,
        RoleManager<IdentityRole> roleManager, IConfiguration configuration, IMapper mapper, AppDbContext context) :
        base(logger, userManager, roleManager, configuration, mapper, context)
    {
    }

    [HttpGet("Profile")]
    public async Task<IActionResult> GetProfile()
    {
        var user = await GetUserModelAsync();
        if (user == null)
        {
            return NotFound();
        }

        return Ok(_mapper.Map<UserDto>(user));
    }

    [HttpPut("UpdateProfile")]
    public async Task<IActionResult> UpdateProfile([FromBody]UserDto item)
    {
        var user = await GetUserModelAsync();

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        

        if ((await _context.Users.AnyAsync(r => r.UserName == item.UserName && r.Id != user.Id.ToString())))
        {
            {
                return BadRequest("Invalid username");
            }
        }

        var dbUser = await _context.Users.FirstAsync(r => r.Id == user.Id);

        if(!string.IsNullOrWhiteSpace(item.DisplayName))
        {
            dbUser.DisplayName = item.DisplayName;
        }
        if(!string.IsNullOrWhiteSpace(item.UserName))
        {
            dbUser.UserName = item.UserName;
            dbUser.NormalizedUserName = item.UserName.ToUpper();
        }

        _context.Users.Update(dbUser);
        await _context.SaveChangesAsync();


        return Updated(_mapper.Map<UserDto>(item));
    }
}