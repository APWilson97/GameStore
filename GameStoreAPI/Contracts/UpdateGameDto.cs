// Best to specify a new DTO (Data Transfer Object) / Contract for every single new operation as things may change in the future
using System.ComponentModel.DataAnnotations;

namespace GameStoreAPI.Contracts;

public record class UpdateGameDto(
    [Required][StringLength(50)] string Name, 
    [Required][StringLength(20)] string Genre, 
    [Range(1, 100)] decimal Price, 
    DateOnly ReleaseDate
);