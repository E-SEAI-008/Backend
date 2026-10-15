using BlogApi.Models;
using BlogApi.Services.Interfaces;

namespace BlogApi.Services;

public class InMemoryUserService : IUserService
{
    private readonly List<User> _users = new();
    public User? Get(Guid id) => _users.FirstOrDefault(u => u.Id == id);
    public IReadOnlyList<User> List() => _users;
    public User Create(string name, string email)
    {
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