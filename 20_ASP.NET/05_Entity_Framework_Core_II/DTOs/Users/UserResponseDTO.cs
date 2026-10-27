namespace BlogApi.DTOs.Users;

public record UserResponseDTO(
    Guid Id,
    string Name,
    string Email,
    string? Bio,
    DateTimeOffset CreatedAt
);