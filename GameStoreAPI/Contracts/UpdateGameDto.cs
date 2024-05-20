// Best to specify a new DTO (Data Transfer Object) / Contract for every single new operation as things may change in the future
namespace GameStoreAPI.Contracts;

public record class UpdateGameDto(
    string Name, 
    string Genre, 
    decimal Price, 
    DateOnly ReleaseDate
);