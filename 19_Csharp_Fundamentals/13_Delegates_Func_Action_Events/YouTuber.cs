public class YouTuber
{
    public string Name { get; }

    public event EventHandler<VideoEventArgs>? VideoPublished;
    public YouTuber(string name)
    {
        Name = name;
    }
    public void PublishVideo(string title)
    {
        Console.WriteLine($"{Name} published a new video: {title}");

        var eventArgs = new VideoEventArgs(title);

        VideoPublished?.Invoke(this, eventArgs);
    }
}