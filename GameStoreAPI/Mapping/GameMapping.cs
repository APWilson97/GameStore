using GameStoreAPI.Contracts;
using GameStoreAPI.Entities;

namespace GameStoreAPI.Mapping;

public static class GameMapping
{
    public static Game ToEntity(this CreateGameDto game)
    {
        return new Game()
        {
            Name = game.Name,
            GenreId = game.GenreId,
            Price = game.Price,
            ReleaseDate = game.ReleaseDate
        };
    }

    public static GameDto ToDto(this Game game)
    {
        return new GameDto(
            game.Id,
            game.Name,
            // ! states that you're not expecting property to be null at any point
            game.Genre!.Name,
            game.Price,
            game.ReleaseDate
        );
    }
}
