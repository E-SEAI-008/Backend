using System.Security.Claims;
using System.Text;
using BudgetApi.Dtos.Auth;
using BudgetApi.Models;
using BudgetApi.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;

namespace BudgetApi.Services.Ef;

public class AuthService : IAuthService
{
    private readonly UserManager<User> _userManager;
    private readonly IConfiguration _configuration;

    public AuthService(UserManager<User> userManager, IConfiguration configuration)
    {
        _userManager = userManager;
        _configuration = configuration;
    }

    public async Task<(IdentityResult Result, UserResponseDto? User)> RegisterAsync(RegisterDto dto)
    {
        // Identity requires a UserName; we use the email for both.
        var user = new User
        {
            Name = dto.Name,
            UserName = dto.Email,
            Email = dto.Email
        };

        // Identity hashes the password before saving the user.
        var result = await _userManager.CreateAsync(user, dto.Password);
        if (!result.Succeeded) return (result, null);

        return (result, new UserResponseDto(user.Id, user.Name, user.Email!));
    }

    public async Task<LoginResponseDto?> LoginAsync(LoginDto dto)
    {
        var user = await _userManager.FindByEmailAsync(dto.Email);

        // If the user doesn't exist OR the password is incorrect, login fails.
        if (user is null || !await _userManager.CheckPasswordAsync(user, dto.Password))
            return null;

        var expiresAt = DateTimeOffset.UtcNow.AddMinutes(
            _configuration.GetValue("Jwt:ExpiryMinutes", 60));

        var key = _configuration["Jwt:Key"]
            ?? throw new InvalidOperationException("Jwt:Key is missing.");

        var token = new JsonWebTokenHandler().CreateToken(new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity([
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                // new Claim(JwtRegisteredClaimNames.Email, user.Email!)
            ]),

            Issuer = _configuration["Jwt:Issuer"],
            Audience = _configuration["Jwt:Audience"],
            Expires = expiresAt.UtcDateTime,

            // !!!
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)),
                SecurityAlgorithms.HmacSha256)
        });

        return new LoginResponseDto(token, expiresAt);
    }
}