namespace BlogApi.Models;

public class Post
{
    public Guid Id { get; set; }
    // Foreign key: the author of this post.
    public Guid UserId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public DateTimeOffset? PublishedAt { get; set; }
    // Navigation: the related User object, when loaded
    public User? User { get; set; }
}