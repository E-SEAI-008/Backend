public class VideoEventArgs : EventArgs
{
    public string Title { get; }

    public VideoEventArgs(string title)
    {
        Title = title;
    }
}