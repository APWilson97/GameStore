using System.ComponentModel.DataAnnotations;

namespace GameStoreAPI.Contracts;

public record class CreateGameDto( 
    // Data annotiations
    [Required][StringLength(50)] string Name, 
    int GenreId, 
    [Range(1, 100)] decimal Price, 
    DateOnly ReleaseDate
);
