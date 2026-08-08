using ApiSharp.Converters;
using ApiSharp.Throttling;
using Newtonsoft.Json.Linq;
using OKX.Api.Account;
using OKX.Api.Common;
using OKX.Api.Funding;
using OKX.Api.Tests.TestInfrastructure;
using OKX.Api.Trade;

namespace OKX.Api.Tests.Funding;

public class OkxFundingRequestOverloadClientBehaviorTests
{
    [Fact]
    public async Task TransferRequestOverload_MatchesLegacyBody()
    {
        using var server = CreateServer("/api/v5/asset/transfer", "{\"code\":\"0\",\"msg\":\"\",\"data\":[{}]}");
        var client = CreateClient(server);

        await client.Funding.TransferAsync(OkxFundingTransferType.TransferWithinAccount, "BTC", 1.23m, OkxAccount.Funding, OkxAccount.Trading, "sub", true, false, "legacy");
        await client.Funding.TransferAsync(new OkxFundingTransferRequest
        {
            Type = OkxFundingTransferType.TransferWithinAccount,
            Currency = "BTC",
            Amount = 1.23m,
            FromAccount = OkxAccount.Funding,
            ToAccount = OkxAccount.Trading,
            SubAccountName = "sub",
            LoanTransfer = true,
            OmitPositionRisk = false,
            ClientOrderId = "legacy"
        });

        AssertRequestBodiesEqual(server);
    }

    [Fact]
    public async Task GetBillsRequestOverload_MatchesLegacyQuery()
    {
        using var server = CreateServer("/api/v5/asset/bills", "{\"code\":\"0\",\"msg\":\"\",\"data\":[{}]}");
        var client = CreateClient(server);

        await client.Funding.GetBillsAsync("BTC", OkxFundingBillType.TransferToSubAccount, "cid", 1, 2, 3);
        await client.Funding.GetBillsAsync(new OkxFundingBillQueryRequest
        {
            Currency = "BTC",
            Type = OkxFundingBillType.TransferToSubAccount,
            ClientOrderId = "cid",
            After = 1,
            Before = 2,
            Limit = 3
        });

        AssertRequestQueriesEqual(server);
        Assert.DoesNotContain("pagingType", server.Requests[0].Query);
    }

    [Fact]
    public async Task GetBillsAsync_SerializesCustodyProviderWithoutHistoryPaging()
    {
        using var server = CreateServer("/api/v5/asset/bills", "{\"code\":\"0\",\"msg\":\"\",\"data\":[]}");
        var client = CreateClient(server);

        var result = await client.Funding.GetBillsAsync(new OkxFundingBillQueryRequest
        {
            Type = OkxFundingBillType.TransferOutTradingSubAccount,
            ThirdPartyType = OkxFundingThirdPartyType.Scb
        });

        Assert.True(result.Success);
        var request = Assert.Single(server.Requests);
        Assert.Contains("type=284", request.Query);
        Assert.Contains("thirdPartyType=5", request.Query);
        Assert.DoesNotContain("pagingType", request.Query);
    }

    [Fact]
    public async Task GetBillsHistoryAsync_SerializesCustodyProviderAndPagingType()
    {
        const string response = "{\"code\":\"0\",\"msg\":\"\",\"data\":[{\"billId\":\"12344\",\"ccy\":\"RLUSD\",\"clientId\":\"\",\"balChg\":\"2\",\"bal\":\"12\",\"type\":\"523\",\"ts\":\"1597026383085\",\"notes\":\"\"}]}";
        using var server = CreateServer("/api/v5/asset/bills-history", response);
        var client = CreateClient(server);

        var result = await client.Funding.GetBillsHistoryAsync(new OkxFundingBillQueryRequest
        {
            ThirdPartyType = OkxFundingThirdPartyType.Komainu,
            After = 12344,
            PagingType = 2
        });

        Assert.True(result.Success);
        Assert.Equal(OkxFundingBillType.AutoEarnRlusdInterest, Assert.Single(result.Data).Type);
        var request = Assert.Single(server.Requests);
        Assert.Contains("thirdPartyType=2", request.Query);
        Assert.Contains("after=12344", request.Query);
        Assert.Contains("pagingType=2", request.Query);
    }

    [Fact]
    public async Task BillQueries_ApplyCurrentEndpointSpecificRateLimits()
    {
        const string response = "{\"code\":\"0\",\"msg\":\"\",\"data\":[]}";
        using var server = new LocalOkxRestServer(new Dictionary<string, string>
        {
            ["GET /api/v5/asset/bills"] = response,
            ["GET /api/v5/asset/bills-history"] = response,
        });
        var client = CreateClient(server, RateLimitingBehavior.Fail);

        for (var i = 0; i < 6; i++)
            Assert.True((await client.Funding.GetBillsAsync()).Success);

        Assert.False((await client.Funding.GetBillsAsync()).Success);
        Assert.True((await client.Funding.GetBillsHistoryAsync()).Success);
        Assert.False((await client.Funding.GetBillsHistoryAsync()).Success);

        Assert.Equal(7, server.Requests.Count);
        Assert.Equal(6, server.Requests.Count(x => x.Path == "/api/v5/asset/bills"));
        Assert.Single(server.Requests, x => x.Path == "/api/v5/asset/bills-history");
    }

    [Fact]
    public async Task BillQueries_RejectUnsupportedPagingAndCustodyValuesBeforeSending()
    {
        using var billsServer = CreateServer("/api/v5/asset/bills", "{\"code\":\"0\",\"msg\":\"\",\"data\":[]}");
        var billsClient = CreateClient(billsServer);
        await Assert.ThrowsAsync<ArgumentOutOfRangeException>(() => billsClient.Funding.GetBillsAsync(new OkxFundingBillQueryRequest { PagingType = 2 }));
        Assert.Empty(billsServer.Requests);

        using var historyServer = CreateServer("/api/v5/asset/bills-history", "{\"code\":\"0\",\"msg\":\"\",\"data\":[]}");
        var historyClient = CreateClient(historyServer);
        await Assert.ThrowsAsync<ArgumentException>(() => historyClient.Funding.GetBillsHistoryAsync(new OkxFundingBillQueryRequest { PagingType = 3 }));
        await Assert.ThrowsAsync<ArgumentOutOfRangeException>(() => historyClient.Funding.GetBillsHistoryAsync(new OkxFundingBillQueryRequest { ThirdPartyType = (OkxFundingThirdPartyType)3 }));
        Assert.Empty(historyServer.Requests);
    }

    [Theory]
    [InlineData(OkxFundingBillType.DCDBrokerRebate, 271)]
    [InlineData(OkxFundingBillType.SolStakingSendLiquidityStakingTokenReward, 328)]
    [InlineData(OkxFundingBillType.AutoLendInterest, 400)]
    [InlineData(OkxFundingBillType.AutoEarnUsdgInterest, 408)]
    [InlineData(OkxFundingBillType.TransferredOutToCloudExchange, 476)]
    [InlineData(OkxFundingBillType.TransferredInFromCloudExchange, 477)]
    [InlineData(OkxFundingBillType.OkusdSubscription, 509)]
    [InlineData(OkxFundingBillType.OkusdRedemption, 511)]
    [InlineData(OkxFundingBillType.OkusdEarnings, 516)]
    [InlineData(OkxFundingBillType.OkusdMint, 518)]
    [InlineData(OkxFundingBillType.AutoEarnRlusdInterest, 523)]
    public void BillTypes_MapToCurrentOfficialValues(OkxFundingBillType type, int expected)
    {
        Assert.Equal(expected, (int)type);
        Assert.Equal(expected.ToString(), MapConverter.GetString(type));
    }

    [Fact]
    public void ThirdPartyTypes_MapToCurrentOfficialValues()
    {
        Assert.Equal("1", MapConverter.GetString(OkxFundingThirdPartyType.Copper));
        Assert.Equal("2", MapConverter.GetString(OkxFundingThirdPartyType.Komainu));
        Assert.Equal("5", MapConverter.GetString(OkxFundingThirdPartyType.Scb));
    }

    [Fact]
    public async Task GetDepositHistoryRequestOverload_MatchesLegacyQuery()
    {
        using var server = CreateServer("/api/v5/asset/deposit-history", "{\"code\":\"0\",\"msg\":\"\",\"data\":[{}]}");
        var client = CreateClient(server);

        await client.Funding.GetDepositHistoryAsync("BTC", "dep-1", 123, "tx-1", OkxFundingDepositType.InternalDeposit, OkxFundingDepositState.Credited, 1, 2, 3);
        await client.Funding.GetDepositHistoryAsync(new OkxFundingDepositHistoryRequest
        {
            Currency = "BTC",
            DepositId = "dep-1",
            FromWithdrawalId = 123,
            TransactionId = "tx-1",
            Type = OkxFundingDepositType.InternalDeposit,
            State = OkxFundingDepositState.Credited,
            After = 1,
            Before = 2,
            Limit = 3
        });

        AssertRequestQueriesEqual(server);
    }

    [Fact]
    public async Task WithdrawRequestOverload_MatchesLegacyBody()
    {
        using var server = CreateServer("/api/v5/asset/withdrawal", "{\"code\":\"0\",\"msg\":\"\",\"data\":[{}]}");
        var client = CreateClient(server);

        await client.Funding.WithdrawAsync("BTC", 1.5m, OkxFundingWithdrawalDestination.DigitalCurrencyAddress, "addr", OkxFundingWithdrawalAddressType.AddressInfo, "BTC-Bitcoin", null, null, "cid");
        await client.Funding.WithdrawAsync(new OkxFundingWithdrawalRequest
        {
            Currency = "BTC",
            Amount = 1.5m,
            Destination = OkxFundingWithdrawalDestination.DigitalCurrencyAddress,
            ToAddress = "addr",
            ToAddressType = OkxFundingWithdrawalAddressType.AddressInfo,
            Chain = "BTC-Bitcoin",
            ClientOrderId = "cid"
        });

        AssertRequestBodiesEqual(server);
    }

    [Fact]
    public async Task EstimateQuoteRequestOverload_MatchesLegacyBody()
    {
        using var server = CreateServer("/api/v5/asset/convert/estimate-quote", "{\"code\":\"0\",\"msg\":\"\",\"data\":[{}]}");
        var client = CreateClient(server);

        await client.Funding.EstimateQuoteAsync("BTC", "USDT", OkxTradeOrderSide.Sell, 2m, "BTC", "cid", true);
        await client.Funding.EstimateQuoteAsync(new OkxFundingConvertEstimateQuoteRequest
        {
            BaseCurrency = "BTC",
            QuoteCurrency = "USDT",
            Side = OkxTradeOrderSide.Sell,
            RfqAmount = 2m,
            RfqCurrency = "BTC",
            ClientOrderId = "cid",
            UseLargeOrderConvert = true
        });

        AssertRequestBodiesEqual(server);
    }

    [Fact]
    public async Task PlaceBuySellTradeRequestOverload_MatchesLegacyBody()
    {
        using var server = CreateServer("/api/v5/fiat/buy-sell/trade", "{\"code\":\"0\",\"msg\":\"\",\"data\":[{}]}");
        var client = CreateClient(server);

        await client.Funding.PlaceBuySellTradeAsync("quote-1", OkxTradeOrderSide.Buy, "USD", "BTC", 30m, "USD", "balance", "cid");
        await client.Funding.PlaceBuySellTradeAsync(new OkxFundingBuySellTradeRequest
        {
            QuoteId = "quote-1",
            Side = OkxTradeOrderSide.Buy,
            FromCurrency = "USD",
            ToCurrency = "BTC",
            RfqAmount = 30m,
            RfqCurrency = "USD",
            PaymentMethod = "balance",
            ClientOrderId = "cid"
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

    private static OkxRestApiClient CreateClient(LocalOkxRestServer server, RateLimitingBehavior rateLimitingBehavior = RateLimitingBehavior.Wait)
    {
        var options = new OkxRestApiOptions(new OkxApiCredentials("key", "secret", "pass"))
        {
            AutoTimestamp = false,
            BaseAddress = server.BaseAddress,
            RateLimitingBehavior = rateLimitingBehavior,
        };

        return new OkxRestApiClient(options);
    }
}
