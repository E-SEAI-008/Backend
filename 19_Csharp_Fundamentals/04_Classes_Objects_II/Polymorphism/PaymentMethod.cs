public class PaymentMethod
{
    public virtual void Pay(decimal amount)
    {
        Console.WriteLine($"How would you like to pay?");
    }
}