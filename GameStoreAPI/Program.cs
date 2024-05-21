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

var app = builder.Build();

app.MapGamesEndpoints();

app.Run();
