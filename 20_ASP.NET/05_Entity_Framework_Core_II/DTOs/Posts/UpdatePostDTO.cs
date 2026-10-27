using System.ComponentModel.DataAnnotations;

namespace BlogApi.DTOs.Posts;

public record UpdatePostDTO(
    [property: StringLength(50, MinimumLength = 5)]
    string? Title,

    [property: StringLength(10_000, MinimumLength = 5)]
    string? Content
);