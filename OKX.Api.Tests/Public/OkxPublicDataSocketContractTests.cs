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
    public void ManualAdlWarningSocketFixture_ParsesCurrentEmptyDeprecatedFields()
    {
        var response = DeserializeSocket<OkxPublicAdlWarning>("Public", "ws-adl-warning.json");

        var warning = Assert.Single(response);
        Assert.Equal(OkxPublicAdlState.warning, warning.State);
        Assert.Equal(280784384.9564228289548144m, warning.Balance);
        Assert.Equal(1700210763001L, warning.Timestamp);
#pragma warning disable CS0612, CS0618
        Assert.Equal(string.Empty, warning.Currency);
        Assert.Equal(string.Empty, warning.InstrumentId);
        Assert.Null(warning.MaximumBalance);
        Assert.Null(warning.MaximumBalanceTimestamp);
        Assert.Null(warning.AdlType);
        Assert.Null(warning.AdlBalance);
        Assert.Null(warning.AdlRecordBalance);
        Assert.Null(warning.DeclineRate);
        Assert.Null(warning.AdlRate);
        Assert.Null(warning.AdlRecoveryRate);
#pragma warning restore CS0612, CS0618
    }

    [Fact]
    public void ManualInstrumentsSocketFixture_ParsesPostOnlyState()
    {
        var response = DeserializeSocket<OkxPublicInstrument>("Public", "ws-instruments-post-only-state.json");

        var instrument = Assert.Single(response);
        Assert.Equal("BTC-USDT-SWAP", instrument.InstrumentId);
        Assert.Equal(OkxInstrumentState.PostOnly, instrument.State);
    }

    [Fact]
    public void ManualInstrumentsSocketFixture_ParsesSpacexRenameSequenceWithoutChangingInstrumentCode()
    {
        var response = DeserializeSocket<OkxPublicInstrument>("Public", "ws-instruments-spacex-rename.json");

        Assert.Collection(
            response,
            expired =>
            {
                Assert.Equal("SPACEX-USDT-SWAP", expired.InstrumentId);
                Assert.Equal("SPACEX-USDT", expired.InstrumentFamily);
                Assert.Equal("SPACEX", expired.ContractValueCurrency);
                Assert.Equal(OkxInstrumentState.Expired, expired.State);
            },
            rebase =>
            {
                Assert.Equal("SPCX-USDT-SWAP", rebase.InstrumentId);
                Assert.Equal(OkxInstrumentState.Rebase, rebase.State);
            },
            postOnly =>
            {
                Assert.Equal("SPCX-USDT-SWAP", postOnly.InstrumentId);
                Assert.Equal(OkxInstrumentState.PostOnly, postOnly.State);
            },
            live =>
            {
                Assert.Equal("SPCX-USDT-SWAP", live.InstrumentId);
                Assert.Equal("SPCX-USDT", live.Underlying);
                Assert.Equal("SPCX", live.ContractValueCurrency);
                Assert.Equal(OkxInstrumentState.Live, live.State);
            });

        Assert.All(response, instrument =>
        {
            Assert.Equal("4", instrument.GroupId);
            Assert.Equal(260602001L, instrument.InstrumentIdCode);
        });
    }

    [Theory]
    [InlineData(OkxInstrumentType.Any)]
    [InlineData(OkxInstrumentType.Contracts)]
    public async Task InstrumentsSubscription_RejectsUndocumentedInstrumentTypes(OkxInstrumentType instrumentType)
    {
        using var client = new OkxWebSocketApiClient();

        await Assert.ThrowsAsync<ArgumentException>(() =>
            client.Public.SubscribeToInstrumentsAsync(_ => { }, instrumentType));
    }

    [Fact]
    public async Task InstrumentsSubscription_RejectsEmptyInstrumentTypeList()
    {
        using var client = new OkxWebSocketApiClient();

        await Assert.ThrowsAsync<ArgumentException>(() =>
            client.Public.SubscribeToInstrumentsAsync(_ => { }, []));
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
