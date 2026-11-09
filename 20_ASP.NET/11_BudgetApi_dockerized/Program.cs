using System.Text.Json.Serialization;
using BudgetApi.Api.Endpoints;
using BudgetApi.Errors;
using BudgetApi.Extensions;
using BudgetApi.Infrastructure;
using BudgetApi.Services.Ef;
using BudgetApi.Services.EF;
using BudgetApi.Services.Interfaces;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// DOC's AND VALIDATION
builder.Services.AddOpenApi();
builder.Services.AddValidation();
builder.Services.AddProblemDetails();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

// STRING <=> ENUM CONVERTER
builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.Converters.Add(
        new JsonStringEnumConverter(allowIntegerValues: false));
});

// DATABASE
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("Missing default connection string");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(connectionString));

// IDENTITY + JWT
builder.Services.AddIdentityServices();
builder.Services.AddJwtAuthentication(builder.Configuration);
builder.Services.AddAuthorization();

// RATE LIMITING
builder.Services.AddApiRateLimiting();

// APP SERVICES
builder.Services.AddScoped<ITransactionService, TransactionService>();
builder.Services.AddScoped<IReportService, ReportService>();
builder.Services.AddScoped<IAuthService, AuthService>();


builder.Services.AddCors(options =>
{
    options.AddPolicy("Frontend", policy =>
        policy.WithOrigins([
            "http://localhost:5173",
            "https://someapp.vercel.app"])
            .AllowAnyHeader()
            .AllowAnyMethod());
    // .WithMethods("GET", "PUT", "POST", "PATCH"));
});

builder.Services.AddHealthChecks()
    .AddCheck("BudgetAPI", () =>
        HealthCheckResult.Healthy("API is running"));

// CREATE THE APP
var app = builder.Build();

if (app.Configuration.GetValue<bool>("ApplyMigrations"))
{
    await using var scope = app.Services.CreateAsyncScope();

    var db = scope.ServiceProvider
        .GetRequiredService<ApplicationDbContext>();

    await db.Database.MigrateAsync();
}

// CHECK DB CONNECTION
if (!app.Environment.IsEnvironment("Testing"))
{
    await app.CheckDatabaseConnectionAsync();
}

// MIDDLEWARES
app.UseExceptionHandler();
app.UseRouting();
app.UseCors("Frontend");
app.UseAuthentication();
app.UseAuthorization();
app.UseRateLimiter();

// ROUTES
app.MapOpenApi();
app.MapScalarApiReference();
app.MapTransactionEndpoints();
app.MapReportEndpoints();
app.MapAuthEndpoints();
app.MapHealthChecks("/health", new HealthCheckOptions
{
    ResponseWriter = async (context, report) =>
    {
        await context.Response.WriteAsJsonAsync(new
        {
            status = report.Status.ToString(),
            checks = report.Entries.Select(entry => new
            {
                name = entry.Key,
                status = entry.Value.Status.ToString(),
                descripiton = entry.Value.Description,
                duration = entry.Value.Duration,
            })
        });
    }
});

// DOC's
app.Logger.LogInformation("OpenAPI JSON: http://localhost:3000/openapi/v1.json");
app.Logger.LogInformation("Scalar UI: http://localhost:3000/scalar/v1");

app.Run();

public partial class Program { }