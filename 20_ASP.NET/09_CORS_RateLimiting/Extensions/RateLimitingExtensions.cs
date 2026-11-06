
using System.Security.Claims;
using System.Threading.RateLimiting;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.IdentityModel.JsonWebTokens;

namespace BudgetApi.Extensions;

public static class RateLimitingExtensions
{
    public static IServiceCollection AddApiRateLimiting(
        this IServiceCollection services)
    {
        services.AddRateLimiter(options =>
        {
            options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;

            // login: 5 requests per 30 seconds (shared limit)
            options.AddFixedWindowLimiter("login", limiter =>
            {
                limiter.PermitLimit = 5;
                limiter.Window = TimeSpan.FromSeconds(30);
                limiter.QueueLimit = 0;
            });

            // per-user: each authenticated user gets a separate limit
            options.AddPolicy("per-user", context =>
            {
                var userId = context.User.FindFirstValue(
                    JwtRegisteredClaimNames.Sub) ?? "anonymous";

                return RateLimitPartition.GetFixedWindowLimiter(
                    partitionKey: userId,
                    factory: _ => new FixedWindowRateLimiterOptions
                    {
                        PermitLimit = 5,
                        Window = TimeSpan.FromSeconds(30),
                        QueueLimit = 0,
                        AutoReplenishment = true
                    });
            });

            // clled when a request exceeds its limit
            options.OnRejected = async (context, cancellationToken) =>
            {
                var httpContext = context.HttpContext;
                var path = httpContext.Request.Path;

                httpContext.Response.StatusCode =
                    StatusCodes.Status429TooManyRequests;

                // log rejected login requests
                if (path.StartsWithSegments("/auth/login"))
                {
                    var logger = httpContext.RequestServices
                        .GetRequiredService<ILoggerFactory>()
                        .CreateLogger("RateLimiting");

                    logger.LogWarning(
                        "Rate limit exceeded for {Path}", path);
                }

                // return JSON to the client
                await httpContext.Response.WriteAsJsonAsync(
                    new
                    {
                        status = 429,
                        message = "Too many requests. Please try again later."
                    },
                    cancellationToken);
            };
        });

        return services;
    }
}

