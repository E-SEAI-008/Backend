using System.Text.Json;

namespace BlogApi.Filters;

public sealed class LogEndpointFilter : IEndpointFilter
{
    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        WriteIndented = true
    };

    public async ValueTask<object?> InvokeAsync(
        EndpointFilterInvocationContext context,
        EndpointFilterDelegate next)
    {
        var request = context.HttpContext.Request;
        var userAgent = request.Headers["User-Agent"].ToString();

        Console.WriteLine($"Starting: {request.Method} {request.Path}");

        var result = await next(context);

        Directory.CreateDirectory("logs");

        var filePath = Path.Combine("logs", "requests.json");


        var entries = File.Exists(filePath)
            ? JsonSerializer.Deserialize<List<RequestLogEntry>>(
                File.ReadAllText(filePath)) ?? new List<RequestLogEntry>()
            : new List<RequestLogEntry>();

        entries.Add(new RequestLogEntry(
            request.Method,
            request.Path,
            userAgent,
            DateTimeOffset.Now));

        var json = JsonSerializer.Serialize(entries, JsonOptions);
        File.WriteAllText(filePath, json);

        Console.WriteLine("Endpoint handler finished");

        return result;
    }
}


record RequestLogEntry(
    string Method,
    string Path,
    string UserAgent,
    DateTimeOffset Timestamp);