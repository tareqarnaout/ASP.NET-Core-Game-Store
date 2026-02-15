namespace GameStore.Dtos;

public record class GameDto(// dtos are structs immutable
    int Id,
    string Name,
    string Genre,
    decimal Price,
    DateOnly ReleaseDate
);
