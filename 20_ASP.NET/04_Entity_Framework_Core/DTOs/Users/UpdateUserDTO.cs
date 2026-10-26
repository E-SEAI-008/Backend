using System.ComponentModel.DataAnnotations;

namespace BlogApi.DTOs.Users;

public record UpdateUserDTO(
    [property: StringLength(50, MinimumLength =2)]
    string? Name,

    [property: EmailAddress]
    string? Email
);