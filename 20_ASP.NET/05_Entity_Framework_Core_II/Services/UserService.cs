using BlogApi.Errors;
using BlogApi.Infrastructure;
using BlogApi.Models;
using BlogApi.Services.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace BlogApi.Services;

public sealed class UserService : IUserService
{
    private readonly ApplicationDbContext _db;
    public UserService(ApplicationDbContext db) => _db = db;
    public async Task<User?> GetAsync(Guid id)
        => await _db.Users.FindAsync(id);
    public async Task<IReadOnlyList<User>> ListAsync()
        => await _db.Users.ToListAsync();
    public async Task<User> CreateAsync(string name, string email, string? bio = null)
    {
        if (await _db.Users.AnyAsync(user => user.Email == email))
        {
            throw new ConflictException(
                $"A user with email '{email}' already exists");
        }

        var user = new User
        {
            Id = Guid.NewGuid(),
            Name = name,
            Email = email,
            Bio = bio,
            CreatedAt = DateTimeOffset.UtcNow
        };

        _db.Users.Add(user);
        await _db.SaveChangesAsync();
        return user;
    }

    public async Task<User?> UpdateAsync(
    Guid id, string? name, string? email, string? bio)
    {
        var user = await _db.Users.FindAsync(id);

        if (user is null)
            return null;

        if (name is not null)
            user.Name = name;

        if (email is not null)
        {
            var emailExists = await _db.Users.AnyAsync(
                u => u.Email == email && u.Id != id);

            if (emailExists)
                throw new ConflictException("This email is already in use.");

            user.Email = email;
        }

        if (bio is not null)
            user.Bio = bio;

        await _db.SaveChangesAsync();

        return user;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var user = await _db.Users.FindAsync(id);
        if (user is null) return false;

        _db.Users.Remove(user);
        await _db.SaveChangesAsync();
        return true;
    }

}