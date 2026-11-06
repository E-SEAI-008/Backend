using System.ComponentModel.DataAnnotations;
using BudgetApi.Dtos.Auth;

namespace BudgetApi.Tests;

public class RegisterDtoTests
{
    [Fact]
    public void EmptyName_IsInvalid()
    {
        // ARRANGE
        var dto = new RegisterDto("", "alice@mail.com", "Test1234!");
        var results = new List<ValidationResult>();

        // ACT
        var valid = Validator.TryValidateObject(
            dto, new ValidationContext(dto), results,
            validateAllProperties: true);

        // ASSERT
        Assert.False(valid);
    }

    [Fact]
    public void ValidRegistration_IsValid()
    {
        var dto = new RegisterDto("Alice", "alice@mail.com", "Test1234!");
        var results = new List<ValidationResult>();

        var valid = Validator.TryValidateObject(dto,
        new ValidationContext(dto), results,
        validateAllProperties: true);

        Assert.True(valid);
    }
}