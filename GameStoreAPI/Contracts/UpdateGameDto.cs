// Best to specify a new DTO (Data Transfer Object) / Contract for every single new operation as things may change in the future
using System.ComponentModel.DataAnnotations;

namespace GameStoreAPI.Contracts;

public record class UpdateGameDto(
    [Required][StringLength(50)] string Name, 
    int GenreId, 
    [Range(1, 100)] decimal Price, 
    DateOnly ReleaseDate
);