using GameStoreAPI.Data;
using GameStoreAPI.Endpoints;
using SQLitePCL;

var builder = WebApplication.CreateBuilder(args);

var connectionString = "Data Source=GameStore.db";
// Registering services for the builder to use using dependency injection, this creates connection to database
// EFC is going to read connection string, create instance of GameStoreContext and pass in DbContextOptions that are going to contain all details
// in connection string to connect to database
builder.Services.AddSqlite<GameStoreContext>(connectionString);

var app = builder.Build();

app.MapGamesEndpoints();

app.Run();
