public class Subscriber
{
    public string Name { get; }
    public Subscriber(string name)
    {
        Name = name;
    }

    public void OnVideoPublished(object? sender, VideoEventArgs e)
    {
        Console.WriteLine($"{Name} recieved a notification about '{e.Title}'.");
    }
}