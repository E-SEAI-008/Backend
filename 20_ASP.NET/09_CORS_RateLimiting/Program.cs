using System.Text.Json.Serialization;
using BudgetApi.Api.Endpoints;
using BudgetApi.Errors;
using BudgetApi.Extensions;
using BudgetApi.Infrastructure;
using BudgetApi.Services.Ef;
using BudgetApi.Services.EF;
using BudgetApi.Services.Interfaces;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore;
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


// CREATE THE APP
var app = builder.Build();

// CHECK DB CONNECTION
await app.CheckDatabaseConnectionAsync();

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

// DOC's
app.Logger.LogInformation("OpenAPI JSON: http://localhost:3000/openapi/v1.json");
app.Logger.LogInformation("Scalar UI: http://localhost:3000/scalar/v1");

app.Run();
