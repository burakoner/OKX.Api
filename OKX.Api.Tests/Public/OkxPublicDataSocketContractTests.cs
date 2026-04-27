using ApiSharp.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OKX.Api.Base;
using OKX.Api.Common;
using OKX.Api.Public;
using OKX.Api.Tests.TestInfrastructure;
using System.Reflection;

namespace OKX.Api.Tests.Public;

public class OkxPublicDataSocketContractTests
{
    [Fact]
    public void ManualLiquidationOrdersSocketFixture_ParsesSzField()
    {
        var response = DeserializeSocket<OkxPublicLiquidationOrder>("Public", "ws-liquidation-orders.json");

        var order = Assert.Single(response);
        var detail = Assert.Single(order.Details);
        Assert.Equal(12.5m, detail.Size);
    }

    [Fact]
    public void ManualAdlWarningSocketFixture_ParsesMaxBalTimestampSeparately()
    {
        var response = DeserializeSocket<OkxPublicAdlWarning>("Public", "ws-adl-warning.json");

        var warning = Assert.Single(response);
        Assert.Equal(1777284100000L, warning.MaximumBalanceTimestamp);
        Assert.Equal(1777284161402L, warning.Timestamp);
#pragma warning disable CS0612
        Assert.Equal(0.1m, warning.DeclineRate);
        Assert.Equal(0.2m, warning.AdlRate);
        Assert.Equal(0.05m, warning.AdlRecoveryRate);
#pragma warning restore CS0612
    }

    [Fact]
    public void EconomicCalendarSubscription_UsesAuthenticatedBusinessSocket()
    {
        var method = typeof(OkxPublicSocketClient).GetMethod("CreateEconomicCalendarSubscription", BindingFlags.NonPublic | BindingFlags.Static);
        Assert.NotNull(method);

        var subscription = method!.Invoke(null, null);
        Assert.NotNull(subscription);

        var endpoint = subscription!.GetType().GetField("Item1")!.GetValue(subscription);
        var authenticated = (bool)subscription.GetType().GetField("Item2")!.GetValue(subscription)!;
        var request = (OkxSocketRequest)subscription.GetType().GetField("Item3")!.GetValue(subscription)!;

        Assert.Equal("Business", endpoint?.ToString());
        Assert.True(authenticated);
        var argument = Assert.Single(request.Arguments);
        Assert.Equal("economic-calendar", argument.Channel);
    }

    private static List<T> DeserializeSocket<T>(params string[] fixturePath)
    {
        var json = FixtureReader.ReadManual(fixturePath);
        var token = JObject.Parse(json)["data"];

        Assert.NotNull(token);

        var response = token!.ToObject<List<T>>(JsonSerializer.Create(SerializerOptions.WithConverters));
        Assert.NotNull(response);
        return response!;
    }
}
