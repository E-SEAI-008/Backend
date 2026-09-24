public class ConcertTicket : Ticket
{
    public string Band { get; set; }
    public ConcertTicket(string holder, decimal price, string band)
        : base(holder, price)
    {
        Band = band;
    }

    public void PrintConcertInfo()
    {
        Console.WriteLine($"Concert Ticket for {Holder}, Band: {Band}, Price: {Price:C}");
    }
}