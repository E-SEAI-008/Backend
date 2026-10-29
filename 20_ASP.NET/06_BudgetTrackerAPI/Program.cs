using System.Text.Json.Serialization;
using BudgetTrackerApi.Endpoints;
using BudgetTrackerApi.Infrastructure;
using BudgetTrackerApi.Interfaces;
using BudgetTrackerApi.Services;
using BudgetTrackerApi.Utilities;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// REGISTER SERVICES
builder.Services.AddOpenApi();
builder.Services.AddValidation();
builder.Services.AddProblemDetails();
builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.Converters.Add(
        new JsonStringEnumConverter(allowIntegerValues: false));
});

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("Missing default connection string");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(connectionString));


builder.Services.AddScoped<ITransactionService, TransactionService>();

// CREATE THE APP
var app = builder.Build();

app.UseApiExceptionHandling();
await app.CheckDatabaseConnectionAsync();

// ROUTES
app.MapGet("/", () => Results.Ok(new { message = "Budget Tracker API is running" }));
app.MapOpenApi();
app.MapScalarApiReference();
app.MapTransactions();
app.MapReports();

// DOC's
app.Logger.LogInformation("OpenAPI JSON: http://localhost:3000/openapi/v1.json");
app.Logger.LogInformation("Scalar UI: http://localhost:3000/scalar/v1");

app.Run();
