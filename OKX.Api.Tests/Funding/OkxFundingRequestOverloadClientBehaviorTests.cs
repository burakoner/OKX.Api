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

        await client.Funding.GetBillsAsync("BTC", OkxFundingBillType.TransferToSubAccount, "cid", 1, 2, 3, 2);
        await client.Funding.GetBillsAsync(new OkxFundingBillQueryRequest
        {
            Currency = "BTC",
            Type = OkxFundingBillType.TransferToSubAccount,
            ClientOrderId = "cid",
            After = 1,
            Before = 2,
            Limit = 3,
            PagingType = 2
        });

        AssertRequestQueriesEqual(server);
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
