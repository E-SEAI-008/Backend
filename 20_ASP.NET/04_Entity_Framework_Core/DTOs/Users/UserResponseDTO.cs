namespace BlogApi.DTOs.Users;

public record UserResponseDTO(
    Guid Id,
    string Name,
    string Email,
    DateTimeOffset CreatedAt
);