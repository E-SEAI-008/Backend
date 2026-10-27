namespace BlogApi.DTOs.Posts;

public record PostResponseDTO(
    Guid Id,
    Guid UserId,
    string Title,
    string Content,
    DateTimeOffset? PublishedAt
);