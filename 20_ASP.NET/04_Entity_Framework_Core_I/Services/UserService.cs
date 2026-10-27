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
    public async Task<User> CreateAsync(string name, string email)
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
            CreatedAt = DateTimeOffset.UtcNow
        };

        _db.Users.Add(user);
        await _db.SaveChangesAsync();
        return user;

        // try
        // {
        //     await _db.SaveChangesAsync();
        // }
        // catch (DbUpdateException exception) when (
        //     exception.InnerException is SqliteException sqlite &&
        //     sqlite.SqliteExtendedErrorCode == 2067)
        // {
        //     throw new ConflictException(
        //         $"a user with the email '{email}' already exists"
        //     );
        // }

        // return user;
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