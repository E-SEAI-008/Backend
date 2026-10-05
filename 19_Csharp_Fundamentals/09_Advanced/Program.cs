// int x = 5;

// Increment(x); // pass by value => X stay 5
// Console.WriteLine(x);

// IncrementByRef(ref x); // pass by ref => X change
// Console.WriteLine(x);

// void Increment(int number)
// {
//     number++;
// }

// void IncrementByRef(ref int number)
// {
//     number++;
// }

// VALUE Type (int) => copied
// int a = 5;
// int b = a;
// b++;
// Console.WriteLine(a); // 5

// // REF type (class) => shared
// var box1 = new Box { Count = 5 };
// var box2 = box1;
// box2.Count++;
// Console.WriteLine(box1.Count);


// public class Box
// {
//     public int Count { get; set; }
// }

// ENUMS

// DayOfWeek today = DayOfWeek.Monday;

// if (today == DayOfWeek.Monday)
// {
//     Console.WriteLine("Start of the week");
// }

// switch (today)
// {
//     case DayOfWeek.Saturday:
//     case DayOfWeek.Sunday:
//         Console.WriteLine("WEEKEND");
//         break;
//     default:
//         Console.WriteLine("WEEKDAY");
//         break;
// }

// OrderStatus status = OrderStatus.Shipped;

// if (status == OrderStatus.Shipped)
// {
//     Console.WriteLine("the order is on its way");
// }

// enum OrderStatus
// {
//     Pending,
//     Shipped,
//     Delivered
// }

// DayOfWeek today = DayOfWeek.Monday;

// switch (today)
// {
//     case DayOfWeek.Saturday or DayOfWeek.Sunday:
//         Console.WriteLine("WEEKEND");
//         break;
//     default:
//         Console.WriteLine("WEEKDAY");
//         break;
// }

// STRUCT
// var p1 = new Point { X = 5, Y = 10 };
// var p2 = p1; // COPY
// p2.X = 99;

// Console.WriteLine(p1.X);

// struct Point
// {
//     public int X;
//     public int Y;
// }

// var c1 = new Coordinate(3, 4);
// // c1.X = 10000;

// Console.WriteLine($"{c1.X}, {c1.Y}"); // 3,4

// var s1 = new Student("E-SEAI#08", "John", 2);
// var s2 = s1; // REF!
// s2.Promote();

// Console.WriteLine(s1.Year);

// RECORDS
// var r1 = new StudentRecord("E-SEAI#08", "John", 2);
// var r2 = new StudentRecord("E-SEAI#08", "John", 2);

// Console.WriteLine(r1 == r2);

// var r3 = r1 with { Year = 3 };

// Console.WriteLine(r1.Year);
// Console.WriteLine(r3.Year);

// public record StudentRecord(string Id, string Name, int Year);

public class EmailNotification : INotification
{
    public void Send()
    {
        Console.WriteLine("Sending an email...");
    }
}

interface INotification
{
    void Send();
}