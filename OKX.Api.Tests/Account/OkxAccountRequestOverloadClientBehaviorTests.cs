using Newtonsoft.Json.Linq;
using OKX.Api.Account;
using OKX.Api.Common;
using OKX.Api.Trade;
using OKX.Api.Tests.TestInfrastructure;

namespace OKX.Api.Tests.Account;

public class OkxAccountRequestOverloadClientBehaviorTests
{
    [Fact]
    public async Task GetPositionsHistoryRequestOverload_MatchesLegacyQuery()
    {
        using var server = CreateServer("/api/v5/account/positions-history", "{\"code\":\"0\",\"msg\":\"\",\"data\":[{}]}");
        var client = CreateClient(server);

        await client.Account.GetPositionsHistoryAsync(OkxInstrumentType.Swap, "BTC-USDT-SWAP", OkxAccountMarginMode.Cross, OkxClosingPositionType.ClosePartially, "123", 1, 2, 3);
        await client.Account.GetPositionsHistoryAsync(new OkxAccountPositionsHistoryRequest
        {
            InstrumentType = OkxInstrumentType.Swap,
            InstrumentId = "BTC-USDT-SWAP",
            MarginMode = OkxAccountMarginMode.Cross,
            Type = OkxClosingPositionType.ClosePartially,
            PositionId = "123",
            After = 1,
            Before = 2,
            Limit = 3
        });

        AssertRequestQueriesEqual(server);
    }

    [Fact]
    public async Task GetBillHistoryRequestOverload_MatchesLegacyQuery()
    {
        using var server = CreateServer("/api/v5/account/bills", "{\"code\":\"0\",\"msg\":\"\",\"data\":[{}]}");
        var client = CreateClient(server);

        await client.Account.GetBillHistoryAsync(OkxInstrumentType.Swap, "BTC-USDT-SWAP", "BTC", OkxAccountMarginMode.Cross, OkxContractType.Linear, OkxAccountBillType.Trade, OkxAccountBillSubType.OpenLong, 1, 2, 3, 4, 5);
        await client.Account.GetBillHistoryAsync(new OkxAccountBillQueryRequest
        {
            InstrumentType = OkxInstrumentType.Swap,
            InstrumentId = "BTC-USDT-SWAP",
            Currency = "BTC",
            MarginMode = OkxAccountMarginMode.Cross,
            ContractType = OkxContractType.Linear,
            BillType = OkxAccountBillType.Trade,
            BillSubType = OkxAccountBillSubType.OpenLong,
            After = 1,
            Before = 2,
            Begin = 3,
            End = 4,
            Limit = 5
        });

        AssertRequestQueriesEqual(server);
    }

    [Fact]
    public async Task SetLeverageRequestOverload_MatchesLegacyBody()
    {
        using var server = CreateServer("/api/v5/account/set-leverage", "{\"code\":\"0\",\"msg\":\"\",\"data\":[{}]}");
        var client = CreateClient(server);

        await client.Account.SetLeverageAsync(3m, "BTC", "BTC-USDT-SWAP", OkxAccountMarginMode.Cross, OkxTradePositionSide.Long);
        await client.Account.SetLeverageAsync(new OkxAccountSetLeverageRequest
        {
            Leverage = 3m,
            Currency = "BTC",
            InstrumentId = "BTC-USDT-SWAP",
            MarginMode = OkxAccountMarginMode.Cross,
            PositionSide = OkxTradePositionSide.Long
        });

        AssertRequestBodiesEqual(server);
    }

    [Fact]
    public async Task GetMaximumLoanAmountRequestOverload_MatchesLegacyQuery()
    {
        using var server = CreateServer("/api/v5/account/max-loan", "{\"code\":\"0\",\"msg\":\"\",\"data\":[{}]}");
        var client = CreateClient(server);

        await client.Account.GetMaximumLoanAmountAsync(OkxAccountMarginMode.Cross, "BTC-USDT", "BTC", "USDT", "USD");
        await client.Account.GetMaximumLoanAmountAsync(new OkxAccountMaximumLoanAmountRequest
        {
            MarginMode = OkxAccountMarginMode.Cross,
            InstrumentId = "BTC-USDT",
            Currency = "BTC",
            MarginCurrency = "USDT",
            TradeQuoteCurrency = "USD"
        });

        AssertRequestQueriesEqual(server);
    }

    [Fact]
    public async Task GetInterestAccruedRequestOverload_MatchesLegacyQuery()
    {
        using var server = CreateServer("/api/v5/account/interest-accrued", "{\"code\":\"0\",\"msg\":\"\",\"data\":[{}]}");
        var client = CreateClient(server);

        await client.Account.GetInterestAccruedAsync(OkxAccountLoanType.Market, "BTC", "BTC-USDT", OkxAccountMarginMode.Cross, 1, 2, 3);
        await client.Account.GetInterestAccruedAsync(new OkxAccountInterestAccruedRequest
        {
            Type = OkxAccountLoanType.Market,
            Currency = "BTC",
            InstrumentId = "BTC-USDT",
            MarginMode = OkxAccountMarginMode.Cross,
            After = 1,
            Before = 2,
            Limit = 3
        });

        AssertRequestQueriesEqual(server);
    }

    [Fact]
    public async Task GetBorrowRepayHistoryRequestOverload_MatchesLegacyQuery()
    {
        using var server = CreateServer("/api/v5/account/spot-borrow-repay-history", "{\"code\":\"0\",\"msg\":\"\",\"data\":[{}]}");
        var client = CreateClient(server);

        await client.Account.GetBorrowRepayHistoryAsync("BTC", "manual_borrow", 1, 2, 3);
        await client.Account.GetBorrowRepayHistoryAsync(new OkxAccountBorrowRepayHistoryRequest
        {
            Currency = "BTC",
            Type = "manual_borrow",
            After = 1,
            Before = 2,
            Limit = 3
        });

        AssertRequestQueriesEqual(server);
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
