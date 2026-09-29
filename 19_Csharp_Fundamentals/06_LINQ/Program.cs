// var numbers = new List<int> { 1, 2, 3, 4, 5, 6 };
// var evens = new List<int>();

// foreach (var n in numbers)
// {
//     if (n % 2 == 0)
//     {
//         evens.Add(n);
//     }
// }

// foreach (var e in evens)
// {
//     Console.WriteLine(e);
// }

// WHERE => FILTER
// var numbers = new List<int> { 1, 2, 3, 4, 5, 6 };
// var evens = numbers.Where(n => n % 2 == 0);

// foreach (var e in evens)
// {
//     Console.WriteLine(e);
// }

// var array = new int[] { 10, 15, 20, 25 };
// var result = array.Where(n => n > 15);

// foreach (var n in result)
// {
//     Console.WriteLine(n);
// }

var products = new List<Product>
{
    new Product {Name = "Mug", Category="Kitchen", Price= 6.50m, InStock= true},
    new Product {Name = "Notebook", Category="Stationery", Price= 3.20m, InStock= true},
    new Product {Name = "Pen", Category="Stationery", Price= 1.80m, InStock= false},
    new Product {Name = "Tea Kettle", Category="Kitchen", Price= 24.90m, InStock= true},
    new Product {Name = "Lamp", Category="Home", Price= 18.00m, InStock= false},
};


// WHERE => FILTER in JS
// var inStock = products.Where(p => p.InStock);

// foreach (var p in inStock)
// {
//     Console.WriteLine($"{p.Name} is in stock");
// }

// combine conditions with && 
// var inStockStationery = products.Where(p => p.Category == "Stationery" && p.InStock);

// foreach (var p in inStockStationery)
// {
//     Console.WriteLine($"{p.Name} {p.Price:0.00} €");
// }

// SELECT => MAP in JS
// var numbers = new List<int> { 1, 2, 3 };
// var doubled = numbers.Select(n => n * 2);

// foreach (var n in doubled)
// {
//     Console.WriteLine(n);
// }

// var names = products.Select(p => p.Name);

// foreach (var name in names)
// {
//     Console.WriteLine(name);
// }

// shorthand:
// products.Select(p => p.Name)
//     .ToList()
//     .ForEach(p => Console.WriteLine(p));

// var display = products.Select(p => new
// {
//     p.Name,
//     PriceWithVat = p.Price * 1.20m
// });

// foreach (var d in display)
// {
//     Console.WriteLine($"{d.Name} => {d.PriceWithVat:0.00} €");
// }

// ORDERBY (ascending) => OrderByDescending => ThenBy => tie breaker
// var byPrice = products.OrderBy(p => p.Price);
// var expensiveFirst = products.OrderByDescending(p => p.Price);

// foreach (var p in expensiveFirst)
// {
//     Console.WriteLine($"{p.Name} {p.Price:0.00} €");
// }

// var byCategoryThenPrice = products
//     .OrderBy(p => p.Category)          // main sort
//     .ThenBy(p => p.Price);             // tie-breaker within the same category

// foreach (var p in byCategoryThenPrice)
// {
//     Console.WriteLine($"{p.Category} - {p.Name} {p.Price:0.00} €");
// }

// var topKitchenNames = products
//     .Where(p => p.Category == "Kitchen" && p.InStock)       // 1. Filter
//     .OrderByDescending(p => p.Price)                       // 2. Sort
//     .Select(p => p.Name)                                  // 3. Shape
//     .Take(1);                                            // 4. Limit (opt)

// foreach (var name in topKitchenNames)
// {
//     Console.WriteLine(name);
// }

// var mug = products.Find(p => p.Name == "Mug");


// EXECUTION
// var numbers = new List<int> { 1, 2, 3 };

// var query = numbers.Where(n => n > 1); // no query

// numbers.Add(4); // change the source

// foreach (var n in query) // <= the quer runs HERE, now!
// {
//     Console.WriteLine(n);
// }

// var numbers = new List<int> { 1, 2, 3 };

// var query = numbers.Where(n => n > 1).ToList(); // runs NOW!

// numbers.Add(4); // change the source

// foreach (var n in query)
// {
//     Console.WriteLine(n);
// }

// MORE PROJECTIONS 
// SelectMany

// var students = new List<Student>
// {
//     new Student {Name = "John", Subjects = new() {"Math", "Physics"}},
//     new Student {Name = "Jane", Subjects = new() {"History"}},
//     new Student {Name = "Bob", Subjects = new() {"Math", "Biology"}},
// };

// var allSubjects = students.SelectMany(s => s.Subjects);
// // .Distinct(); // remove duplicates

// foreach (var subj in allSubjects)
// {
//     Console.WriteLine(subj);
// }

// ZIP
// var numbers = new List<int> { 1, 2, 3 };
// var words = new List<string> { "one", "two", "three" };

// var zipped = numbers.Zip(words, (n, w) => $"{n} = {w}");

// foreach (var z in zipped)
// {
//     Console.WriteLine(z);
// }

// JOINS & GROUPING :)
var students = new List<Student>
{
    new Student {Id = 1, Name = "John"},
    new Student {Id = 2, Name = "Jane"},
    new Student {Id = 3, Name = "Bob"}
};

var enrollments = new List<Enrollment>
{
    new Enrollment{StudentId = 1, Course ="Math"},
    new Enrollment{StudentId = 2, Course ="History"},
    new Enrollment{StudentId = 1, Course ="Physics"},
    new Enrollment{StudentId = 3, Course ="Biology"},
};

// var studentCourse = students.Join(
//     enrollments,                            // the second list
//     s => s.Id,                             // key from students
//     e => e.StudentId,                     // key from enrollments
//     (s, e) => new { s.Name, e.Course }   // the result
// );

// foreach (var sc in studentCourse)
// {
//     Console.WriteLine($"{sc.Name} => {sc.Course}");
// }

var grouped = enrollments.GroupBy(e => e.StudentId);

foreach (var group in grouped)
{
    Console.WriteLine($"StudentID: {group.Key}");    // the key of this bucket
    foreach (var e in group)
    {
        Console.WriteLine($"   {e.Course}");        // the items in this bucket
    }
}