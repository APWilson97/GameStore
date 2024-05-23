using GameStoreAPI.Contracts;
using GameStoreAPI.Entities;

namespace GameStoreAPI.Mapping;

public static class GenreMapping
{
    public static GenreDto ToDto(this Genre genre)
    {
        return new GenreDto(genre.Id, genre.Name);
    }
}
