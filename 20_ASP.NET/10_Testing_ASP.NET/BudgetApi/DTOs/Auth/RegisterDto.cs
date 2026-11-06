using System.ComponentModel.DataAnnotations;

namespace BudgetApi.Dtos.Auth;

// public record RegisterDto(
//     [Required] string Name,
//     [Required, EmailAddress] string Email,
//     [Required, MinLength(8)] string Password
// );

public record RegisterDto(
    [property: Required] string Name,
    [property: Required, EmailAddress] string Email,
    [property: Required, MinLength(8)] string Password
);