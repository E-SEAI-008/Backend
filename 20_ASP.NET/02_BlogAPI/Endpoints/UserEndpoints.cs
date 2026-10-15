using BlogApi.DTOs.Users;
using BlogApi.Services.Interfaces;

namespace BlogApi.UserEndpoints;

public static class UserEndpoints
{
    public static RouteGroupBuilder MapUsers(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/users");

        // GET /users => listAll
        group.MapGet("/", (IUserService service) =>
        {
            var users = service.List()
            .Select(u => new UserResponseDTO(u.Id, u.Name, u.Email, u.CreatedAt));
            return Results.Ok(users);
        });

        // GET /users/{id} => one user by it's ID, or 404
        group.MapGet("/{id:guid}", (Guid id, IUserService service) =>
        {
            var user = service.Get(id);
            if (user is null) return Results.NotFound();

            // map model => response DTO
            var response = new UserResponseDTO(user.Id, user.Name, user.Email, user.CreatedAt);
            return Results.Ok(response);
        });

        // POST /users (create)
        group.MapPost("/", (CreateUserDTO dto, IUserService service) =>
        {
            var user = service.Create(dto.Name, dto.Email);
            var response = new UserResponseDTO(user.Id, user.Name, user.Email, user.CreatedAt);

            return Results.Created($"/users/{user.Id}", response);
        });

        // DELETE /userrs{id}
        group.MapDelete("/{id:guid}", (Guid id, IUserService service) =>
        {
            return service.Delete(id) ? Results.NoContent() : Results.NotFound();
        });

        return group;
    }
}