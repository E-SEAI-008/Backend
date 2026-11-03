using System.ComponentModel.DataAnnotations;

namespace BudgetApi.Dtos.Auth;

public record RegisterDto(
    [Required] string Name,
    [Required, EmailAddress] string Email,
    [Required, MinLength(8)] string Password
);