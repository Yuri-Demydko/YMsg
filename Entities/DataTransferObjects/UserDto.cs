using System.ComponentModel.DataAnnotations;

namespace Entities.DataTransferObjects;

public class UserDto
{
    public Guid Id { get; set; } 
    
    [Required]
    [MaxLength(40)]
    public string DisplayName { get; set; }
    
    [Required]
    [MaxLength(20)]
    public string UserName { get; set; }
}