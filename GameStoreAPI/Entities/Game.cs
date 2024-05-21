namespace GameStoreAPI.Entities;

public class Game
{
    public int Id { get; set; }
    public required string Name { get; set; }
    // Setting 1 to 1 relationship between Game and Genre
    public int GenreId { get; set; }
    // We may decide to populate Genre or not, may be enough to have only Genre ID when reading database through Entity Framework
    public Genre? Genre { get; set; }
    public decimal Price { get; set; }
    public DateOnly ReleaseDate { get; set; }
}
