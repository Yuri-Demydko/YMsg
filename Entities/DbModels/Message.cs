using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Entities.DbModels;

public class Message
{
    public Guid Id { get; set; }
    
    public User UserFrom { get; set; }
    
    public string UserFromId { get; set; }

    public User UserTo { get; set; }
    
    public string UserToId { get; set; }
    
    public string Text { get; set; }
    
    public DateTime CreatedAt { get; set; }
}

public class MessageConfiguration : IEntityTypeConfiguration<Message>
{
    public void Configure(EntityTypeBuilder<Message> item)
    {
        item.HasOne(r => r.UserFrom)
            .WithMany(r => r.MessagesFrom)
            .HasForeignKey(r => r.UserFromId);

        item.HasOne(r => r.UserTo)
            .WithMany(r => r.MessagesTo)
            .HasForeignKey(r => r.UserToId);
        
        item.Property(i => i.CreatedAt)
            .HasDefaultValueSql("CURRENT_TIMESTAMP");
    }
}