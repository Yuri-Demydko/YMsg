using System.Configuration;
using Entities.DbModels;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Entities;

public class AppDbContext:IdentityDbContext<User>
{
    public DbSet<Message> Messages { get; set; }
    
    public DbSet<CacheDbSchemaUpdate> CacheDbSchemaUpdates { get; set; }

    // public AppDbContext() : base()
    // {
    //     
    // }
    

    public AppDbContext(DbContextOptions options) : base(options) { }
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ApplyConfiguration(new UserConfiguration());
        builder.ApplyConfiguration(new MessageConfiguration());

        builder.Entity<CacheDbSchemaUpdate>().Property(r => r.CreatedAt)
            .HasDefaultValueSql("CURRENT_TIMESTAMP");

        builder.Entity<CacheDbSchemaUpdate>().HasData(new List<CacheDbSchemaUpdate>()
        {
            new CacheDbSchemaUpdate()
            {
                Version = "1.0",
                Id = Guid.NewGuid()
            }
        });

    }
    
    
    
    // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    // {
    //    // base.OnConfiguring(optionsBuilder);
    //     optionsBuilder.UseNpgsql(
    //         "");
    // }
}