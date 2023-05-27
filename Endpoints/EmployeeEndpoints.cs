using EmpMgmtWebApi6.Models;
using Microsoft.EntityFrameworkCore;

namespace EmpMgmtWebApi6.Endpoints
{
    public static class EmployeeEndpoints
    {
        public static void MapEmployeeEndpoints(this IEndpointRouteBuilder routes)
        {
            routes.MapGet("/api/Employee", async (MegaMovieDbContext db) =>
            {
                return await db.Employees.ToListAsync();
            })
            .WithName("GetAllEmployees")
            .Produces<List<Employee>>(StatusCodes.Status200OK);

            routes.MapGet("/api/Employee/{id}", async (int Id, MegaMovieDbContext db) =>
            {
                return await db.Employees.FindAsync(Id)
                    is Employee model
                        ? Results.Ok(model)
                        : Results.NotFound();
            })
            .WithName("GetEmployeeById")
            .Produces<Employee>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound);

            routes.MapPut("/api/Employee/{id}", async (int Id, Employee employee, MegaMovieDbContext db) =>
            {
                var foundModel = await db.Employees.FindAsync(Id);

                if (foundModel is null)
                {
                    return Results.NotFound();
                }

                db.Update(employee);

                await db.SaveChangesAsync();

                return Results.NoContent();
            })
            .WithName("UpdateEmployee")
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status204NoContent);

            routes.MapPost("/api/Employee/", async (Employee employee, MegaMovieDbContext db) =>
            {
                db.Employees.Add(employee);
                await db.SaveChangesAsync();
                return Results.Created($"/Employees/{employee.Id}", employee);
            })
            .WithName("CreateEmployee")
            .Produces<Employee>(StatusCodes.Status201Created);


            routes.MapDelete("/api/Employee/{id}", async (int Id, MegaMovieDbContext db) =>
            {
                if (await db.Employees.FindAsync(Id) is Employee employee)
                {
                    db.Employees.Remove(employee);
                    await db.SaveChangesAsync();
                    return Results.Ok(employee);
                }

                return Results.NotFound();
            })
            .WithName("DeleteEmployee")
            .Produces<Employee>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound);
        }
    }
}
