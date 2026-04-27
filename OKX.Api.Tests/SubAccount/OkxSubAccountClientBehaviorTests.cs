using Newtonsoft.Json.Linq;
using OKX.Api.SubAccount;
using OKX.Api.Tests.TestInfrastructure;

namespace OKX.Api.Tests.SubAccount;

public class OkxSubAccountClientBehaviorTests
{
    [Fact]
    public async Task CreateSubAccountAsync_SendsBodyParametersInsteadOfQueryString()
    {
        using var server = CreateServer("/api/v5/users/subaccount/create-subaccount", "create-sub-account-response.json");
        var client = CreateClient(server);

        var result = await client.SubAccount.CreateSubAccountAsync(
            "subAccount002",
            OkxSubAccountType.StandardSubAccount,
            "123456");

        Assert.True(result.Success);
        var request = Assert.Single(server.Requests);
        Assert.Equal("/api/v5/users/subaccount/create-subaccount", request.Path);
        Assert.True(string.IsNullOrEmpty(request.Query));

        var payload = JObject.Parse(request.Body);
        Assert.Equal("subAccount002", payload["subAcct"]?.Value<string>());
        Assert.Equal("1", payload["type"]?.Value<string>());
        Assert.Equal("123456", payload["label"]?.Value<string>());
    }

    [Fact]
    public async Task CreateSubAccountAsync_RejectsUnsupportedManagedTypes()
    {
        using var server = CreateServer("/api/v5/users/subaccount/create-subaccount", "create-sub-account-response.json");
        var client = CreateClient(server);

        var exception = await Assert.ThrowsAsync<ArgumentException>(() =>
            client.SubAccount.CreateSubAccountAsync("subAccount002", OkxSubAccountType.ManagedTradingSubAccount));

        Assert.Contains("Create sub-account only supports", exception.Message);
    }

    [Fact]
    public async Task GetSubAccountFundingBalancesAsync_ReturnsAllRowsAndPreservesCurrencyFilter()
    {
        using var server = CreateServer("/api/v5/asset/subaccount/balances", "get-sub-account-funding-balances.json");
        var client = CreateClient(server);

        var result = await client.SubAccount.GetSubAccountFundingBalancesAsync("test-1", "BTC,ETH");

        Assert.True(result.Success);
        Assert.Equal(2, result.Data!.Count);

        var request = Assert.Single(server.Requests);
        Assert.Contains("subAcct=test-1", request.Query);
        Assert.Contains("ccy=BTC", request.Query);
    }

    [Fact]
    public async Task SetPermissionOfTransferOutAsync_OmitsOptionalFlagWhenNotProvided()
    {
        using var server = CreateServer("/api/v5/users/subaccount/set-transfer-out", "set-permission-of-transfer-out.json");
        var client = CreateClient(server);

        var result = await client.SubAccount.SetPermissionOfTransferOutAsync("Test001,Test002");

        Assert.True(result.Success);
        var request = Assert.Single(server.Requests);
        Assert.DoesNotContain("canTransOut", request.Body);
        Assert.Contains("\"subAcct\":\"Test001,Test002\"", request.Body);
    }

    [Fact]
    public async Task GetCustodySubAccountsAsync_AllowsOmittingSubAccountFilter()
    {
        using var server = CreateServer("/api/v5/users/entrust-subaccount-list", "get-custody-subaccounts.json");
        var client = CreateClient(server);

        var result = await client.SubAccount.GetCustodySubAccountsAsync();

        Assert.True(result.Success);
        Assert.Equal(2, result.Data!.Count);

        var request = Assert.Single(server.Requests);
        Assert.True(string.IsNullOrEmpty(request.Query));
    }

    private static LocalOkxRestServer CreateServer(string path, string fixtureName)
        => new(new Dictionary<string, string>
        {
            [$"GET {path}"] = FixtureReader.ReadManual("SubAccount", fixtureName),
            [$"POST {path}"] = FixtureReader.ReadManual("SubAccount", fixtureName),
        });

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
