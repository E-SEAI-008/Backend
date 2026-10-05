using System.Data;
using System.Diagnostics;

public class BookApp
{
    private readonly BookStorage _storage;
    private List<Book> _books;

    public BookApp(BookStorage storage)
    {
        _storage = storage;
        _books = _storage.Load();
    }

    public void Run()
    {
        var running = true;
        while (running)
        {
            Console.WriteLine("\n ---READING TRACKER---");
            Console.WriteLine("1) Add a book");
            Console.WriteLine("2) Show all books");
            Console.WriteLine("3) Remove a book");
            Console.WriteLine("4) Report");
            Console.WriteLine("5) Update a book's status");
            Console.WriteLine("6) Save & Exit");
            Console.Write("Choose an option: ");

            string? choice = Console.ReadLine();

            switch (choice)
            {
                case "1": AddBook(); break;
                case "2": ShowBooks(); break;
                case "3": RemoveBook(); break;
                case "4": ShowReport(); break;
                case "5": UpdateStatus(); break;
                case "6":
                    _storage.Save(_books); // save before leaving
                    Console.WriteLine("Saved. Goodbye!");
                    running = false;
                    break;
                default:
                    Console.WriteLine("Unknown option, try again");
                    break;
            }
        }
    }
    private void AddBook()
    {
        Console.Write("Title: ");
        string title = Console.ReadLine() ?? "";

        Console.Write("Author: ");
        string author = Console.ReadLine() ?? "";

        var book = new Book(title, author, ReadingStatus.ToRead);
        _books.Add(book);

        Console.WriteLine($"Added: {book.Title} by {book.Author}");
    }

    private void ShowBooks()
    {
        if (_books.Count == 0)
        {
            Console.WriteLine("No books yet");
            return;
        }

        for (int i = 0; i < _books.Count; i++)
        {
            var book = _books[i];
            Console.WriteLine($"{i + 1} {book.Title} by {book.Author} [{book.Status}]");
        }
    }

    private void RemoveBook()
    {
        ShowBooks();// show the book list first
        if (_books.Count == 0) return;

        Console.Write("Which book number to remove? ");

        // read the number the user typed
        if (!int.TryParse(Console.ReadLine(), out int number))
        {
            Console.WriteLine("That's not a valid number.");
            return;
        }

        int index = number - 1;    // convert their 1-based choice to a 0-based index

        // make sure the number actually points to a real book
        if (index < 0 || index >= _books.Count)
        {
            Console.WriteLine("No book with that number.");
            return;
        }

        // remove the book at that position
        var removed = _books[index];
        _books.RemoveAt(index);

        Console.WriteLine($"Removed: {removed.Title}");
    }

    private void ShowReport()
    {
        var finishedCount = _books.Where(b => b.Status == ReadingStatus.Finished).Count();
        Console.WriteLine($"Finished books: {finishedCount}");

        // take toRead books, then sort them alphabetically by title
        var toRead = _books
            .Where(b => b.Status == ReadingStatus.ToRead)
            .OrderBy(b => b.Title);

        Console.Write("\nStill to read:");
        foreach (var book in toRead)
        {
            Console.WriteLine($"- {book.Title}");
        }

        // grab every author, then drop duplicates
        var authors = _books
            .Select(b => b.Author)
            .Distinct();

        Console.WriteLine("\nAuthors on your shelf:");
        foreach (var author in authors)
        {
            Console.WriteLine($"- {author}");
        }
    }

    private void UpdateStatus()
    {
        ShowBooks();               // show the numbered list first
        if (_books.Count == 0) return;

        Console.Write("Which book number? ");

        // read and validate the number (same pattern as RemoveBook)
        if (!int.TryParse(Console.ReadLine(), out int number))
        {
            Console.WriteLine("That's not a valid number.");
            return;
        }

        int index = number - 1;
        if (index < 0 || index >= _books.Count)
        {
            Console.WriteLine("No book with that number.");
            return;
        }

        // ask what the new status should be, and map it to the enum
        Console.WriteLine("New status: 1) To read  2) Reading  3) Finished");
        ReadingStatus newStatus = Console.ReadLine() switch
        {
            "2" => ReadingStatus.Reading,
            "3" => ReadingStatus.Finished,
            _ => ReadingStatus.ToRead    // anything else = ToRead
        };

        // records cannot be changed in place => so we make a COPY with the new status
        _books[index] = _books[index] with { Status = newStatus };

        Console.WriteLine($"Updated: {_books[index].Title} → [{_books[index].Status}]");
    }
}