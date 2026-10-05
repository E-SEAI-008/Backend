using System.Text.Json;
using System.Text.Json.Serialization;

public class BookStorage
{
    private readonly string _filePath;
    public BookStorage(string filePath)
    {
        _filePath = filePath;

        string? folder = Path.GetDirectoryName(filePath);
        if (folder != null && !Directory.Exists(folder))
        {
            Directory.CreateDirectory(folder);
        }
    }

    // OBJECT => JSON => FILE
    public void Save(List<Book> books)
    {
        // WriteIndented for prettier JSON, JsonStringEnumConverter is to save Enums as strings
        var options = new JsonSerializerOptions { WriteIndented = true, Converters = { new JsonStringEnumConverter() } };

        string json = JsonSerializer.Serialize(books, options);
        File.WriteAllText(_filePath, json);
    }

    // FILE => JSON => LIST
    public List<Book> Load()
    {
        if (!File.Exists(_filePath))
        {
            return new List<Book>();
        }

        try
        {
            string json = File.ReadAllText(_filePath);
            var books = JsonSerializer.Deserialize<List<Book>>(json);
            return books ?? new List<Book>();
        }
        catch (JsonException)
        {
            Console.WriteLine("Warning: books file is corruppted. Starting fresh");
            return new List<Book>();
        }
        catch (IOException ex)
        {
            Console.WriteLine($"Could not read the file: {ex.Message}");
            return new List<Book>();
        }
    }
}