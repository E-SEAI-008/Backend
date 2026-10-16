using BlogApi.Errors;
using BlogApi.Models;
using BlogApi.Services.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;

namespace BlogApi.Services;

public class InMemoryUserService : IUserService
{
    private readonly List<User> _users = new();
    public User? Get(Guid id) => _users.FirstOrDefault(u => u.Id == id);
    public IReadOnlyList<User> List() => _users;
    public User Create(string name, string email)
    {

        if (_users.Any(user =>
           string.Equals(user.Email, email, StringComparison.OrdinalIgnoreCase)))
        {
            throw new ConflictException($"a user with email '{email}' already exists.");
        }

        var user = new User
        {
            Id = Guid.NewGuid(),
            Name = name,
            Email = email,
            CreatedAt = DateTimeOffset.UtcNow
        };
        _users.Add(user);
        return user;
    }
    public bool Delete(Guid id)
    {
        var user = Get(id);
        if (user is null) return false;

        _users.Remove(user);
        return true;
    }
}