// Console.WriteLine("Start");

// Thread.Sleep(3000);

// Console.WriteLine("Doing other work");
// Console.WriteLine("End");

// Console.WriteLine("Start");

// var task = Task.Delay(3000);

// Console.WriteLine("Doing some other work...");

// await task;

// Console.WriteLine("End");

// async Task SayHelloAsync()
// {
//     Console.WriteLine("Hello");

//     await Task.Delay(2000);

//     Console.WriteLine("World");
// }

// await SayHelloAsync();

// async Task<int> CalculateAsync()
// {
//     Console.WriteLine("hey");
//     await Task.Delay(1000);
//     return 40;
// }

// int result = await CalculateAsync();
// Console.WriteLine(result);

// // const reusult = await CalculateAsync(); => Prmise<number>



// async Task<object> FetchDataAsync(string url)
// {
//     using var client = new HttpClient();
//     var data = await client.GetStringAsync(url);
//     return data;
// }

// var result = await FetchDataAsync("https://fakestoreapi.com/products/10");
// Console.WriteLine(result);

// using System.Text.Json;

// await FetchProducts();

// async Task FetchProducts()
// {
//     using var client = new HttpClient();
//     var json = await client.GetStringAsync("https://fakestoreapi.com/products");

//     var options = new JsonSerializerOptions
//     {
//         PropertyNameCaseInsensitive = true
//     };

//     var products = JsonSerializer.Deserialize<List<Product>>(json, options)!;

//     foreach (var product in products)
//     {
//         Console.WriteLine($"{product.Id} - {product.Title}");
//     }
// }

// record Product(int Id, string Title);

// async Task<string> FetchDataAsync(string url)
// {
//     using var client = new HttpClient();
//     return await client.GetStringAsync(url);
// }

// try
// {
//     var result = await FetchDataAsync("https://jsonplaceholder.typsicode.com/todos");
//     Console.WriteLine(result);
// }
// catch (Exception ex)
// {
//     Console.WriteLine($"Error: {ex.Message}");
// }
// finally
// {
//     Console.WriteLine("I don't care");
// }

// async Task<string> FetchAsync(string url)
// {
//     using var client = new HttpClient();
//     return await client.GetStringAsync(url);
// }

// try
// {
//     // start BOTH requests
//     var tasks = new[]
//     {
//         FetchAsync("https://fakestoreapi.com/products"),
//         FetchAsync("https://fakes2toreapi.com/products/15")
//     };

//     string[] results = await Task.WhenAll(tasks);

//     Console.WriteLine("Both fetched!");
//     Console.WriteLine(results[0]);
//     Console.WriteLine(results[1]);
// }
// catch (Exception ex)
// {
//     Console.WriteLine($"Error: {ex.Message}");
// }

async Task<string> FetchAsync(string url)
{
    using var client = new HttpClient();
    return await client.GetStringAsync(url);
}

try
{
    // start BOTH requests
    var tasks = new[]
    {
        FetchAsync("https://fakestoreapi.com/products"),
        FetchAsync("https://jsonplaceholder.typicode.com/todos")
    };

    Task<string> firstTask = await Task.WhenAny(tasks);
    string firstResult = await firstTask;

    Console.WriteLine(firstResult);
}
catch (Exception ex)
{
    Console.WriteLine($"Error: {ex.Message}");
}