using System.ComponentModel.DataAnnotations;

namespace BlogApi.DTOs.Users;

public record CreateUserDTO(
    [property: Required]
    [property: StringLength(50, MinimumLength =2)]
    string Name,

    [property:Required]
    [property: EmailAddress]
    string Email
);