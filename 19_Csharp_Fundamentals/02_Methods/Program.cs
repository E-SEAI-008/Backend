// METHODS AND PARAMETERS
// void PrintLine()
// {
//     Console.WriteLine("This is a simple message");
// }


// void SayHello(string name)
// {
//     Console.WriteLine($"Hello, {name}");
// }

// int Add(int a, int b)
// {
//     return a + b;
// }

// int Multiply(int a, int b)
// {
//     return a * b;
// }

// PrintLine();
// SayHello("Alice");

// int result = Add(3, 4);
// Console.WriteLine(result);

// Console.Write(Multiply(2, 5));


// ACCESS MODIFIERS?
// var car = new Car();
// car.Drive();

// public class Car
// {
//     public void Drive()
//     {
//         Console.WriteLine("Driving...");
//     }
// }

// var toyota = new Car();
// toyota.Drive();
// public class Car
// {
//     private void StartEngine()
//     {
//         Console.WriteLine("Engine started.");
//     }
//     public void Drive()
//     {
//         StartEngine();
//         Console.WriteLine("Driving...");
//     }
// }

// public class Car
// {
//     private void StartEngine()
//     {
//         Console.WriteLine("Engine started.");
//     }

//     public void Drive()
//     {
//         StartEngine();
//         Console.Write("Driving...2)");
//     }
// }

// public class Person
// {
//     public void UseCar()
//     {
//         var car = new Car();
//         car.Drive();
//         // car.StartEngine(); // ERROR
//     }
// }

// Console.WriteLine(Maths.Square(5));
// public class Maths
// {
//     public static int Square(int x)
//     {
//         return x * x;
//     }
// }

// NAMESPACES
// using Utils;

// Greeter.Welcome();
// Greeter.Welcome(course: "OOP");
// Greeter.Welcome(name: "Earl");
// Greeter.Welcome("Laissa", "ASP.NET");
// Console.WriteLine(Maths.Square(5));

// METHOD OVERLOADING
// Console.WriteLine(Formatter.FormatPrice(12.5));
// Console.WriteLine(Formatter.FormatPrice(12.5, "$"));
// public class Formatter
// {
//     public static string FormatPrice(double amount)
//     {
//         return $"{amount: 0.00} €";
//     }

//     public static string FormatPrice(double amount, string currency)
//     {
//         return $"{currency}{amount:0.00}";
//     }
// }

// VALUE VS REFERENCE TYPES => HOW DATA IS PASSED TO METHODS
// int x = 5;
// Increment(x);

// Console.WriteLine(x);

// void Increment(int number)
// {
//     number++;
//     Console.WriteLine(number);
// }

// int x = 5;
// Console.WriteLine($"BEFORE: {x}");

// Increment(ref x);

// Console.WriteLine($"AFTER: {x}");

// void Increment(ref int number)
// {
//     number++;
// }

// int x = 15;

// IncrementByValue(x);
// Console.WriteLine($"NUMBER OUTSIDE: {x}"); // 15

// IncrementByRef(ref x);
// Console.WriteLine($"NUMBER OUTSIDE WITH REF: {x}"); // 16

// void IncrementByValue(int number)
// {
//     number++;
// }

// void IncrementByRef(ref int number)
// {
//     number++;
// }

// OUT => MULTIPLE RETURN VALUES
// bool success = TryDivide(10, 3, out double result);

// Console.WriteLine($"Success: {success}");
// Console.WriteLine($"Result: {result}");

// bool TryDivide(int a, int b, out double result)
// {
//     // check for invalid division
//     if (b == 0)
//     {
//         result = 0; // out parameter must be assigned
//         return false;
//     }

//     // perform the division
//     result = (double)a / b;
//     return true;
// }

// IN => READ-ONLY REFERENCE

// void Log(in string message)
// {
//     // message = "something";
//     Console.WriteLine(message);
// }

// Log("System started");