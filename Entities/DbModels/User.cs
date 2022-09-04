using Microsoft.AspNetCore.Identity;

namespace Entities.DbModels;

public class User:IdentityUser
{
    public string? DisplayName { get; set; }
}