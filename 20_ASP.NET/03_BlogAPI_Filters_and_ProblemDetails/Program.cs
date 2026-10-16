using BlogApi.Errors;
using BlogApi.PostEndpoints;
using BlogApi.Services;
using BlogApi.Services.Interfaces;
using BlogApi.UserEndpoints;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Enable .NET 10 built-in Validation
builder.Services.AddOpenApi();
builder.Services.AddValidation();
builder.Services.AddProblemDetails();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

// register services (interface => implementation)
builder.Services.AddSingleton<IUserService, InMemoryUserService>();
builder.Services.AddSingleton<IPostService, inMemoryPostService>();


var app = builder.Build();

app.UseExceptionHandler();
app.UseStatusCodePages();

app.MapOpenApi();
app.MapScalarApiReference();

app.MapUsers();
app.MapPosts();

app.Logger.LogInformation("OpenAPI JSON: http://localhost:5188/openapi/v1.json");
app.Logger.LogInformation("Scalar UI: http://localhost:5188/scalar/v1");

app.Run();
