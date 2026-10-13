public static class UserEndpoints
{
    public static RouteGroupBuilder MapUsers(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/users");

        group.MapGet("/", () => new { Name = "John", Role = "Student" });
        group.MapPost("/", (UserRequestDto user) => Results.Ok(user));

        return group;
    }
}