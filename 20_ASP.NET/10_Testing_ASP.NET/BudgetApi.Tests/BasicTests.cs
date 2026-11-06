namespace BlogApi.Tests;

public class BasicTests
{
    [Fact]
    public void Add_TwoNumbers_ReturnsCorrectSum()
    {
        // Arrange
        int a = 5;
        int b = 3;

        // ACT
        int result = a + b;

        // ASSERT
        Assert.Equal(8, result);
    }
}