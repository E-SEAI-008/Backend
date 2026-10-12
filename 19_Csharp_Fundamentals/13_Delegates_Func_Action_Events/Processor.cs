class Processor
{
    public void Process(Action<string> logAction)
    {
        logAction("Process started...");
        // do some work...
        logAction("Process finished");
    }
}