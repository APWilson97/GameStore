using System.Data;
using GameStoreAPI.Contracts;

namespace GameStoreAPI.Endpoints;

// Extend classes we don't own like WebApplication so that with one call we can map all our endpoints to app
// Contains extension methods
public static class GamesEndpoints
{
    const string GetGameEndpointName = "GetGame";

    private static readonly List<GameDto> games = [
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

    // this keyword makes it into extension method, this method is going to show up as new method of WebApplication class
    public static RouteGroupBuilder MapGamesEndpoints(this WebApplication app)
    {
        // Defines common prefixes for all routes, routes chain into group games
        var group = app.MapGroup("games")
                        .WithParameterValidation();
                        // Appropriate endpoints filters will be applied and recognize data annotations specified in CreateGameDto

        // GET /games
        group.MapGet("/", () => games);

        // GET /games/1
        group.MapGet("/{id}", (int id) => 
        {
            GameDto? game = games.Find(game => game.Id == id);
            // Have to return value with type IResult, hence why we cannot just return 'game' in this case
            return game is null ? Results.NotFound() : Results.Ok(game);
        })
        .WithName(GetGameEndpointName);

        // POST /games
        group.MapPost("/", (CreateGameDto newGame) => 
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
        group.MapPut("/{id}", (int id, UpdateGameDto updatedGame) => 
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
        group.MapDelete("/{id}", (int id) => 
        {
            games.RemoveAll(game => game.Id == id);

            return Results.NoContent();
        });

        return group;
    }
}
