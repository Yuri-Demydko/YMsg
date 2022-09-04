using Entities.DbModels;
using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;

namespace YMsg.Models.Edm;

public class AppEdmModel
{
    public static IEdmModel GetEdmModel()
    {
        var modelBuilder = new ODataConventionModelBuilder();
        modelBuilder.EntitySet<User>("Users");
        modelBuilder.EntitySet<Message>("Messages");
        
        // Send as Lower Camel Case Properties, so the JSON looks better:
        modelBuilder.EnableLowerCamelCase();
        return modelBuilder.GetEdmModel();
    }
}