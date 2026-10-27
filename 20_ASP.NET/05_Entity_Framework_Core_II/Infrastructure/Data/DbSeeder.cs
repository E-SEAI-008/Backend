using BlogApi.Models;
using Microsoft.EntityFrameworkCore;

namespace BlogApi.Infrastructure.Data;

public sealed class DbSeeder
{
    private readonly ApplicationDbContext _db;
    public DbSeeder(ApplicationDbContext db) => _db = db;
    public async Task SeedAsync(CancellationToken ct = default)
    {
        var kosta = await _db.Users
            .FirstOrDefaultAsync(u => u.Email == "kosta@mail.com", ct);

        if (kosta is null)
        {
            kosta = new User
            {
                Id = Guid.NewGuid(),
                Name = "Kosta",
                Email = "kosta@mail.com",
                Bio = "Learning EF Core",
                CreatedAt = DateTimeOffset.UtcNow
            };

            _db.Users.Add(kosta);
            await _db.SaveChangesAsync();
        }

        // check weather Kosta already has this sample post:
        var postExists = await _db.Posts.AnyAsync(
            p => p.UserId == kosta.Id && p.Title == "Hello EF Core", ct
        );

        if (!postExists)
        {
            var post = new Post
            {
                Id = Guid.NewGuid(),
                UserId = kosta.Id,
                Title = "Hello EF Core",
                Content = "My first seeded post!",
                PublishedAt = DateTimeOffset.UtcNow
            };

            _db.Posts.Add(post);
            await _db.SaveChangesAsync(ct);
        }
    }
}