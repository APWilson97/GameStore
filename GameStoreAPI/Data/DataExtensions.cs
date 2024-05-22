using Microsoft.EntityFrameworkCore;

namespace GameStoreAPI.Data;

// code that will perform migrations automatically when application starts up
public static class DataExtensions
{
    public static void MigrateDb(this WebApplication app)
    {
        // Scoped lifetime is needed, we cannot just access dbContext directly, we need to provide a scope that allows us to start interacting with database
        // Creating instance of scope that we can use to request service container of ASP.NET Core to give us an instance of some of the services that have been registered
        // in our application
        using var scope = app.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<GameStoreContext>();
        dbContext.Database.Migrate();
    }
}
