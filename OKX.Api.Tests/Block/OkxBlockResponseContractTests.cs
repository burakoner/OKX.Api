using ApiSharp.Models;
using Newtonsoft.Json;
using OKX.Api.Base;
using OKX.Api.Block;
using OKX.Api.Tests.TestInfrastructure;
using OKX.Api.Trade;

namespace OKX.Api.Tests.Block;

public class OkxBlockResponseContractTests
{
    [Fact]
    public void ManualCreateRfqFixture_ParsesAccountAllocationStatuses()
    {
        var response = DeserializeList<OkxBlockRfq>("create-rfq-response.json");

        var rfq = Assert.Single(response.Data!);
        Assert.Equal("rfq-tag", rfq.Tag);
        Assert.Equal("grp-100", rfq.GroupId);

        var allocation = Assert.Single(rfq.AccountLevelAllocations);
        Assert.Equal("sub-1", allocation.Account);
        Assert.Equal("0", allocation.Code);
        Assert.Equal(string.Empty, allocation.Message);
    }

    [Fact]
    public void ManualGetRfqsFixture_ParsesFlowTypeAndTag()
    {
        var response = DeserializeList<OkxBlockRfq>("get-rfqs.json");

        var rfq = Assert.Single(response.Data!);
        Assert.Equal("maker_rfq", rfq.FlowType);
        Assert.Equal("maker-tag", rfq.Tag);
        Assert.Equal("grp-200", rfq.GroupId);
    }

    [Fact]
    public void ManualCreateQuoteFixture_ParsesQuoteMetadata()
    {
        var response = DeserializeList<OkxBlockQuote>("create-quote-response.json");

        var quote = Assert.Single(response.Data!);
        Assert.Equal("22539", quote.RfqId);
        Assert.Equal("25092", quote.QuoteId);
        Assert.Equal("q001", quote.ClientQuoteId);
        Assert.Equal("quote-tag", quote.Tag);
        Assert.Equal(OkxTradeOrderSide.Buy, quote.QuoteSide);
    }

    [Fact]
    public void ManualGetTradesFixture_ParsesTradeMetadataAndAllocationErrors()
    {
        var response = DeserializeList<OkxBlockTrade>("get-trades.json");

        var trade = Assert.Single(response.Data!);
        Assert.Equal("trade-tag", trade.Tag);
        Assert.False(trade.IsSuccessful);
        Assert.Equal("54001", trade.ErrorCode);

        var allocation = Assert.Single(trade.AccountLevelAllocations);
        Assert.Equal("sub-1", allocation.Account);
        Assert.Equal("51000", allocation.Code);
    }

    [Fact]
    public void ManualPublicExecutedTradesFixture_ParsesGroupIdsAndEmptyIdentifiers()
    {
        var response = DeserializeList<OkxBlockPublicExecutedTrade>("get-public-executed-trades.json");

        var trade = Assert.Single(response.Data!);
        Assert.Equal("grp-300", trade.GroupId);
        Assert.Null(trade.BlockTradeId);

        var leg = Assert.Single(trade.Legs);
        Assert.Null(leg.TradeId);
    }

    [Fact]
    public void ManualPublicRecentTradesFixture_ParsesGroupIds()
    {
        var response = DeserializeList<OkxBlockPublicRecentTrade>("get-public-recent-trades.json");

        var trade = Assert.Single(response.Data!);
        Assert.Equal("grp-400", trade.GroupId);
        Assert.Equal("BTC-USDT", trade.InstrumentId);
    }

    private static OkxRestApiResponse<List<T>> DeserializeList<T>(params string[] fixturePath) where T : class
    {
        var json = FixtureReader.ReadManual(["Block", .. fixturePath]);
        var response = JsonConvert.DeserializeObject<OkxRestApiResponse<List<T>>>(json, SerializerOptions.WithConverters);

        Assert.NotNull(response);
        Assert.Equal(0, response.ErrorCode);
        Assert.NotNull(response.Data);
        return response;
    }
}
