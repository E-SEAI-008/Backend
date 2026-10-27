using BlogApi.Models;

namespace BlogApi.Services.Interfaces;

public interface IUserService
{
    Task<User?> GetAsync(Guid id);
    Task<IReadOnlyList<User>> ListAsync();
    Task<User> CreateAsync(string name, string email, string? bio = null);
    Task<User?> UpdateAsync(Guid id, string? name, string? email, string? bio);
    Task<bool> DeleteAsync(Guid id);
}