using ApiSharp.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OKX.Api.Account;
using OKX.Api.Base;
using OKX.Api.Common;
using System.Reflection;

namespace OKX.Api.Tests.Account;

public class OkxAccountSocketContractTests
{
    [Fact]
    public void AccountSubscriptionRequest_SerializesUpdateIntervalAsStringifiedJson()
    {
        var method = typeof(OkxAccountSocketClient).GetMethod("CreateAccountSubscriptionRequest", BindingFlags.NonPublic | BindingFlags.Static);
        Assert.NotNull(method);

        var request = Assert.IsType<OkxSocketRequest>(method!.Invoke(null, ["BTC", 0]));
        var argument = ParseSingleArgument(request);

        Assert.Equal("account", argument["channel"]?.Value<string>());
        Assert.Equal("BTC", argument["ccy"]?.Value<string>());
        Assert.Equal("{\"updateInterval\":\"0\"}", argument["extraParams"]?.Value<string>());
    }

    [Fact]
    public void PositionSubscriptionRequest_SerializesUpdateIntervalForEachArgument()
    {
        var method = typeof(OkxAccountSocketClient).GetMethod("CreatePositionSubscriptionRequest", BindingFlags.NonPublic | BindingFlags.Static);
        Assert.NotNull(method);

        var request = Assert.IsType<OkxSocketRequest>(method!.Invoke(null, [new[] { new OkxSocketSymbolRequest(OkxInstrumentType.Futures, "BTC-USD", null) }, 2000]));
        var argument = ParseSingleArgument(request);

        Assert.Equal("positions", argument["channel"]?.Value<string>());
        Assert.Equal("FUTURES", argument["instType"]?.Value<string>());
        Assert.Equal("BTC-USD", argument["instFamily"]?.Value<string>());
        Assert.Equal("{\"updateInterval\":\"2000\"}", argument["extraParams"]?.Value<string>());
    }

    [Fact]
    public void PositionRiskSubscriptionRequest_IncludesInstrumentFilters()
    {
        var method = typeof(OkxAccountSocketClient).GetMethod("CreatePositionRiskSubscriptionRequest", BindingFlags.NonPublic | BindingFlags.Static);
        Assert.NotNull(method);

        var request = Assert.IsType<OkxSocketRequest>(method!.Invoke(null, [OkxInstrumentType.Swap, "BTC-USDT", "BTC-USDT-SWAP"]));
        var argument = ParseSingleArgument(request);

        Assert.Equal("liquidation-warning", argument["channel"]?.Value<string>());
        Assert.Equal("SWAP", argument["instType"]?.Value<string>());
        Assert.Equal("BTC-USDT", argument["instFamily"]?.Value<string>());
        Assert.Equal("BTC-USDT-SWAP", argument["instId"]?.Value<string>());
    }

    [Fact]
    public void AccountGreeksSubscriptionRequest_IncludesOptionalCurrency()
    {
        var method = typeof(OkxAccountSocketClient).GetMethod("CreateAccountGreeksSubscriptionRequest", BindingFlags.NonPublic | BindingFlags.Static);
        Assert.NotNull(method);

        var request = Assert.IsType<OkxSocketRequest>(method!.Invoke(null, ["BTC"]));
        var argument = ParseSingleArgument(request);

        Assert.Equal("account-greeks", argument["channel"]?.Value<string>());
        Assert.Equal("BTC", argument["ccy"]?.Value<string>());
    }

    private static JObject ParseSingleArgument(OkxSocketRequest request)
    {
        var json = JsonConvert.SerializeObject(request, SerializerOptions.WithConverters);
        return (JObject)JObject.Parse(json)["args"]![0]!;
    }
}
