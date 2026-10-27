namespace BlogApi.Errors;

public sealed class ConflictException : Exception
{
    public ConflictException(string message) : base(message) { }
}