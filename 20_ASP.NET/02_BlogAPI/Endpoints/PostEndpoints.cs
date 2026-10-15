using BlogApi.DTOs.Posts;
using BlogApi.Services.Interfaces;

namespace BlogApi.PostEndpoints;

public static class PostEndpoints
{
    public static RouteGroupBuilder MapPosts(this IEndpointRouteBuilder rotues)
    {
        var group = rotues.MapGroup("/posts");

        // GET /posts => list all posts
        group.MapGet("/", (IPostService service) =>
        {
            var posts = service.List()
            .Select(post => new PostResponseDTO(
                post.Id,
                post.UserId,
                post.Title,
                post.Content,
                post.PublishedAt
            ));
            return Results.Ok(posts);
        });

        group.MapGet("/{id:guid}", (Guid id, IPostService service) =>
        {
            var post = service.Get(id);

            if (post is null)
            {
                return Results.NotFound(new { message = $"Post with id: {id} was not found" });
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

        // POST => create a validated post
        group.MapPost("/", (CreatePostDTO dto, IPostService service) =>
        {
            var post = service.Create(
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

        // DELETE 
        group.MapDelete("/{id:guid}", (Guid id, IPostService service) =>
        {
            return service.Delete(id)
            ? Results.NoContent()
            // : Results.NotFound();
            : Results.NotFound(new { message = $"Post with ID {id} was not found" });
        });
        return group;
    }
}