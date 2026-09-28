// int[] numbers = new int[3];

// numbers[0] = 10;
// numbers[1] = 20;
// numbers[2] = 30;

// foreach (var n in numbers)
// {
//     Console.WriteLine(n);
// }

// string[] names = { "Costa", "Jessie", "Laissa", "Samuel" };

// foreach (var name in names)
// {
//     Console.WriteLine(name);
// }

// var shoppingList = new List<string>();

// shoppingList.Add("Milk");
// shoppingList.Add("Bread");
// shoppingList.Add("Eggs");

// shoppingList.Remove("Bread");

// if (shoppingList.Contains("Milk"))
// {
//     Console.WriteLine("Milk is on the list");
// }

// foreach (var item in shoppingList)
// {
//     Console.WriteLine(item);
// }

// var capitals = new Dictionary<string, string>();

// capitals["Germany"] = "Berlin";
// capitals["France"] = "Paris";
// capitals["Japan"] = "Tokyo";

// var city = capitals["Spain"];
// Console.WriteLine(city);

// if (capitals.ContainsKey("France"))
// {
//     Console.WriteLine(capitals["France"]);
// }

// if (capitals.TryGetValue("Spain", out var city))
// {
//     Console.WriteLine(city);
// }
// else
// {
//     Console.WriteLine("Spain not found");
// }

// foreach (var pair in capitals)
// {
//     Console.WriteLine($"{pair.Key} => {pair.Value}");
// }

// public interface IAnimal
// {
//     void MakeSound();
// }

// public class Dog : IAnimal
// {
//     public void MakeSound()
//     {
//         Console.WriteLine("...");
//     }
// }

// public class Cat : IAnimal
// {
//     public void MakeSound()
//     {
//         Console.WriteLine("...");
//     }
// }

// IENUMARABLE - ICOLLECTION - IREADONLY

// IENUMARABLE
// var numbers = new List<int> { 1, 2, 3 };
// int[] otherNumbers = { 4, 5, 6 };


// void PrintNumbers(IEnumerable<int> items)
// {
//     foreach (var item in items)
//     {
//         Console.WriteLine(item);
//     }
// }

// PrintNumbers(otherNumbers);
// PrintNumbers(numbers);

// ICOLLECTION

// var items = new List<string> { "Apple", "Banana" };

// void ManageItems(ICollection<string> items)
// {
//     items.Add("Orange"); // => allowed
//     items.Remove("Banana"); // => allowed

//     Console.WriteLine($"Count: {items.Count}");

//     foreach (var item in items)
//     {
//         Console.WriteLine(item);
//     }
// }

// ManageItems(items);

// IREADONLY
// var basket = new ShoppingBasket();

// foreach (var item in basket.Items)
// {
//     Console.WriteLine(item);
// }

// Console.WriteLine($"Count: {basket.Items.Count}");

// // basket.Items.Add("Orange"); // IREADONLY
// public class ShoppingBasket
// {
//     private readonly List<string> _items = new()
//     {
//         "Apple",
//         "Banana"
//     };

//     public IReadOnlyCollection<string> Items => _items.AsReadOnly();
// }


// int[] numbers = { 10, 20, 30 };
// numbers[1] = 99;
// numbers.Add(99)
