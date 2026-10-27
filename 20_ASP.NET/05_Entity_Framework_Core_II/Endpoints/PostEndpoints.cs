using BlogApi.DTOs.Posts;
using BlogApi.Services.Interfaces;

namespace BlogApi.PostEndpoints;

public static class PostEndpoints
{
    public static RouteGroupBuilder MapPosts(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/posts").WithTags("Posts");

        // GET /posts => list all posts
        group.MapGet("/", async (string? search, IPostService service) =>
        {
            var posts = await service.ListAsync(search);

            var response = posts.Select(post => new PostResponseDTO(
                post.Id,
                post.UserId,
                post.Title,
                post.Content,
                post.PublishedAt
            ));

            return Results.Ok(response);
        });

        // GET /posts/{id} => get one post
        group.MapGet("/{id:guid}", async (Guid id, IPostService service) =>
        {
            var post = await service.GetAsync(id);

            if (post is null)
            {
                return Results.NotFound(new
                {
                    message = $"Post with id: {id} was not found"
                });
            }

            var response = new PostResponseDTO(
                post.Id,
                post.UserId,
                post.Title,
                post.Content,
                post.PublishedAt
            );

            return Results.Ok(response);
        });

        // POST /posts => create a validated post
        group.MapPost("/", async (CreatePostDTO dto, IPostService service) =>
        {
            var post = await service.CreateAsync(
                dto.UserId!.Value,
                dto.Title,
                dto.Content
            );

            var response = new PostResponseDTO(
                post.Id,
                post.UserId,
                post.Title,
                post.Content,
                post.PublishedAt
            );

            return Results.Created($"/posts/{post.Id}", response);
        });

        // PUT /posts/{id} => update an existing post
        group.MapPut("/{id:guid}", async (
            Guid id,
            UpdatePostDTO dto,
            IPostService service) =>
        {
            var post = await service.UpdateAsync(
                id,
                dto.Title,
                dto.Content
            );

            if (post is null)
            {
                return Results.NotFound(new
                {
                    message = $"Post with ID {id} was not found"
                });
            }

            var response = new PostResponseDTO(
                post.Id,
                post.UserId,
                post.Title,
                post.Content,
                post.PublishedAt
            );

            return Results.Ok(response);
        });

        // DELETE /posts/{id}
        group.MapDelete("/{id:guid}", async (Guid id, IPostService service) =>
        {
            var deleted = await service.DeleteAsync(id);

            return deleted
                ? Results.NoContent()
                : Results.NotFound(new
                {
                    message = $"Post with ID {id} was not found"
                });
        });

        return group;
    }
}