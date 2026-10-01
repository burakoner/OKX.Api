using ApiSharp.Models;
using ApiSharp.WebSocket;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OKX.Api.Base;
using OKX.Api.Common;
using OKX.Api.Public;
using OKX.Api.Tests.TestInfrastructure;

namespace OKX.Api.Tests.Public;

// Synthetic payloads exercise the real subscription callback/routing, without a network connection.
public class OkxInstrumentIncrementalContractTests
{
    [Theory]
    [InlineData(OkxInstrumentType.Spot, "SPOT")]
    [InlineData(OkxInstrumentType.Margin, "MARGIN")]
    [InlineData(OkxInstrumentType.Swap, "SWAP")]
    [InlineData(OkxInstrumentType.Futures, "FUTURES")]
    [InlineData(OkxInstrumentType.Option, "OPTION")]
    [InlineData(OkxInstrumentType.Events, "EVENTS")]
    public async Task Subscription_UsesCurrentPublicTypeContract(OkxInstrumentType type, string wireType)
    {
        using var client = new RecordingClient();
        using var cancellation = new CancellationTokenSource();
        var received = new List<OkxPublicInstrument>();
        await client.Public.SubscribeToInstrumentsAsync(received.Add, type, cancellation.Token);

        Assert.Equal(OkxSocketEndpoint.Public, client.Endpoint);
        Assert.False(client.Authenticated);
        Assert.Equal(cancellation.Token, client.Cancellation);
        var wire = JObject.Parse(JsonConvert.SerializeObject(client.Request, SerializerOptions.WithConverters));
        Assert.Equal("subscribe", wire["op"]?.Value<string>());
        var argument = Assert.Single(Assert.IsType<JArray>(wire["args"]));
        Assert.Equal("instruments", argument["channel"]?.Value<string>());
        Assert.Equal(wireType, argument["instType"]?.Value<string>());
        var push = Fixture();
        push["arg"]!["instType"] = wireType;
        push["data"]![0]!["instType"] = wireType;
        Assert.True(client.Publish(push));
        Assert.Equal(type, Assert.Single(received).InstrumentType);
    }

    [Fact]
    public async Task SubsetPush_UpdatesByIdWithoutReplacingOtherConsumerEntriesOrSynthesizingChanges()
    {
        using var client = new RecordingClient();
        var other = new OkxPublicInstrument { InstrumentId = "ETH-USDC", TickSize = 0.01m };
        var catalog = new Dictionary<string, OkxPublicInstrument> { [other.InstrumentId] = other };
        var received = new List<OkxPublicInstrument>();
        await client.Public.SubscribeToInstrumentsAsync(item =>
        {
            received.Add(item);
            catalog[item.InstrumentId] = item; // Application-owned upsert, not full-list replacement.
        }, OkxInstrumentType.Spot);
        Assert.Empty(received); // No invented initial list.
        Assert.True(client.Publish(Fixture()));

        var btc = Assert.Single(received);
        Assert.Equal(2, catalog.Count);
        Assert.Same(other, catalog[other.InstrumentId]);
        Assert.Equal("BTC-USDC", btc.InstrumentId);
        Assert.Equal(9000000001L, btc.InstrumentIdCode);
        Assert.Equal("USDC", btc.QuoteCurrency);
        Assert.Equal(0.1m, btc.TickSize);
        Assert.Equal(0.00001m, btc.MinimumOrderSize);
        Assert.Equal(100m, btc.MaximumMarketOrderSize);
        Assert.Null(btc.ExpiryTimestamp);
        Assert.Null(btc.AuctionEndTimestamp);
        Assert.Equal(new[] { "tickSz", "minSz", "maxMktSz" }, btc.UpcomingChanges!.Select(change => change.Parameter));
        Assert.Equal(new[] { "0.01", "0.0001", "200" }, btc.UpcomingChanges!.Select(change => change.NewValue));
        Assert.All(btc.UpcomingChanges!, change => Assert.Equal(1790899200000, change.EffectiveTimestamp));

        var next = Fixture();
        next["data"]![0]!["tickSz"] = "0.01";
        next["data"]![0]!["upcChg"] = new JArray();
        Assert.True(client.Publish(next));
        Assert.Equal(2, received.Count);
        Assert.Equal(0.01m, catalog["BTC-USDC"].TickSize);
        Assert.Empty(catalog["BTC-USDC"].UpcomingChanges!);
        Assert.Equal(0.1m, btc.TickSize); // Earlier callback object is not mutated.
        Assert.Same(other, catalog[other.InstrumentId]);
    }

    [Fact]
    public async Task ExpiredAndNewIds_KeepActualIndependentCodesAndDoNotInferAMigrationAlias()
    {
        using var client = new RecordingClient();
        var received = new List<OkxPublicInstrument>();
        await client.Public.SubscribeToInstrumentsAsync(received.Add, OkxInstrumentType.Spot);
        var old = Fixture();
        old["data"]![0]!["instId"] = "BTC-USD";
        old["data"]![0]!["instIdCode"] = "123456";
        old["data"]![0]!["quoteCcy"] = "USD";
        old["data"]![0]!["state"] = "expired";
        Assert.True(client.Publish(old));
        Assert.True(client.Publish(Fixture()));
        Assert.Equal(new[] { "BTC-USD", "BTC-USDC" }, received.Select(item => item.InstrumentId));
        Assert.Equal(new long?[] { 123456, 9000000001 }, received.Select(item => item.InstrumentIdCode));
        Assert.Equal(OkxInstrumentState.Expired, received[0].State);
        Assert.Equal(OkxInstrumentState.Live, received[1].State);
    }

    [Fact]
    public async Task MultiTypeSubsetAndEmptyPushes_RouteOnlySubscribedTypesWithoutDeletingOrDeduplicating()
    {
        using var client = new RecordingClient();
        var received = new List<OkxPublicInstrument>();
        await client.Public.SubscribeToInstrumentsAsync(received.Add, new[] { OkxInstrumentType.Spot, OkxInstrumentType.Swap });
        Assert.Equal(2, client.Request!.Arguments.Count());
        var spot = Fixture();
        var row = spot["data"]![0]!.DeepClone();
        spot["data"] = new JArray(row, row.DeepClone());
        Assert.True(client.Publish(spot));
        Assert.Equal(2, received.Count); // No wrapper deduplication.
        var swap = Fixture();
        swap["arg"]!["instType"] = "SWAP";
        swap["data"]![0]!["instType"] = "SWAP";
        swap["data"]![0]!["instId"] = "BTC-USDT-SWAP";
        Assert.True(client.Publish(swap));
        var empty = Fixture();
        empty["data"] = new JArray();
        Assert.False(client.Publish(empty)); // Existing dispatcher ignores empty data, with no callbacks/deletions.
        var unsubscribed = Fixture();
        unsubscribed["arg"]!["instType"] = "OPTION";
        Assert.False(client.Publish(unsubscribed));
        var wrongChannel = Fixture();
        wrongChannel["arg"]!["channel"] = "tickers";
        Assert.False(client.Publish(wrongChannel));
        Assert.Equal(3, received.Count);
    }

    [Fact]
    public async Task AnnouncedListing_EmptyFieldsArePreservedRatherThanFilledFromAnOldRecord()
    {
        using var client = new RecordingClient();
        var received = new List<OkxPublicInstrument>();
        await client.Public.SubscribeToInstrumentsAsync(received.Add, OkxInstrumentType.Spot);
        var push = Fixture();
        push["data"] = new JArray(new JObject
        {
            ["instType"] = "SPOT", ["instId"] = "NEW-USDC", ["state"] = "preopen", ["listTime"] = "1790899200000",
            ["instIdCode"] = "", ["tickSz"] = "", ["minSz"] = "", ["maxMktSz"] = "", ["expTime"] = "", ["upcChg"] = new JArray(),
        });
        Assert.True(client.Publish(push));
        var listing = Assert.Single(received);
        Assert.Equal(OkxInstrumentState.PreOpen, listing.State);
        Assert.Null(listing.InstrumentIdCode);
        Assert.Null(listing.TickSize);
        Assert.Null(listing.MinimumOrderSize);
        Assert.Null(listing.MaximumMarketOrderSize);
        Assert.Equal(1790899200000, listing.ListingTimestamp);
    }

    private static JObject Fixture() => JObject.Parse(FixtureReader.ReadManual("Public", "ws-instruments-incremental.json"));

    private sealed class RecordingClient : OkxWebSocketApiClient
    {
        private Action<JObject>? _publish;
        public OkxSocketRequest? Request { get; private set; }
        public OkxSocketEndpoint Endpoint { get; private set; }
        public bool Authenticated { get; private set; }
        public CancellationToken Cancellation { get; private set; }

        internal override Task<CallResult<WebSocketUpdateSubscription>> RootSubscribeAsync<T>(
            OkxSocketEndpoint endpoint, object request, string? identifier, bool authenticated,
            Action<WebSocketDataEvent<T>> dataHandler, CancellationToken ct)
        {
            Endpoint = endpoint;
            Request = Assert.IsType<OkxSocketRequest>(request);
            Authenticated = authenticated;
            Cancellation = ct;
            _publish = message => dataHandler(new WebSocketDataEvent<T>(
                message.ToObject<T>(JsonSerializer.Create(SerializerOptions.WithConverters))!, DateTime.UnixEpoch));
            return Task.FromResult(new CallResult<WebSocketUpdateSubscription>(new ServerError("Test-only subscription; no network")));
        }

        public bool Publish(JObject message)
        {
            if (!base.MessageMatchesHandler(null!, message, Request!)) return false;
            _publish!(message);
            return true;
        }
    }
}
