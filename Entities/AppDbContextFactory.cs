using System.Configuration;
using Microsoft.EntityFrameworkCore;

namespace Entities;

public class AppDbContextFactory:IDbContextFactory<AppDbContext>
{
    public AppDbContext CreateDbContext()
    {
        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();

        var connectionString = ConfigurationManager.ConnectionStrings["Default"].ConnectionString;
        
        optionsBuilder.UseNpgsql(connectionString);  
  
        return new AppDbContext(optionsBuilder.Options);  
    }
}