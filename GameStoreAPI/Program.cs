using GameStoreAPI.Data;
using GameStoreAPI.Endpoints;
using SQLitePCL;

var builder = WebApplication.CreateBuilder(args);

// Configuration object implements IConfiguration and collects configuration information from all sources that has been configured for this application, including appsettings.json
var connectionString = builder.Configuration.GetConnectionString("GameStore");
// Registering services for the builder to use using dependency injection, this creates connection to database
// EFC is going to read connection string, create instance of GameStoreContext and pass in DbContextOptions that are going to contain all details
// in connection string to connect to database
builder.Services.AddSqlite<GameStoreContext>(connectionString);
// This registers GameStoreContext in the service container, so ASP.NET Core knows about the type and is ready to provide us with an instance of it whenever we request it

var app = builder.Build();

app.MapGamesEndpoints();
app.MapGenresEndpoints();

await app.MigrateDbAsync();

app.Run();
