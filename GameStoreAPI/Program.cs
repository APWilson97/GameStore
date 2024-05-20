using GameStoreAPI.Contracts;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

const string GetGameEndpointName = "GetGame";

List<GameDto> games = [
    new GameDto(
        1,
        "Xenogears",
        "JRPG",
        59.99M,
        new DateOnly(1998, 2, 11)
    ), 
    new GameDto(
        2, 
        "R4: Ridge Racer Type 4",
        "Racing",
        29.99M,
        new DateOnly(1998, 12, 3)
    ),
    new GameDto(
        3,
        "Diablo 2",
        "ARPG",
        49.99M,
        new DateOnly(2000, 6, 28)
    )
];

// GET /games
app.MapGet("games", () => games);

// GET /games/1
app.MapGet("games/{id}", (int id) => 
{
    GameDto? game = games.Find(game => game.Id == id);
    // Have to return value with type IResult, hence why we cannot just return 'game' in this case
    return game is null ? Results.NotFound() : Results.Ok(game);
})
.WithName(GetGameEndpointName);

// POST /games
app.MapPost("games", (CreateGameDto newGame) => 
{
    GameDto game = new(
        games.Count + 1,
        newGame.Name,
        newGame.Genre,
        newGame.Price,
        newGame.ReleaseDate
    );
    
    games.Add(game);

    // Provides location header to the client to tell it where the resource is created, first param is name of route, second param
    // is the value that needs to be provided to the route in the first param (standard is use anonymous type, being new {id = game.id})
    // third param is what we send back to client in payload
    return Results.CreatedAtRoute(GetGameEndpointName, new { id = game.Id }, game);
});

// PUT /games
app.MapPut("games/{id}", (int id, UpdateGameDto updatedGame) => 
{
    var index = games.FindIndex(game => game.Id == id);
    
    if (index == -1)
    {
        return Results.NotFound();
    }

    games[index] = new GameDto(
        id,
        updatedGame.Name,
        updatedGame.Genre,
        updatedGame.Price,
        updatedGame.ReleaseDate
    );

    // Convention is to return NoContent back to client for PUT operation
    return Results.NoContent();
});

// DELETE /games
app.MapDelete("games/{id}", (int id) => 
{
    games.RemoveAll(game => game.Id == id);

    return Results.NoContent();
});

app.Run();
