using AutoMapper;
using Entities;
using Entities.DbModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.OpenApi;
using Microsoft.OpenApi.Extensions;
using Microsoft.OpenApi.OData;
using YMsg.Models.Edm;

namespace YMsg.Controllers.OData;

[Authorize]
[Route("api")]
public abstract class BaseODataController<TModel>:ODataController
{
    protected readonly ILogger<TModel> _logger;
    protected readonly UserManager<User> _userManager;
    protected readonly RoleManager<IdentityRole> _roleManager;
    protected readonly IConfiguration _configuration;
    protected readonly IMapper _mapper;
    protected readonly AppDbContext _context;

    protected BaseODataController(ILogger<TModel> logger, UserManager<User> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration, IMapper mapper, AppDbContext context)
    {
        _logger = logger;
        _userManager = userManager;
        _roleManager = roleManager;
        _configuration = configuration;
        _mapper = mapper;
        _context = context;
    }

    protected async Task<User> GetUserModelAsync()
    {
        return await _userManager.FindByNameAsync(User?.Identity?.Name);
    }
    
}