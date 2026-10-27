using System.ComponentModel.DataAnnotations;

namespace BlogApi.DTOs.Posts;

public record CreatePostDTO(
    [property:Required]
    Guid? UserId,

    [property:Required]
    [property: StringLength(50, MinimumLength =10)]
    string Title,

    [property:Required]
    [property: StringLength(10_000, MinimumLength =10)]
    string Content
);