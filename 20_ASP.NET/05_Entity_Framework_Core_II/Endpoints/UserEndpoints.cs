using BlogApi.DTOs.Users;
using BlogApi.Filters;
using BlogApi.Services.Interfaces;

namespace BlogApi.UserEndpoints;

public static class UserEndpoints
{
    public static RouteGroupBuilder MapUsers(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/users").WithTags("Users");

        // GET /users => list all users
        group.MapGet("/", async (IUserService service) =>
        {
            var users = await service.ListAsync();

            var response = users.Select(user => new UserResponseDTO(
                user.Id,
                user.Name,
                user.Email,
                user.Bio,
                user.CreatedAt
            ));

            return Results.Ok(response);
        });

        // GET /users/{id} => get one user
        group.MapGet("/{id:guid}", async (Guid id, IUserService service) =>
        {
            var user = await service.GetAsync(id);

            if (user is null)
            {
                return Results.NotFound();
            }

            var response = new UserResponseDTO(
                user.Id,
                user.Name,
                user.Email,
                user.Bio,
                user.CreatedAt
            );

            return Results.Ok(response);
        })
        .AddEndpointFilter<RejectEmptyIdFilter>();

        // POST /users => create a validated user
        group.MapPost("/", async (CreateUserDTO dto, IUserService service) =>
        {
            var user = await service.CreateAsync(dto.Name, dto.Email, dto.Bio);

            var response = new UserResponseDTO(
                user.Id,
                user.Name,
                user.Email,
                user.Bio,
                user.CreatedAt
            );

            return Results.Created($"/users/{user.Id}", response);
        });

        // PUT /users/{id} => update an existing user
        group.MapPut("/{id:guid}", async (
            Guid id,
            UpdateUserDTO dto,
            IUserService service) =>
        {
            var user = await service.UpdateAsync(
                id,
                dto.Name,
                dto.Email,
                dto.Bio
            );

            if (user is null)
            {
                return Results.NotFound(new
                {
                    message = $"User with ID {id} was not found"
                });
            }

            var response = new UserResponseDTO(
                user.Id,
                user.Name,
                user.Email,
                user.Bio,
                user.CreatedAt
            );

            return Results.Ok(response);
        });

        // DELETE /users/{id}
        group.MapDelete("/{id:guid}", async (Guid id, IUserService service) =>
        {
            var deleted = await service.DeleteAsync(id);

            return deleted
                ? Results.NoContent()
                : Results.NotFound();
        });

        return group;
    }
}