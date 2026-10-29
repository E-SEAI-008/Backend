using BudgetTrackerApi.Infrastructure;

namespace BudgetTrackerApi.Utilities;

public static class DatabaseExtensions
{
    // check the db connection once during application startup
    public static async Task CheckDatabaseConnectionAsync(
        this WebApplication app)
    {
        await using var scope = app.Services.CreateAsyncScope();

        var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        if (!await db.Database.CanConnectAsync())
        {
            throw new InvalidOperationException(
                "Could not connect to Neon."
            );
        }

        app.Logger.LogInformation("Connected to PostgreSQL on Neon");
    }
}