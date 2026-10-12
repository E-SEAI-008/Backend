var calc = new Calculator();

// Console.WriteLine(calc.Compute(3, 4, MathService.Add));
// Console.WriteLine(calc.Compute(3, 4, MathService.Multiply));

var engine = new PriceEngine();

Func<decimal, decimal> halfOff = p => p / 2;
Func<decimal, decimal> addTax = p => p * 1.2m;
Func<decimal, decimal> noDiscount = p => p;

// Console.WriteLine(engine.CalculatePrice(100, halfOff));
// Console.WriteLine(engine.CalculatePrice(100, addTax));
// Console.WriteLine(engine.CalculatePrice(100, noDiscount));

var processor = new Processor();

// processor.Process(msg => Console.WriteLine($"Console: {msg}"));
// processor.Process(msg => File.AppendAllText("log.txt", msg + "\n")); ;

var john = new YouTuber("John");
var jane = new Subscriber("Jane");
var alex = new Subscriber("Alex");

// Subscribe
john.VideoPublished += jane.OnVideoPublished;
john.VideoPublished += alex.OnVideoPublished;


john.PublishVideo("Learning C# Events");

john.VideoPublished -= alex.OnVideoPublished;

john.PublishVideo("Another C# Topic");