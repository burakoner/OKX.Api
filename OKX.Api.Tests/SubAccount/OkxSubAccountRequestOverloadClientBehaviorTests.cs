using Newtonsoft.Json.Linq;
using OKX.Api.Account;
using OKX.Api.Common;
using OKX.Api.SubAccount;
using OKX.Api.Tests.TestInfrastructure;

namespace OKX.Api.Tests.SubAccount;

public class OkxSubAccountRequestOverloadClientBehaviorTests
{
    [Fact]
    public async Task GetSubAccountsRequestOverload_MatchesLegacyQuery()
    {
        using var server = CreateServer("/api/v5/users/subaccount/list", "{\"code\":\"0\",\"msg\":\"\",\"data\":[{}]}");
        var client = CreateClient(server);

        await client.SubAccount.GetSubAccountsAsync(true, "sub", 1, 2, 3);
        await client.SubAccount.GetSubAccountsAsync(new OkxSubAccountListRequest
        {
            Enable = true,
            SubAccountName = "sub",
            After = 1,
            Before = 2,
            Limit = 3
        });

        AssertRequestQueriesEqual(server);
    }

    [Fact]
    public async Task CreateSubAccountApiKeyRequestOverload_MatchesLegacyBody()
    {
        using var server = CreateServer("/api/v5/users/subaccount/apikey", "{\"code\":\"0\",\"msg\":\"\",\"data\":[{}]}");
        var client = CreateClient(server);

        await client.SubAccount.CreateSubAccountApiKeyAsync("sub", "label", "Pass!123", true, true, "127.0.0.1");
        await client.SubAccount.CreateSubAccountApiKeyAsync(new OkxSubAccountApiKeyCreateRequest
        {
            SubAccountName = "sub",
            Label = "label",
            Passphrase = "Pass!123",
            ReadPermission = true,
            TradePermission = true,
            IpAddresses = "127.0.0.1"
        });

        AssertRequestBodiesEqual(server);
    }

    [Fact]
    public async Task ResetSubAccountApiKeyRequestOverload_MatchesLegacyBody()
    {
        using var server = CreateServer("/api/v5/users/subaccount/modify-apikey", "{\"code\":\"0\",\"msg\":\"\",\"data\":[{}]}");
        var client = CreateClient(server);

        await client.SubAccount.ResetSubAccountApiKeyAsync("sub", "api-key", "label", true, false, "127.0.0.1");
        await client.SubAccount.ResetSubAccountApiKeyAsync(new OkxSubAccountApiKeyResetRequest
        {
            SubAccountName = "sub",
            ApiKey = "api-key",
            ApiLabel = "label",
            ReadPermission = true,
            TradePermission = false,
            IpAddresses = "127.0.0.1"
        });

        AssertRequestBodiesEqual(server);
    }

    [Fact]
    public async Task GetManagedBillsRequestOverload_MatchesLegacyQuery()
    {
        using var server = CreateServer("/api/v5/asset/subaccount/managed-subaccount-bills", "{\"code\":\"0\",\"msg\":\"\",\"data\":[{}]}");
        var client = CreateClient(server);

        await client.SubAccount.GetSubAccountManagedBillsAsync("BTC", OkxSubAccountTransferType.FromMasterAccountToSubAccout, "sub", 123, 1, 2, 3);
        await client.SubAccount.GetSubAccountManagedBillsAsync(new OkxSubAccountManagedBillQueryRequest
        {
            Currency = "BTC",
            Type = OkxSubAccountTransferType.FromMasterAccountToSubAccout,
            SubAccountName = "sub",
            SubAccountId = 123,
            After = 1,
            Before = 2,
            Limit = 3
        });

        AssertRequestQueriesEqual(server);
    }

    [Fact]
    public async Task TransferBetweenSubAccountsRequestOverload_MatchesLegacyBody()
    {
        using var server = CreateServer("/api/v5/asset/subaccount/transfer", "{\"code\":\"0\",\"msg\":\"\",\"data\":[{}]}");
        var client = CreateClient(server);

        await client.SubAccount.TransferBetweenSubAccountsAsync("BTC", 1.5m, OkxAccount.Funding, OkxAccount.Trading, "from-sub", "to-sub", true, false);
        await client.SubAccount.TransferBetweenSubAccountsAsync(new OkxSubAccountTransferRequest
        {
            Currency = "BTC",
            Amount = 1.5m,
            FromAccount = OkxAccount.Funding,
            ToAccount = OkxAccount.Trading,
            FromSubAccountName = "from-sub",
            ToSubAccountName = "to-sub",
            LoanTransfer = true,
            OmitPositionRisk = false
        });

        AssertRequestBodiesEqual(server);
    }

    private static void AssertRequestBodiesEqual(LocalOkxRestServer server)
    {
        Assert.Equal(2, server.Requests.Count);
        Assert.Equal(server.Requests[0].Path, server.Requests[1].Path);
        Assert.True(JToken.DeepEquals(JToken.Parse(server.Requests[0].Body), JToken.Parse(server.Requests[1].Body)));
    }

    private static void AssertRequestQueriesEqual(LocalOkxRestServer server)
    {
        Assert.Equal(2, server.Requests.Count);
        Assert.Equal(server.Requests[0].Path, server.Requests[1].Path);
        Assert.Equal(server.Requests[0].Query, server.Requests[1].Query);
    }

    private static LocalOkxRestServer CreateServer(string path, string response)
        => new(new Dictionary<string, string>
        {
            [$"GET {path}"] = response,
            [$"POST {path}"] = response,
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
