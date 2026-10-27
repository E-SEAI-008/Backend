using BlogApi.Errors;
using BlogApi.Infrastructure;
using BlogApi.Infrastructure.Data;
using BlogApi.PostEndpoints;
using BlogApi.Services;
using BlogApi.Services.Interfaces;
using BlogApi.UserEndpoints;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Enable .NET 10 built-in Validation
builder.Services.AddOpenApi();
builder.Services.AddValidation();
builder.Services.AddProblemDetails();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// register services (interface => implementation)
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IPostService, PostService>();
builder.Services.AddScoped<DbSeeder>();

var app = builder.Build();

app.UseExceptionHandler();
app.UseStatusCodePages();

app.MapOpenApi();
app.MapScalarApiReference();

app.MapUsers();
app.MapPosts();

// SEED
if (app.Environment.IsDevelopment())
{
    using var scope = app.Services.CreateScope();
    var seeder = scope.ServiceProvider.GetRequiredService<DbSeeder>();
    await seeder.SeedAsync();
}


app.Logger.LogInformation("OpenAPI JSON: http://localhost:5188/openapi/v1.json");
app.Logger.LogInformation("Scalar UI: http://localhost:5188/scalar/v1");

app.Run();