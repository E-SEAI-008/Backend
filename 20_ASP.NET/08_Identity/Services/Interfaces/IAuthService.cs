using BudgetApi.Dtos.Auth;
using Microsoft.AspNetCore.Identity;

namespace BudgetApi.Services.Interfaces;

public interface IAuthService
{
    Task<(IdentityResult Result, UserResponseDto? User)> RegisterAsync(RegisterDto dto);
    Task<LoginResponseDto?> LoginAsync(LoginDto dto);
}