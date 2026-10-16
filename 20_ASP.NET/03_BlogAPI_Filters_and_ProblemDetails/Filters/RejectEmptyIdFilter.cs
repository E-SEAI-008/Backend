namespace BlogApi.Filters;

public sealed class RejectEmptyIdFilter : IEndpointFilter
{
    public async ValueTask<object?> InvokeAsync(
        EndpointFilterInvocationContext context,
        EndpointFilterDelegate next)
    {
        var id = context.Arguments.OfType<Guid>().FirstOrDefault();

        if (id == Guid.Empty)
        {
            return Results.Problem(
                statusCode: StatusCodes.Status400BadRequest,
                title: "Invalid ID",
                detail: "The ID cannot be empty"
            );
        }
        return await next(context);
    }
}