using ApiSharp.Models;
using OKX.Api.Financial;
using OKX.Api.Tests.TestInfrastructure;

namespace OKX.Api.Tests.Financial;

public class OkxFinancialStakingClientBehaviorTests
{
    [Fact]
    public async Task EthPurchaseAsync_ReturnsTrue_ForDocsEmptySuccessPayload()
    {
        var result = await ExecuteBooleanResultAsync(
            "POST /api/v5/finance/staking-defi/eth/purchase",
            FixtureReader.ReadManual("Financial", "staking-empty-success.json"),
            api => api.Financial.EthStaking.PurchaseAsync(1.0m));

        Assert.True(result.Data);
    }

    [Fact]
    public async Task EthRedeemAsync_ReturnsTrue_ForDocsEmptySuccessPayload()
    {
        var result = await ExecuteBooleanResultAsync(
            "POST /api/v5/finance/staking-defi/eth/redeem",
            FixtureReader.ReadManual("Financial", "staking-empty-success.json"),
            api => api.Financial.EthStaking.RedeemAsync(1.0m));

        Assert.True(result.Data);
    }

    [Fact]
    public async Task SolPurchaseAsync_ReturnsTrue_ForDocsEmptySuccessPayload()
    {
        var result = await ExecuteBooleanResultAsync(
            "POST /api/v5/finance/staking-defi/sol/purchase",
            FixtureReader.ReadManual("Financial", "staking-empty-success.json"),
            api => api.Financial.SolStaking.PurchaseAsync(1.0m));

        Assert.True(result.Data);
    }

    [Fact]
    public async Task SolRedeemAsync_ReturnsTrue_ForDocsEmptySuccessPayload()
    {
        var result = await ExecuteBooleanResultAsync(
            "POST /api/v5/finance/staking-defi/sol/redeem",
            FixtureReader.ReadManual("Financial", "staking-empty-success.json"),
            api => api.Financial.SolStaking.RedeemAsync(1.0m));

        Assert.True(result.Data);
    }

    [Fact]
    public async Task EthCancelRedeemAsync_ReturnsOrderReference()
    {
        using var server = new LocalOkxRestServer(new Dictionary<string, string>
        {
            ["POST /api/v5/finance/staking-defi/eth/cancel-redeem"] = FixtureReader.ReadManual("Financial", "eth-staking-cancel-redeem.json"),
        });

        var client = CreateClient(server);
        var result = await client.Financial.EthStaking.CancelRedeemAsync("1234567890");

        Assert.True(result.Success);
        Assert.NotNull(result.Data);
        Assert.Equal("1234567890", result.Data.OrderId);

        var request = Assert.Single(server.Requests);
        Assert.Equal("/api/v5/finance/staking-defi/eth/cancel-redeem", request.Path);
        Assert.Contains("\"ordId\":\"1234567890\"", request.Body);
    }

    private static async Task<RestCallResult<bool?>> ExecuteBooleanResultAsync(
        string route,
        string payload,
        Func<OkxRestApiClient, Task<RestCallResult<bool?>>> action)
    {
        using var server = new LocalOkxRestServer(new Dictionary<string, string>
        {
            [route] = payload,
        });

        var client = CreateClient(server);
        var result = await action(client);

        Assert.True(result.Success);
        Assert.NotNull(result.Data);
        Assert.Single(server.Requests);

        return result;
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
