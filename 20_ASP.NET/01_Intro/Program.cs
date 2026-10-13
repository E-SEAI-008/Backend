var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton<TimeService>();
builder.Services.AddSingleton<GreetingService>();


var app = builder.Build();

app.MapUsers();
app.MapGet("/time", (TimeService ts) => ts.Now());
app.MapGet("/hello", (GreetingService greeting) => greeting.SayHello());


app.Run();