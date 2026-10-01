public class Calculator
{
    public int Divide(int a, int b)
    {
        if (b == 0)
        {
            throw new DivideByZeroException("You tried to divide by zero");
        }
        return a / b;
    }
}