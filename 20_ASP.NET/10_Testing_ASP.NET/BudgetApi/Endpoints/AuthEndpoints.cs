using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using BudgetApi.Dtos.Auth;
using BudgetApi.Models;
using BudgetApi.Services.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace BudgetApi.Api.Endpoints;

public static class AuthEndpoints
{
    public static void MapAuthEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/auth").WithTags("Auth");

        // POST /register
        group.MapPost("/register", async (RegisterDto dto, IAuthService auth) =>
        {
            var (result, user) = await auth.RegisterAsync(dto);
            return result.Succeeded
                ? Results.Created($"/auth", user)
                : Results.BadRequest(result.Errors);
        });

        // POST /login
        group.MapPost("/login", async (LoginDto dto, IAuthService auth) =>
        {
            var response = await auth.LoginAsync(dto);
            return response is null ? Results.Unauthorized() : Results.Ok(response);
        }).RequireRateLimiting("login");

        // GET /me
        group.MapGet("/me", async (ClaimsPrincipal user, UserManager<User> users) =>
        {
            var id = user.FindFirstValue(JwtRegisteredClaimNames.Sub);
            var currentUser = id is null ? null : await users.FindByIdAsync(id);

            return currentUser is null
                ? Results.NotFound()
                : Results.Ok(new UserResponseDto(
                    currentUser.Id, currentUser.Name, currentUser.Email!));

        })
        .RequireAuthorization();
        // .RequireRateLimiting("login"); // CHECK
    }
}