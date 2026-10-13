// var builder = WebApplication.CreateBuilder(args);
// var app = builder.Build();

// // USERS
// app.MapGet("/users", () => new { Name = "John", Role = "Student" });
// app.MapGet("/users/{id}", (int id) => $"User with {id}");
// app.MapPost("/users", (UserRequestDto user) => $"Created user {user.Name}");

// // POSTS
// app.MapGet("/posts", () => new { Title = "Something" });
// app.MapGet("/posts/{id}", (int id) => $"Post with {id}");
// app.MapPost("/posts", (PostRequestDto post) => $"Created post {post.Title}");


// // {id:int} enforces that id must be an int
// app.MapGet("/something/{id:int}", (int id) => $"Something with id: {id}");

// // ASP.NET binds term automatically from the query string
// app.MapGet("/posts/search", (string term) => $"You searched for: {term}");

// // complex types are automatically deserialised from JSON:
// app.MapPost("/result", (UserRequestDto user) =>
// {
//     return Results.Created(
//         "/result/1",
//         new { Message = $"Welcome, {user.Name}!" }
//     );
// });

// app.Run();

// record UserRequestDto(string Name);
// record PostRequestDto(string Title);