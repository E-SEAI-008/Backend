using BlogApi.Models;
using BlogApi.Services.Interfaces;

namespace BlogApi.Services;

public class inMemoryPostService : IPostService
{
    private readonly List<Post> _posts = new();
    public Post? Get(Guid id) =>
        _posts.FirstOrDefault(post => post.Id == id);

    public IReadOnlyList<Post> List() => _posts;

    public Post Create(Guid userId, string title, string content)
    {
        var post = new Post
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            Title = title,
            Content = content,
            PublishedAt = DateTimeOffset.UtcNow
        };

        _posts.Add(post);
        return post;
    }

    public bool Delete(Guid id)
    {
        var post = Get(id);
        if (post is null) return false;

        _posts.Remove(post);
        return true;
    }
}