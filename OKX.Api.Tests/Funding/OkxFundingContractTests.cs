using ApiSharp.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OKX.Api.Base;
using OKX.Api.Funding;
using OKX.Api.Trade;
using OKX.Api.Tests.TestInfrastructure;

namespace OKX.Api.Tests.Funding;

public class OkxFundingContractTests
{
    [Fact]
    public void WithdrawalReceiver_SerializesStreetNameWithCorrectJsonKey()
    {
        var receiver = new OkxFundingWithdrawalReceiver
        {
            WalletType = "private",
            ExchangeId = "0",
            ReceiverFirstName = "Bruce",
            ReceiverLastName = "Wayne",
            ReceiverCountry = "US",
            ReceiverCountrySubDivision = "California",
            ReceiverTownName = "San Jose",
            ReceiverStreetName = "Clementi Avenue 1",
        };

        var payload = JObject.Parse(JsonConvert.SerializeObject(receiver, SerializerOptions.WithConverters));

        Assert.Equal("San Jose", payload["rcvrTownName"]?.Value<string>());
        Assert.Equal("Clementi Avenue 1", payload["rcvrStreetName"]?.Value<string>());
    }

    [Fact]
    public void EstimateQuoteFixture_ParsesRfqCurrencyAsString()
    {
        var response = DeserializeOne<OkxFundingConvertEstimateQuote>("estimate-quote.json");

        Assert.Equal("USDT", response.Data!.RfqAmountCurrency);
        Assert.Equal("quoterETH-USDT16461885104612381", response.Data.QuoteId);
    }

    [Fact]
    public void ConvertCurrencyDetailsFixture_ParsesDeprecatedFields()
    {
        var response = DeserializeList<OkxFundingConvertCurrency>("get-convert-currency-details.json");

        Assert.Collection(response.Data!,
            first =>
            {
                Assert.Equal("BTC", first.Currency);
                Assert.Null(first.MinimumAmount);
                Assert.Null(first.MaximumAmount);
            },
            second => Assert.Equal("ETH", second.Currency));
    }

    [Fact]
    public void DepositAddressFixture_ParsesAddressAttachment()
    {
        var response = DeserializeList<OkxFundingDepositAddress>("get-deposit-address-with-attachment.json");

        var address = Assert.Single(response.Data!);
        Assert.Equal("TONCOIN", address.Currency);
        Assert.NotNull(address.AddressAttachment);
        Assert.Equal("123456", address.AddressAttachment!["comment"]);
    }

    [Fact]
    public void DepositWithdrawStatusFixture_ParsesBothWithdrawalAndTransactionIds()
    {
        var response = DeserializeOne<OkxFundingDepositWithdrawStatus>("get-deposit-withdraw-status.json");

        Assert.Equal("200045249", response.Data!.WithdrawalId);
        Assert.Equal("16f3638329xxxxxx42d988f97", response.Data.TransactionId);
    }

    [Fact]
    public void FiatPaymentMethodsFixture_ParsesNestedLimitsAndAccounts()
    {
        var response = DeserializeList<OkxFundingFiatPaymentMethod>("fiat-payment-methods.json");

        var method = Assert.Single(response.Data!);
        Assert.Equal("TRY", method.Currency);
        Assert.Equal("TR_BANKS", method.PaymentMethod);
        Assert.Null(method.Limits.MonthlyLimit);
        Assert.Equal(1m, method.Limits.MinimumAmount);
        Assert.Equal("1", Assert.Single(method.Accounts).PaymentAccountId);
    }

    [Fact]
    public void FiatOrderFixture_ParsesTimestampsAndAmounts()
    {
        var response = DeserializeList<OkxFundingFiatOrder>("fiat-order.json");

        var order = Assert.Single(response.Data!);
        Assert.Equal("024041201450544699", order.OrderId);
        Assert.Equal(100m, order.Amount);
        Assert.Equal(1707429385000L, order.CreateTimestamp);
        Assert.Equal("completed", order.State);
    }

    [Fact]
    public void BuySellCurrenciesFixture_ParsesFiatAndCryptoLists()
    {
        var response = DeserializeOne<OkxFundingBuySellCurrencies>("buy-sell-currencies.json");

        Assert.Equal("USD", response.Data!.FiatCurrencies[0].Currency);
        Assert.Equal("BTC", response.Data.CryptoCurrencies[0].Currency);
    }

    [Fact]
    public void BuySellCurrencyPairFixture_ParsesOptionalQuotaFields()
    {
        var response = DeserializeOne<OkxFundingBuySellCurrencyPair>("buy-sell-currency-pair.json");

        Assert.Equal(OkxTradeOrderSide.Buy, response.Data!.Side);
        Assert.Null(response.Data.FixedPriceDailyLimit);
        Assert.Equal("balance", Assert.Single(response.Data.PaymentMethods));
    }

    [Fact]
    public void BuySellQuoteFixture_ParsesQuotePayload()
    {
        var response = DeserializeOne<OkxFundingBuySellQuote>("buy-sell-quote.json");

        Assert.Equal("quoterBTC-USD16461885104612381", response.Data!.QuoteId);
        Assert.Equal("USD", response.Data.RfqCurrency);
        Assert.Equal(30m, response.Data.QuoteFromAmount);
    }

    [Fact]
    public void BuySellOrderFixture_ParsesHistoryAndTradeShape()
    {
        var response = DeserializeList<OkxFundingBuySellOrder>("buy-sell-order.json");

        var order = Assert.Single(response.Data!);
        Assert.Equal("1234", order.OrderId);
        Assert.Equal(OkxTradeOrderSide.Buy, order.Side);
        Assert.Equal(1646188510461L, order.UpdateTimestamp);
    }

    private static OkxRestApiResponse<List<T>> DeserializeList<T>(string fileName)
    {
        var json = FixtureReader.ReadManual("Funding", fileName);
        var response = JsonConvert.DeserializeObject<OkxRestApiResponse<List<T>>>(json, SerializerOptions.WithConverters);

        Assert.NotNull(response);
        Assert.Equal(0, response.ErrorCode);
        Assert.NotNull(response.Data);
        return response;
    }

    private static OkxRestApiResponse<T> DeserializeOne<T>(string fileName) where T : class
    {
        var json = FixtureReader.ReadManual("Funding", fileName);
        var response = JsonConvert.DeserializeObject<OkxRestApiResponse<List<T>>>(json, SerializerOptions.WithConverters);

        Assert.NotNull(response);
        Assert.Equal(0, response.ErrorCode);
        Assert.NotNull(response.Data);

        return new OkxRestApiResponse<T>
        {
            ErrorCode = response.ErrorCode,
            ErrorMessage = response.ErrorMessage,
            Data = Assert.Single(response.Data),
        };
    }
}
