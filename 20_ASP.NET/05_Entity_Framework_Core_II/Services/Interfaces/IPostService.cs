using BlogApi.Models;

namespace BlogApi.Services.Interfaces;

public interface IPostService
{
    Task<Post?> GetAsync(Guid id);
    Task<IReadOnlyList<Post>> ListAsync(string? search = null);
    Task<Post> CreateAsync(Guid userId, string title, string content);
    Task<Post?> UpdateAsync(Guid id, string? title, string? content);
    Task<bool> DeleteAsync(Guid id);
}