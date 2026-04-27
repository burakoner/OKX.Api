using ApiSharp.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OKX.Api.Base;
using OKX.Api.Common;
using OKX.Api.Spread;
using OKX.Api.Tests.TestInfrastructure;

namespace OKX.Api.Tests.Spread;

public class OkxSpreadContractTests
{
    [Fact]
    public void ManualSpreadFixtures_ParseCurrentDocumentedShapes()
    {
        var spreads = DeserializeList<OkxSpreadInstrument>("Spread", "get-spreads.json");
        var spread = Assert.Single(spreads);
        Assert.Equal("BTC-USDT_BTC-USDT-SWAP", spread.SpreadId);
        Assert.Null(spread.ExpiryTimestamp);

        var orderBook = DeserializeOne<OkxSpreadOrderBook>("Spread", "get-order-book.json");
        Assert.Single(orderBook.Asks);
        Assert.Single(orderBook.Bids);

        var ticker = DeserializeOne<OkxSpreadTicker>("Spread", "get-ticker.json");
        Assert.Equal("BTC-USDT_BTC-USDT-SWAP", ticker.SpreadId);
        Assert.Equal(-78.9m, ticker.BidPrice);

        var publicTrades = DeserializeList<OkxSpreadPublicTrade>("Spread", "get-public-trades.json");
        Assert.Single(publicTrades);

        var candles = DeserializeList<OkxSpreadCandlestick>("Spread", "get-candlesticks.json");
        Assert.Equal(2, candles.Count);

        var history = DeserializeList<OkxSpreadCandlestick>("Spread", "get-candlesticks-history.json");
        Assert.Equal(2, history.Count);
    }

    [Fact]
    public void ManualOrderDetailsFixture_ParsesTagAndEmptyStringFields()
    {
        var order = Assert.Single(DeserializeList<OkxSpreadOrder>("Spread", "get-order-details-empty-fields.json"));

        Assert.Equal(string.Empty, order.Tag);
        Assert.Null(order.FillPrice);
        Assert.Null(order.TradeId);
        Assert.Null(order.OrderCancelSource);
    }

    [Fact]
    public void ManualSpreadTradesFixture_ParsesEmptyLegNumbers()
    {
        var trade = Assert.Single(DeserializeList<OkxSpreadTrade>("Spread", "get-trades-empty-leg-fields.json"));
        var spotLeg = Assert.Single(trade.Legs, x => x.InstrumentId == "BTC-USDT");

        Assert.Equal(string.Empty, trade.Tag);
        Assert.Null(spotLeg.ContractQuantity);
        Assert.Null(spotLeg.LastFilledProfitAndLoss);
        Assert.Null(spotLeg.FeeQuantity);
    }

    [Fact]
    public void ManualSpreadOrderUpdatesSocketFixture_ParsesSpreadScopedChannelPayload()
    {
        var response = DeserializeSocket<OkxSpreadOrder>("Spread", "ws-spread-orders.json");
        Assert.Equal("sprd-orders", response.Arguments?.Channel);
        Assert.Equal("BTC-USDT_BTC-USDT-SWAP", response.Arguments?.SpreadId);

        var order = Assert.Single(response.Data);
        Assert.Equal(string.Empty, order.Tag);
        Assert.Null(order.FillQuantity);
        Assert.Null(order.OrderCancelSource);
    }

    [Fact]
    public void ManualSpreadTradesSocketFixture_ParsesSpreadTradeUpdates()
    {
        var response = DeserializeSocket<OkxSpreadTrade>("Spread", "ws-spread-trades.json");
        Assert.Equal("sprd-trades", response.Arguments?.Channel);
        Assert.Equal("BTC-USDT_BTC-USDT-SWAP", response.Arguments?.SpreadId);

        var trade = Assert.Single(response.Data);
        Assert.Equal(string.Empty, trade.Tag);
        Assert.Null(trade.Legs.Last().ContractQuantity);
    }

    [Fact]
    public void ManualSpreadOrderBookSocketFixture_ParsesSequenceMetadata()
    {
        var response = JObject.Parse(FixtureReader.ReadManual("Spread", "ws-spread-books-l2-tbt.json"))
            .ToObject<OkxSocketSpreadOrderBookUpdate>(JsonSerializer.Create(SerializerOptions.WithConverters));

        Assert.NotNull(response);
        Assert.Equal("snapshot", response!.Action);
        Assert.Equal("BTC-USDT_BTC-USDT-SWAP", response.Arguments?.SpreadId);

        var book = Assert.Single(response.Data);
        Assert.Equal(123456, book.Checksum);
        Assert.Equal(-1, book.PreviousSequenceId);
        Assert.Equal(10, book.SequenceId);
    }

    [Fact]
    public void LiveProductionSpreadFixtures_ParseCurrentPublicSnapshots()
    {
        var liveRoot = Path.Combine(TestPaths.LiveFixture("Production", "Spread"));
        if (!Directory.Exists(liveRoot))
            return;

        foreach (var file in Directory.GetFiles(liveRoot, "*.json"))
        {
            var name = Path.GetFileName(file);
            switch (name)
            {
                case "get-spreads-btc.json":
                    Assert.NotEmpty(DeserializeLiveList<OkxSpreadInstrument>("Production", "Spread", name));
                    break;
                case "get-order-book.json":
                    Assert.NotNull(DeserializeLiveOne<OkxSpreadOrderBook>("Production", "Spread", name));
                    break;
                case "get-ticker.json":
                    Assert.NotNull(DeserializeLiveOne<OkxSpreadTicker>("Production", "Spread", name));
                    break;
                case "get-public-trades.json":
                    Assert.NotEmpty(DeserializeLiveList<OkxSpreadPublicTrade>("Production", "Spread", name));
                    break;
                case "get-candlesticks.json":
                case "get-candlesticks-history.json":
                    Assert.NotEmpty(DeserializeLiveList<OkxSpreadCandlestick>("Production", "Spread", name));
                    break;
            }
        }
    }

    private static T DeserializeOne<T>(params string[] fixturePath)
        => Assert.Single(DeserializeList<T>(fixturePath));

    private static List<T> DeserializeList<T>(params string[] fixturePath)
    {
        var json = FixtureReader.ReadManual(fixturePath);
        var token = JObject.Parse(json)["data"];
        Assert.NotNull(token);

        var response = token!.ToObject<List<T>>(JsonSerializer.Create(SerializerOptions.WithConverters));
        Assert.NotNull(response);
        return response!;
    }

    private static T DeserializeLiveOne<T>(params string[] fixturePath)
        => Assert.Single(DeserializeLiveList<T>(fixturePath));

    private static List<T> DeserializeLiveList<T>(params string[] fixturePath)
    {
        var json = FixtureReader.ReadLive(fixturePath);
        var token = JObject.Parse(json)["data"];
        Assert.NotNull(token);

        var response = token!.ToObject<List<T>>(JsonSerializer.Create(SerializerOptions.WithConverters));
        Assert.NotNull(response);
        return response!;
    }

    private static OkxSocketUpdateResponse<List<T>> DeserializeSocket<T>(params string[] fixturePath)
    {
        var json = FixtureReader.ReadManual(fixturePath);
        var response = JObject.Parse(json).ToObject<OkxSocketUpdateResponse<List<T>>>(JsonSerializer.Create(SerializerOptions.WithConverters));
        Assert.NotNull(response);
        return response!;
    }
}
