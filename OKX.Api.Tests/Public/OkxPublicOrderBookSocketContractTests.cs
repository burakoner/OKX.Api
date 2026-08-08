using System.Reflection;
using System.Runtime.CompilerServices;
using ApiSharp.Converters;
using ApiSharp.Models;
using Newtonsoft.Json.Linq;
using OKX.Api.Base;
using OKX.Api.Common;
using OKX.Api.Public;
using OKX.Api.Tests.TestInfrastructure;

namespace OKX.Api.Tests.Public;

public class OkxPublicOrderBookSocketContractTests
{
    [Fact]
    public void ManualStandardFixtures_ParseDeprecatedChecksumAndSequenceContinuity()
    {
        var snapshot = Deserialize("ws-order-book-snapshot.json");
        var update = Deserialize("ws-order-book-update.json");
        var snapshotBook = Assert.Single(snapshot.Data);
        var updateBook = Assert.Single(update.Data);

        Assert.Equal("snapshot", snapshot.Action);
        Assert.Equal("update", update.Action);
#pragma warning disable CS0618
        Assert.Equal(0L, snapshotBook.Checksum);
        Assert.Equal(0L, updateBook.Checksum);
#pragma warning restore CS0618
        Assert.Equal(-1L, snapshotBook.PreviousSequenceId);
        Assert.Equal(123456L, snapshotBook.SequenceId);
        Assert.Equal(snapshotBook.SequenceId, updateBook.PreviousSequenceId);
        Assert.Equal(123457L, updateBook.SequenceId);
        Assert.Equal(0m, Assert.Single(snapshotBook.Asks).NonRpiQuantity);
    }

    [Fact]
    public void ManualRpiFixture_ParsesNonRpiQuantityWithoutChecksum()
    {
        var response = Deserialize("ws-order-book-rpi.json");
        var book = Assert.Single(response.Data);
        var ask = Assert.Single(book.Asks);
        var bid = Assert.Single(book.Bids);

        Assert.Equal("books-rpi", response.Arguments!.Channel);
        Assert.Equal(10m, ask.Quantity);
        Assert.Equal(8m, ask.NonRpiQuantity);
        Assert.Equal(9m, bid.NonRpiQuantity);
#pragma warning disable CS0618
        Assert.Null(book.Checksum);
        Assert.Equal(8m, ask.LiquidatedOrders);
#pragma warning restore CS0618
        Assert.Equal(-1L, book.PreviousSequenceId);
        Assert.Equal(200L, book.SequenceId);
    }

    [Fact]
    public void DeprecatedCompatibilityMembers_AreMarkedObsolete()
    {
        Assert.NotNull(typeof(OkxPublicOrderBookStream).GetProperty("Checksum")!.GetCustomAttribute<ObsoleteAttribute>());
        Assert.NotNull(typeof(OkxPublicOrderBookRow).GetProperty("LiquidatedOrders")!.GetCustomAttribute<ObsoleteAttribute>());
        Assert.NotNull(typeof(OkxOrderBookType).GetField("OrderBook_ELP")!.GetCustomAttribute<ObsoleteAttribute>());
    }

    [Fact]
    public void OrderBookTypes_MapCurrentRpiAndTemporaryElpChannels()
    {
        Assert.Equal("books-rpi", MapConverter.GetString(OkxOrderBookType.OrderBook_RPI));
#pragma warning disable CS0618
        Assert.Equal("books-elp", MapConverter.GetString(OkxOrderBookType.OrderBook_ELP));
#pragma warning restore CS0618
    }

    [Theory]
    [InlineData(OkxOrderBookType.OrderBook, "books", false)]
    [InlineData(OkxOrderBookType.OrderBook_RPI, "books-rpi", false)]
    [InlineData(OkxOrderBookType.OrderBook_50_l2_TBT, "books50-l2-tbt", true)]
    [InlineData(OkxOrderBookType.OrderBook_l2_TBT, "books-l2-tbt", true)]
    public void OrderBookSubscription_UsesCurrentChannelAndAuthentication(
        OkxOrderBookType orderBookType,
        string channel,
        bool authenticated)
    {
        var method = typeof(OkxPublicSocketClient).GetMethod(
            "CreateOrderBookSubscription",
            BindingFlags.NonPublic | BindingFlags.Static);
        Assert.NotNull(method);

        var result = method!.Invoke(null, [new[] { "BTC-USDT" }, orderBookType]);
        var tuple = Assert.IsAssignableFrom<ITuple>(result);

        Assert.Equal("Public", tuple[0]?.ToString());
        Assert.Equal(authenticated, Assert.IsType<bool>(tuple[1]));
        var request = Assert.IsType<OkxSocketRequest>(tuple[2]);
        var argument = Assert.Single(request.Arguments);
        Assert.Equal(channel, argument.Channel);
        Assert.Equal("BTC-USDT", argument.InstrumentId);
    }

    [Fact]
    public async Task OrderBookSubscription_RejectsInvalidRequestShapesBeforeConnecting()
    {
        using var client = new OkxWebSocketApiClient();

        await Assert.ThrowsAsync<ArgumentNullException>(() =>
            client.Public.SubscribeToOrderBookAsync(null!, ["BTC-USDT"], OkxOrderBookType.OrderBook));
        await Assert.ThrowsAsync<ArgumentNullException>(() =>
            client.Public.SubscribeToOrderBookAsync(_ => { }, (IEnumerable<string>)null!, OkxOrderBookType.OrderBook));
        await Assert.ThrowsAsync<ArgumentException>(() =>
            client.Public.SubscribeToOrderBookAsync(_ => { }, [], OkxOrderBookType.OrderBook));
        await Assert.ThrowsAsync<ArgumentException>(() =>
            client.Public.SubscribeToOrderBookAsync(_ => { }, [" "], OkxOrderBookType.OrderBook));
        await Assert.ThrowsAsync<ArgumentOutOfRangeException>(() =>
            client.Public.SubscribeToOrderBookAsync(_ => { }, ["BTC-USDT"], (OkxOrderBookType)byte.MaxValue));
    }

    private static OkxSocketOrderBookUpdate Deserialize(string fixtureName)
    {
        var token = JObject.Parse(FixtureReader.ReadManual("Public", fixtureName));
        var response = token.ToObject<OkxSocketOrderBookUpdate>(Newtonsoft.Json.JsonSerializer.Create(SerializerOptions.WithConverters));
        return Assert.IsType<OkxSocketOrderBookUpdate>(response);
    }
}
