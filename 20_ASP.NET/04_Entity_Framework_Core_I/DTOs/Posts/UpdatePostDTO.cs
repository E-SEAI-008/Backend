using System.ComponentModel.DataAnnotations;

namespace BlogApi.DTOs.Posts;

public record UpdatePostDTO(
    [property: StringLength(40, MinimumLength =10 )]
    string? Title,

    [property: StringLength(10_000, MinimumLength =10)]
    string? Content
);