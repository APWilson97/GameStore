using System.ComponentModel.DataAnnotations;

namespace GameStoreAPI.Contracts;

public record class CreateGameDto( 
    // Data annotiations
    [Required][StringLength(50)] string Name, 
    [Required][StringLength(20)] string Genre, 
    [Range(1, 100)] decimal Price, 
    DateOnly ReleaseDate
);
