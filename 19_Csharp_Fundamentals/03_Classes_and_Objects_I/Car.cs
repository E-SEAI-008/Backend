public class Car
{
    public string Brand { get; set; }

    public Car(string brand)
    {
        Brand = brand;
    }

    private void StartEngine()
    {
        Console.WriteLine($"{Brand}'s engine started...");
    }

    public void Drive()
    {
        StartEngine();
        Console.WriteLine($"{Brand} is driving...");
    }
}