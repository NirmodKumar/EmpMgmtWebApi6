using EmpMgmtWebApi6.Models;
using Microsoft.EntityFrameworkCore;
namespace EmpMgmtWebApi6.Endpoints;

public static class MovieEndpoints
{
    public static void MapMovieEndpoints(this IEndpointRouteBuilder routes)
    {
        routes.MapGet("/api/Movie", async (MegaMovieDbContext db) =>
        {
            return await db.Movies.ToListAsync();
        })
        .WithName("GetAllMovies")
        .Produces<List<Movie>>(StatusCodes.Status200OK);

        routes.MapGet("/api/Movie/{id}", async (int Id, MegaMovieDbContext db) =>
        {
            return await db.Movies.FindAsync(Id)
                is Movie model
                    ? Results.Ok(model)
                    : Results.NotFound();
        })
        .WithName("GetMovieById")
        .Produces<Movie>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);

        routes.MapPut("/api/Movie/{id}", async (int Id, Movie movie, MegaMovieDbContext db) =>
        {
            var foundModel = await db.Movies.FindAsync(Id);

            if (foundModel is null)
            {
                return Results.NotFound();
            }

            db.Update(movie);

            await db.SaveChangesAsync();

            return Results.NoContent();
        })
        .WithName("UpdateMovie")
        .Produces(StatusCodes.Status404NotFound)
        .Produces(StatusCodes.Status204NoContent);

        routes.MapPost("/api/Movie/", async (Movie movie, MegaMovieDbContext db) =>
        {
            db.Movies.Add(movie);
            await db.SaveChangesAsync();
            return Results.Created($"/Movies/{movie.Id}", movie);
        })
        .WithName("CreateMovie")
        .Produces<Movie>(StatusCodes.Status201Created);

        routes.MapDelete("/api/Movie/{id}", async (int Id, MegaMovieDbContext db) =>
        {
            if (await db.Movies.FindAsync(Id) is Movie movie)
            {
                db.Movies.Remove(movie);
                await db.SaveChangesAsync();
                return Results.Ok(movie);
            }

            return Results.NotFound();
        })
        .WithName("DeleteMovie")
        .Produces<Movie>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);
    }
}
