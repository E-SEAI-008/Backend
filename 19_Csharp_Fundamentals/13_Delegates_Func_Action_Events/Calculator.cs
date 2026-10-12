// public delegate int Operation(int x, int y);
class Calculator
{
    // public int Compute(int a, int b, Operation operation)
    public int Compute(int a, int b, Func<int, int, int> operation)
    {
        return operation(a, b);
    }
}

