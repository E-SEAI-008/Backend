string filePath = Path.Combine("data", "books.json");

var storage = new BookStorage(filePath);
var app = new BookApp(storage);

app.Run();