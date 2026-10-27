using BlogApi.Infrastructure;
using BlogApi.Models;
using BlogApi.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BlogApi.Services;

public sealed class PostService : IPostService
{
    private readonly ApplicationDbContext _db;
    public PostService(ApplicationDbContext db) => _db = db;

    public async Task<Post?> GetAsync(Guid id)
        => await _db.Posts.FindAsync(id);

    // public async Task<IReadOnlyList<Post>> ListAsync()
    // {
    //     var posts = await _db.Posts.ToListAsync();

    //     return posts
    //         .OrderByDescending(p => p.PublishedAt)
    //         .ToList();
    // }
    public async Task<IReadOnlyList<Post>> ListAsync(string? search = null)
    {
        var query = _db.Posts.AsQueryable();

        if (!string.IsNullOrWhiteSpace(search))
        {
            var term = search.Trim();
            query = query.Where(p => p.Title.Contains(term));
        }

        var posts = await query.ToListAsync();

        return posts
            .OrderByDescending(p => p.PublishedAt)
            .ToList();
    }
    public async Task<Post> CreateAsync(Guid userId, string title, string content)
    {
        // a post must belong to an existing user
        var userExists = await _db.Users.AnyAsync(user => user.Id == userId);

        if (!userExists)
        {
            throw new BadHttpRequestException(
                $"User with ID '{userId}' was not found",
                StatusCodes.Status400BadRequest);
        }

        var post = new Post
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            Title = title,
            Content = content,
            PublishedAt = DateTimeOffset.UtcNow
        };

        _db.Posts.Add(post);
        await _db.SaveChangesAsync();
        return post;
    }
    public async Task<Post?> UpdateAsync(
     Guid id, string? title, string? content)
    {
        var post = await _db.Posts.FindAsync(id);

        if (post is null)
        {
            return null;
        }

        if (title is not null)
        {
            post.Title = title;
        }

        if (content is not null)
        {
            post.Content = content;
        }

        await _db.SaveChangesAsync();

        return post;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var post = await _db.Posts.FindAsync(id);
        if (post is null) return false;

        _db.Posts.Remove(post);
        await _db.SaveChangesAsync();
        return true;
    }
}