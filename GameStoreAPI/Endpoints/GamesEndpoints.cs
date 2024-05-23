using System.Data;
using GameStoreAPI.Contracts;
using GameStoreAPI.Data;
using GameStoreAPI.Entities;
using GameStoreAPI.Mapping;
using Microsoft.EntityFrameworkCore;

namespace GameStoreAPI.Endpoints;

// Extend classes we don't own like WebApplication so that with one call we can map all our endpoints to app
// Contains extension methods
public static class GamesEndpoints
{
    const string GetGameEndpointName = "GetGame";

    // Dependency injection of GameStoreContext into endpoints, this removes the need for in memory database through using a List of games
    // previously and properly tracks and persists changes to entities in the database, as well as retrieval of those entities to send back to client

    // this keyword makes it into extension method, this method is going to show up as new method of WebApplication class
    public static RouteGroupBuilder MapGamesEndpoints(this WebApplication app)
    {
        // Defines common prefixes for all routes, routes chain into group games
        var group = app.MapGroup("games")
                        .WithParameterValidation();
                        // Appropriate endpoints filters will be applied and recognize data annotations specified in CreateGameDto

        // GET /games
        group.MapGet("/", async (GameStoreContext dbContext) => 
        {
            return await dbContext.Games
                        // Ensures Genre property is not null for correct mapping into game summary dto, includes Genre property for each Game
                        .Include(game => game.Genre)
                        .Select(game => game.ToGameSummaryDto())
                        // Tells EFC to not do any tracking of the return entities, just send it back to client for performance
                        .AsNoTracking()
                        .ToListAsync();
        });

        // GET /games/1
        group.MapGet("/{id}", async (int id, GameStoreContext dbContext) => 
        {
            Game? game = await dbContext.Games.FindAsync(id);
            // Have to return value with type IResult, hence why we cannot just return 'game' in this case
            return game is null ? Results.NotFound() : Results.Ok(game.ToGameDetailsDto());
        })
        .WithName(GetGameEndpointName);

        // POST /games
        group.MapPost("/", async (CreateGameDto newGame, GameStoreContext dbContext) => 
        {
            Game game = newGame.ToEntity();
            
            dbContext.Games.Add(game);
            await dbContext.SaveChangesAsync();

            // We should not return internal entities back to client, only our DTOs available hence why we're converting game into gameDto

            // Provides location header to the client to tell it where the resource is created, first param is name of route, second param
            // is the value that needs to be provided to the route in the first param (standard is use anonymous type, being new {id = game.id})
            // third param is what we send back to client in payload
            return Results.CreatedAtRoute(GetGameEndpointName, new { id = game.Id }, game.ToGameDetailsDto());
        });
        

        // PUT /games
        group.MapPut("/{id}", async (int id, UpdateGameDto updatedGame, GameStoreContext dbContext) => 
        {
            var existingGame = await dbContext.Games.FindAsync(id);
            
            if (existingGame == null)
            {
                return Results.NotFound();
            }

            dbContext.Entry(existingGame)
                        .CurrentValues
                        .SetValues(updatedGame.ToEntity(id));
            
            await dbContext.SaveChangesAsync();

            // Convention is to return NoContent back to client for PUT operation
            return Results.NoContent();
        });

        // DELETE /games
        group.MapDelete("/{id}", async (int id, GameStoreContext dbContext) => 
        {
            await dbContext.Games
                        .Where(game => game.Id == id)
                        .ExecuteDeleteAsync();

            return Results.NoContent();
        });

        return group;
    }
}
