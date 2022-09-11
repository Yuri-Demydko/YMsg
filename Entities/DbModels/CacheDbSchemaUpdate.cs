namespace Entities.DbModels;

public class CacheDbSchemaUpdate
{
    public Guid Id { get; set; }
    
    public string Version { get; set; }
    
    public DateTime CreatedAt { get; set; }
}