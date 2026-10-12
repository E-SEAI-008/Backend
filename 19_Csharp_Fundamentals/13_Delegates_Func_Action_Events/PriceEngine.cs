class PriceEngine
{
    public decimal CalculatePrice(decimal basePrice, Func<decimal, decimal> strategy)
    {
        return strategy(basePrice);
    }
}