using System;
using GameStore.Dtos;

namespace GameStore.GameEndPoints;

public static class GameEndPoints
{
    const string EndPoint = "GetGame";
    private static readonly List<GameDto> games = [
        new (1, "Withcer", "Action", 20.0M,new DateOnly(2026, 2, 1)),
        new (2, "mario", "idk", 10.0M, new DateOnly(2026, 4, 1)),
    ];

    // this WebApplication tells c# that this function is an extension of webaplpication so we can call it staticly 
    public static void MapGameEndPoints(this WebApplication app)
    {
        var group = app.MapGroup("/games");
        

        group.MapGet("/", () => games);
        // .net is smart it auto matches  id in url is for the function 
        group.MapGet("/{id}", (int id) =>
        {
            var game = games.Find(game => game.Id == id);
            return game is null ? Results.NotFound() : Results.Ok(game);
        }).WithName(EndPoint);

        // C# is smart it auto maps the form submission with temp
        group.MapPost("/", (CreateGameDto temp) =>
        {
            GameDto game = new GameDto(games.Count + 1, temp.Name, temp.Genre, temp.Price, temp.ReleaseDate);
            games.Add(game);
            return Results.CreatedAtRoute(
                EndPoint, // 1. Route name to use
                new { id = game.Id },  // 2. Route values (parameters)
                game);// 3. Response body
        });

        group.MapPut("/{id}", (int id, CreateGameDto temp) =>s
        {
            var index = games.FindIndex(game => id == game.Id);
            if (index == -1)
            {
                return Results.NotFound();
            }

            games[index] = new(id, temp.Name, temp.Genre, temp.Price, temp.ReleaseDate);
            return Results.NoContent();
        });


        group.MapDelete("/{id}", (int id) =>
        {
            games.RemoveAll(game => id == game.Id);
            return Results.NoContent();
        });


    }
}
