using System.IO;
using System.Text.Json;


// File I/O

File.WriteAllText("example.txt", "Hello there again!");

// using var writer = new StreamWriter("log.txt", append: true);
// writer.WriteLine($"Log entry at {DateTime.Now}");
// writer.WriteLine("something else");

// var writer = new StreamWriter("log2.txt", append: true);
// try
// {
//     writer.WriteLine($"Log entry at {DateTime.Now}");
// }
// finally
// {
//     // dispose even if an error happens
//     writer.Dispose();
// }

// string text = File.ReadAllText("example.txt");
// Console.WriteLine(text);

// using var reader = new StreamReader("log.txt");

// string? line;

// while ((line = reader.ReadLine()) != null)
// {
//     Console.WriteLine(line);
// }

// if (File.Exists("example.txt"))
// {
//     Console.WriteLine("File exists");
// }
// else
// {
//     Console.WriteLine("file not found");
// }

// string folder = "data";

// if (!Directory.Exists(folder))
// {
//     Directory.CreateDirectory(folder);
// }

// string fileName = "records.csv";

// string fullPath = Path.Combine(folder, fileName); // data/records.csv

// using var writer = new StreamWriter(fullPath);
// writer.WriteLine("1. Laissa");
// writer.WriteLine("2. Zekeriye");
// writer.WriteLine("3. Jessie");
// writer.WriteLine("4. Fridtjof");
// writer.WriteLine("5. Samuel");
// writer.WriteLine("6. Kosta");
// writer.WriteLine("7. Earl");
// writer.WriteLine("8. Onur");

// try
// {
//     string content = File.ReadAllText("config.json");
//     Console.Write(content);
// }
// catch (FileNotFoundException)
// {
//     Console.WriteLine("Config file is missing");
// }
// catch (UnauthorizedAccessException)
// {
//     Console.WriteLine("Access denied, check file permissions");
// }
// catch (IOException)
// {
//     Console.WriteLine("AN I/O error occurred while accessing the file");
// }
// catch (Exception ex)
// {
//     Console.WriteLine($"An unexpected error occurred: {ex.Message}");
// }

// ---------------------------------------------------------------------


//  JSON Serialisation

// var person = new
// {
//     Name = "John",
//     Age = 30,
//     Email = "john@example.com",
//     IsStudent = true,
//     Skills = new[] { "C#", "JavaScript", "Python" }
// };

// string json = JsonSerializer.Serialize(person);
// Console.WriteLine(json);

// File.WriteAllText("person.json", json);

// var cust = new Customer { Name = "John", Age = 34 };

// // OBJECT => JSON
// string custJson = JsonSerializer.Serialize(cust);
// Console.Write(custJson);


// // JSON => OBJECT
// var back = JsonSerializer.Deserialize<Customer>(custJson);
// Console.WriteLine($"{back?.Name} ({back?.Age})");

var student = new StudentProfile
{
    Name = "John",
    Age = 30,
    Email = "john@example.com",
    IsEnrolled = true,
    Courses = new List<string> { "C# Fundamentals", "File I/O", "LINQ" }
};

// OBJECT => JSON
// string json = JsonSerializer.Serialize(student);
// File.WriteAllText("student.json", json);

// OBJECT => JSON WITH OPTIONS
var options = new JsonSerializerOptions { WriteIndented = true };
string json = JsonSerializer.Serialize(student, options);
File.WriteAllText("student2.json", json);

// FILE => JSON
string jsonIn = File.ReadAllText("student.json");
var loaded = JsonSerializer.Deserialize<StudentProfile>(jsonIn);

if (loaded is not null)
{
    Console.WriteLine($"{loaded.Name} is enrolled in {loaded.Courses.Count} courses");
}

// PRETTY PRINTING with OPTIONS
// var person = new { Name = "John", Age = 30 };

// string normal = JsonSerializer.Serialize(person);
// Console.WriteLine(normal);

// var options = new JsonSerializerOptions { WriteIndented = true };
// string pretty = JsonSerializer.Serialize(person, options);

// Console.WriteLine(pretty);