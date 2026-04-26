using ApiSharp.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OKX.Api.Base;
using OKX.Api.Common;
using OKX.Api.Financial;
using OKX.Api.Tests.TestInfrastructure;

namespace OKX.Api.Tests.Financial;

public class OkxFinancialDualInvestmentContractTests
{
    [Fact]
    public void ManualCurrencyPairsFixture_ParsesDocumentedOptionTypes()
    {
        var response = DeserializeList<OkxFinancialDualInvestmentCurrencyPair>("get-currency-pairs.json");

        Assert.Equal(2, response.Data!.Count);
        Assert.Equal(OkxOptionType.Call, response.Data[0].OptionType);
        Assert.Equal(OkxOptionType.Put, response.Data[1].OptionType);
        Assert.All(response.Data, item => Assert.Equal("BTC-USD", item.Underlying));
    }

    [Fact]
    public void ManualProductsFixture_ParsesDocumentationArrayShape()
    {
        var response = DeserializeProducts("get-products-docs-shape.json");

        var product = Assert.Single(response.Data!.Products);
        Assert.Equal("BTC-USDT-260327-54500-P", product.ProductId);
        Assert.Equal(OkxOptionType.Put, product.OptionType);
        Assert.Equal(54500m, product.StrikePrice);
        Assert.Equal(10m, product.MinimumSize);
        Assert.Equal(6000000m, product.MaximumSize);
    }

    [Fact]
    public void ManualProductsFixture_ParsesLiveWrapperShape()
    {
        var response = DeserializeProducts("get-products-live-wrapper-shape.json");

        var product = Assert.Single(response.Data!.Products);
        Assert.Equal("BTC-USDT-260427-79250-C", product.ProductId);
        Assert.Equal(OkxOptionType.Call, product.OptionType);
        Assert.Equal("BTC", product.NotionalCurrency);
        Assert.Equal(79250m, product.StrikePrice);
        Assert.Equal(0.0001m, product.StepSize);
    }

    [Fact]
    public void ManualRequestQuoteFixture_ParsesQuoteResponse()
    {
        var response = DeserializeList<OkxFinancialDualInvestmentQuote>("post-request-quote.json");

        var quote = Assert.Single(response.Data!);
        Assert.Equal("BTC-USDT-260312-72000-C", quote.ProductId);
        Assert.Equal("qtbcDCD-QUOTE17732395560537636", quote.QuoteId);
        Assert.Equal(0.001m, quote.NotionalSize);
        Assert.Equal(69000m, quote.IndexPrice);
    }

    [Fact]
    public void ManualTradeFixture_ParsesPendingBookState()
    {
        var response = DeserializeList<OkxFinancialDualInvestmentTrade>("post-trade.json");

        var trade = Assert.Single(response.Data!);
        Assert.Equal("987654321", trade.OrderId);
        Assert.Equal(OkxFinancialDualInvestmentOrderState.PendingBook, trade.State);
    }

    [Fact]
    public void ManualRedeemQuoteFixture_ParsesRedeemQuoteResponse()
    {
        var response = DeserializeList<OkxFinancialDualInvestmentRedeemQuote>("post-request-redeem-quote.json");

        var quote = Assert.Single(response.Data!);
        Assert.Equal("987654321", quote.OrderId);
        Assert.Equal("BTC", quote.RedeemCurrency);
        Assert.Equal(1.4856m, quote.RedeemSize);
        Assert.Equal(-0.50m, quote.TermRate);
    }

    [Fact]
    public void ManualRedeemFixture_ParsesRedeemBookingState()
    {
        var response = DeserializeList<OkxFinancialDualInvestmentRedeem>("post-redeem.json");

        var redeem = Assert.Single(response.Data!);
        Assert.Equal("987654321", redeem.OrderId);
        Assert.Equal(OkxFinancialDualInvestmentOrderState.PendingRedeemBooking, redeem.State);
    }

    [Fact]
    public void ManualOrderStatusFixture_ParsesPendingSettleState()
    {
        var response = DeserializeList<OkxFinancialDualInvestmentOrderStatus>("get-order-status.json");

        var status = Assert.Single(response.Data!);
        Assert.Equal("987654321", status.OrderId);
        Assert.Equal(OkxFinancialDualInvestmentOrderState.PendingSettle, status.State);
    }

    [Fact]
    public void ManualOrderHistoryFixture_ParsesSettlementAndRedeemFields()
    {
        var response = DeserializeList<OkxFinancialDualInvestmentOrder>("get-order-history.json");

        Assert.Equal(3, response.Data!.Count);

        var settled = response.Data.Single(x => x.OrderId == "987654321");
        Assert.Equal(OkxFinancialDualInvestmentOrderState.Settled, settled.State);
        Assert.Equal(0.01209057m, settled.YieldSize);
        Assert.Equal(1.51209057m, settled.SettlementSize);
        Assert.Equal(76500m, settled.SettlementPrice);
        Assert.Equal(1774598400000L, settled.RedeemEndTimestamp);

        var pendingRedeem = response.Data.Single(x => x.OrderId == "987654322");
        Assert.Equal(OkxFinancialDualInvestmentOrderState.PendingRedeem, pendingRedeem.State);
        Assert.Null(pendingRedeem.SettlementSize);
        Assert.Null(pendingRedeem.SettlementTime);

        var redeemed = response.Data.Single(x => x.OrderId == "987654323");
        Assert.Equal(OkxFinancialDualInvestmentOrderState.Redeemed, redeemed.State);
        Assert.Equal(1774000000000L, redeemed.UpdateTimestamp);
    }

    private static OkxRestApiResponse<List<T>> DeserializeList<T>(params string[] fixturePath) where T : class
    {
        var json = FixtureReader.ReadManual(["Financial", "DualInvestment", .. fixturePath]);
        var response = JsonConvert.DeserializeObject<OkxRestApiResponse<List<T>>>(json, SerializerOptions.WithConverters);

        Assert.NotNull(response);
        Assert.Equal(0, response.ErrorCode);
        Assert.NotNull(response.Data);
        return response;
    }

    private static OkxRestApiResponse<OkxFinancialDualInvestmentProductsData> DeserializeProducts(params string[] fixturePath)
    {
        var json = FixtureReader.ReadManual(["Financial", "DualInvestment", .. fixturePath]);
        var response = JsonConvert.DeserializeObject<OkxRestApiResponse<OkxFinancialDualInvestmentProductsData>>(json, SerializerOptions.WithConverters);

        Assert.NotNull(response);
        Assert.Equal(0, response.ErrorCode);
        Assert.NotNull(response.Data);
        return response;
    }
}
