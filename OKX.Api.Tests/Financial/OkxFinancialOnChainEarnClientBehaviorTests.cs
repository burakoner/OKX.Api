using ApiSharp.Models;
using OKX.Api.Financial;
using OKX.Api.Tests.TestInfrastructure;

namespace OKX.Api.Tests.Financial;

public class OkxFinancialOnChainEarnClientBehaviorTests
{
    [Fact]
    public async Task PurchaseAsync_ReturnsOrderReferenceWithTag()
    {
        var result = await ExecuteAsync(
            "POST /api/v5/finance/staking-defi/purchase",
            api => api.Financial.OnChainEarn.PurchaseAsync("101", [new OkxFinancialOnChainEarnInvestData
            {
                Currency = "DOT",
                Amount = "2",
            }]));

        Assert.Equal("754147", result.OrderId);
        Assert.Equal("broker-42", result.Tag);
    }

    [Fact]
    public async Task RedeemAsync_ReturnsOrderReferenceWithTag()
    {
        var result = await ExecuteAsync(
            "POST /api/v5/finance/staking-defi/redeem",
            api => api.Financial.OnChainEarn.RedeemAsync("754147", "defi"));

        Assert.Equal("754147", result.OrderId);
        Assert.Equal("broker-42", result.Tag);
    }

    [Fact]
    public async Task CancelAsync_ReturnsOrderReferenceWithTag()
    {
        var result = await ExecuteAsync(
            "POST /api/v5/finance/staking-defi/cancel",
            api => api.Financial.OnChainEarn.CancelAsync("754147", "defi"));

        Assert.Equal("754147", result.OrderId);
        Assert.Equal("broker-42", result.Tag);
    }

    private static async Task<OkxFinancialOrderReference> ExecuteAsync(
        string route,
        Func<OkxRestApiClient, Task<RestCallResult<OkxFinancialOrderReference>>> action)
    {
        using var server = new LocalOkxRestServer(new Dictionary<string, string>
        {
            [route] = FixtureReader.ReadManual("Financial", "onchain-order-reference.json"),
        });

        var client = CreateClient(server);
        var result = await action(client);

        Assert.True(result.Success);
        Assert.NotNull(result.Data);
        Assert.Single(server.Requests);

        return result.Data;
    }

    private static OkxRestApiClient CreateClient(LocalOkxRestServer server)
    {
        var options = new OkxRestApiOptions(new OkxApiCredentials("key", "secret", "pass"))
        {
            AutoTimestamp = false,
            BaseAddress = server.BaseAddress,
        };

        return new OkxRestApiClient(options);
    }
}
