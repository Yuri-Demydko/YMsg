namespace Entities.DataTransferObjects;

public class UserDto
{
    public Guid Id { get; set; } 
    
    public string? DisplayName { get; set; }
    
    public string UserName { get; set; }
}