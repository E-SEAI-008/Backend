using System.Security.Claims;
using BudgetApi.Extensions;
using Microsoft.IdentityModel.JsonWebTokens;

namespace BudgetApi.Tests;

public class ClaimsPrincipalTests
{
    [Fact]
    public void GetUserId_ReturnsIdFromSubClaim()
    {
        var id = Guid.NewGuid();
        var user = new ClaimsPrincipal(new ClaimsIdentity([
            new Claim(JwtRegisteredClaimNames.Sub, id.ToString())
        ]));

        var result = user.GetUserId();

        Assert.Equal(id, result);
    }
    [Fact]
    public void GetUserId_WithoutSubClaim_Throws()
    {
        var user = new ClaimsPrincipal(new ClaimsIdentity());

        Assert.Throws<InvalidOperationException>(() => user.GetUserId());

    }
}