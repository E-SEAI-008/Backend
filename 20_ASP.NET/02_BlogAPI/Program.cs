using BlogApi.PostEndpoints;
using BlogApi.Services;
using BlogApi.Services.Interfaces;
using BlogApi.UserEndpoints;

var builder = WebApplication.CreateBuilder(args);

// Enable .NET 10 built-in Validation
builder.Services.AddValidation();

// register services (interface => implementation)
builder.Services.AddSingleton<IUserService, InMemoryUserService>();
builder.Services.AddSingleton<IPostService, inMemoryPostService>();


var app = builder.Build();

app.MapUsers();
app.MapPosts();

app.Run();
