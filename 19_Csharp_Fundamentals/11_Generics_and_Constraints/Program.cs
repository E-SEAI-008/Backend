// T Identity<T>(T value)
// {
//     return value;
// }

// int number = Identity(5);
// string text = Identity("Hello");

// List<Book> _books; where T = BOOK
// JsonSerializer.Deserialize<List<Book>>(json);

// var numberBox = new Box<int> { Value = 10 };
// var textBox = new Box<string> { Value = "Hello" };

// Console.WriteLine(numberBox.Value);
// Console.WriteLine(textBox.Value);

// void Swap<T>(ref T a, ref T b)
// {
//     (a, b) = (b, a);
// }

// int x = 1;
// int y = 2;
// Swap(ref x, ref y);
// Console.WriteLine($"{x}, {y}");
// Console.Write(x);

// string a = "hello";
// string b = "world";
// Swap(ref a, ref b);
// Console.WriteLine($"{a}, {b}");

// var repo = new Repository<Book>();
// repo.Save(new Book { Id = 1, Title = "C#" });

// var movie = new Repository<Movie>();

// public class Movie : Entity
// {
//     public string Name { get; set; } = string.Empty;
// }