using System.Net;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;

namespace BudgetApi.Tests;

public class ApiTests
{
    public static WebApplicationFactory<Program> CreateFactory()
    {
        return new WebApplicationFactory<Program>()
            .WithWebHostBuilder(builder =>
            {
                builder.UseEnvironment("Testing");

                builder.UseSetting(
                    "ConnectionStrings:DefaultConnection",
                    "Host=loclahost;Database=budget_tests;Username=test;Password=test");

                builder.UseSetting("Jwt:Key", new string('x', 64));

            });
    }
    [Fact]
    public async Task AuthMe_WithoutToken_Returns401()
    {
        using var factory = CreateFactory();
        using var client = factory.CreateClient();

        var response = await client.GetAsync("/auth/me");

        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }
    [Fact]
    public async Task UnknownRoute_Returns404()
    {
        using var factory = CreateFactory();
        using var client = factory.CreateClient();

        var response = await client.GetAsync("/doesntExist");

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }
}