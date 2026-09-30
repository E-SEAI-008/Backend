// ============================================================
// LINQ Exercise
// ============================================================
// Use the Products list below to complete all exercises.
//
// To run this single-file C# program with the .NET 10 SDK:
//   dotnet run Exercise.cs
//
// Microsoft Learn:
// Select    → https://learn.microsoft.com/dotnet/api/system.linq.enumerable.select
// Where     → https://learn.microsoft.com/dotnet/api/system.linq.enumerable.where
// First...  → https://learn.microsoft.com/dotnet/api/system.linq.enumerable.firstordefault
// Any       → https://learn.microsoft.com/dotnet/api/system.linq.enumerable.any
// All       → https://learn.microsoft.com/dotnet/api/system.linq.enumerable.all
// Sum/Avg   → https://learn.microsoft.com/dotnet/api/system.linq.enumerable.sum
// GroupBy   → https://learn.microsoft.com/dotnet/api/system.linq.enumerable.groupby
// OrderBy   → https://learn.microsoft.com/dotnet/api/system.linq.enumerable.orderbydescending
// ============================================================
//
// JS → LINQ cheat sheet:
//   map()     → Select()
//   filter()  → Where()
//   find()    → FirstOrDefault()
//   some()    → Any()
//   every()   → All()
//   reduce()  → Sum() / Average() / Aggregate()
//   sort()    → OrderBy() / OrderByDescending()
//   (new!)    → GroupBy(), Join()
// ============================================================

using System;
using System.Collections.Generic;
using System.Linq;

// ---- the data model ----
public record Rating(double Rate, int Count);

public record Product(
    int Id,
    string Title,
    decimal Price,
    string Category,
    Rating Rating
);

public static class LinqExercises
{
    public static readonly List<Product> Products = new()
    {
        new(1,  "Fjallraven Backpack",          109.95m, "men's clothing",   new(3.9, 120)),
        new(2,  "Mens Casual T-Shirts",          22.30m, "men's clothing",   new(4.1, 259)),
        new(3,  "Mens Cotton Jacket",            55.99m, "men's clothing",   new(4.7, 500)),
        new(4,  "Mens Casual Slim Fit",           15.99m, "men's clothing",   new(2.1, 430)),
        new(5,  "Gold & Silver Bracelet",        695.00m, "jewelery",         new(4.6, 400)),
        new(6,  "Solid Gold Petite Micropave",   168.00m, "jewelery",         new(3.9, 70)),
        new(7,  "White Gold Plated Princess",      9.99m, "jewelery",         new(3.0, 400)),
        new(8,  "Pierced Owl Rose Gold",          10.99m, "jewelery",         new(1.9, 100)),
        new(9,  "WD 2TB External Hard Drive",     64.00m, "electronics",      new(3.3, 203)),
        new(10, "SanDisk SSD PLUS 1TB",          109.00m, "electronics",      new(2.9, 470)),
        new(11, "Silicon Power 256GB SSD",       109.00m, "electronics",      new(4.8, 319)),
        new(12, "WD 4TB Gaming Drive",           114.00m, "electronics",      new(4.8, 400)),
        new(13, "Acer SB220Q Monitor",           599.00m, "electronics",      new(2.9, 250)),
        new(14, "Samsung Curved Monitor",        999.99m, "electronics",      new(2.2, 140)),
        new(15, "Snowboard Jacket",               56.99m, "women's clothing", new(2.6, 235)),
        new(16, "Faux Leather Moto Jacket",        29.95m, "women's clothing", new(2.9, 340)),
        new(17, "Rain Jacket Windbreaker",         39.99m, "women's clothing", new(3.8, 679)),
        new(18, "MBJ Solid Short Sleeve",           9.85m, "women's clothing", new(4.7, 130)),
        new(19, "Opna Short Sleeve Moisture",       7.95m, "women's clothing", new(4.5, 146)),
        new(20, "DANVOUY Womens T Shirt",          12.99m, "women's clothing", new(3.6, 145)),
    };

    public static void Run()
    {
        // ============================================================
        // 🟡 Exercise 1 — Select()  (JS: map)
        //
        // Get a list of all product titles.
        // ============================================================


        // ============================================================
        // 🟡 Exercise 2 — Select() into a new shape  (JS: map to object)
        //
        // Project each product to an anonymous object with only Title and Price.
        // ============================================================


        // ============================================================
        // 🟠 Exercise 3 — Where()  (JS: filter)
        //
        // Get all products in the "electronics" category.
        // ============================================================


        // ============================================================
        // 🟠 Exercise 4 — Where()  (JS: filter)
        //
        // Get all products that cost less than $20.
        // ============================================================


        // ============================================================
        // 🟠 Exercise 5 — FirstOrDefault()  (JS: find)
        //
        // Find the product with Id 12.
        // Remember: FirstOrDefault() returns the default value if nothing matches.
        // ============================================================


        // ============================================================
        // 🟠 Exercise 6 — FirstOrDefault()  (JS: find)
        //
        // Find the first product with a rating rate above 4.5.
        // ============================================================


        // ============================================================
        // 🔵 Exercise 7 — Any()  (JS: some)
        //
        // Check if there is ANY product that costs more than $500.
        // Your result should be true or false.
        // ============================================================


        // ============================================================
        // 🔵 Exercise 8 — All()  (JS: every)
        //
        // Check if ALL products have a rating rate above 1.8.
        // Your result should be true or false.
        // ============================================================


        // ============================================================
        // 🔴 Exercise 9 — Sum()  (JS: reduce)
        //
        // Calculate the total price of all products combined.
        // ============================================================


        // ============================================================
        // 🔴 Exercise 10 — Average()  (JS: reduce for average)
        //
        // Calculate the average rating rate of all products, rounded to 2 decimals.
        // Hint: Average() has a selector overload too.
        // ============================================================


        // ============================================================
        // ⭐ Exercise 11 — Chaining (Where + Select)
        //
        // Get the titles of all "jewelery" products that cost less than $100.
        // ============================================================


        // ============================================================
        // ⭐ Exercise 12 — Chaining (Where + Sum)
        //
        // Get the total price of all "electronics" products.
        // Hint: filter first, then sum.
        // ============================================================


        // ============================================================
        // 🟣 Exercise 13 — OrderByDescending() + Take()  (LINQ extra)
        //
        // Get the titles of the 3 highest-rated products, best first.
        // Hint: OrderByDescending, then Take(3), then Select.
        // ============================================================


        // ============================================================
        // 🟣 Exercise 14 — GroupBy()  (LINQ extra)
        //
        // Count how many products are in each category.
        // ============================================================


        // ============================================================
        // 🟣 Exercise 15 — GroupBy() + aggregate per group  (LINQ extra)
        //
        // For each category, calculate the average price, ordered by average
        // price from high to low.
        // Hint: GroupBy → Select (Key + Average) → OrderByDescending.
        // ============================================================


        // ============================================================
        // 🟣 BONUS — Build a category report  (LINQ extra)
        //
        // Build one summary object per category with:
        //   - Category name
        //   - Number of products
        //   - Average rating rate, rounded to 1 decimal
        //   - Title of the most expensive product
        // Order the result by product count, most products first.
        //
        // Hint: GroupBy(p => p.Category), then Select into an anonymous object.
        // Inside the group you can use g.Count(), g.Average(...),
        // and g.OrderByDescending(p => p.Price).First().Title.
        // ============================================================
    }
}

public class Program
{
    public static void Main()
    {
        LinqExercises.Run();
    }
}
