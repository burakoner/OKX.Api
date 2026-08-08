using OKX.Api.Broker;
using OKX.Api.Common;
using OKX.Api.Tests.TestInfrastructure;

namespace OKX.Api.Tests.Broker;

public class OkxFDBrokerClientBehaviorTests
{
    [Fact]
    public async Task GetDownloadLinksAsync_SendsCurrentFiltersAndParsesCompleteMetadata()
    {
        using var server = CreateServer("/api/v5/broker/fd/rebate-per-orders", "fd-download-links.json");
        var client = CreateClient(server);

        var result = await client.Broker.FD.GetDownloadLinksAsync(
            allHistory: false,
            begin: new DateTime(2026, 5, 1),
            end: new DateTime(2026, 5, 14),
            brokerType: OkxBrokerType.OAuth);

        Assert.True(result.Success, result.Error?.ToString());
        var link = Assert.Single(result.Data!);
        Assert.Equal("https://example.invalid/fd-rebate.csv", link.DownloadLink);
        Assert.Equal(1777593600000, link.BeginTimestamp);
        Assert.Equal(1778716800000, link.EndTimestamp);
        Assert.Equal(1778720400000, link.CreateTimestamp);
        Assert.Equal(1778724000000, link.Timestamp);
        Assert.Equal(OkxDownloadLinkState.Finished, link.State);

        var query = GetDecodedQuery(server);
        Assert.Contains("type=false", query);
        Assert.Contains("begin=20260501", query);
        Assert.Contains("end=20260514", query);
        Assert.Contains("brokerType=oauth", query);
    }

    [Fact]
    public async Task GetRebateInformationAsync_ParsesMsaStatusAndNullableLastRebate()
    {
        using var server = CreateServer("/api/v5/broker/fd/if-rebate", "fd-rebate-information.json");
        var client = CreateClient(server);

        var result = await client.Broker.FD.GetRebateInformationAsync("user-api-key", OkxBrokerType.Api);

        Assert.True(result.Success, result.Error?.ToString());
        Assert.Equal(OkxFDBrokerRebateStatus.MsaNotEligible, result.Data!.Status);
        Assert.Equal("6099c63a8d75SCDE", result.Data.BrokerCode);
        Assert.False(result.Data.IsAffiliated);
        Assert.Equal(0m, result.Data.ClientRebateRatio);
        Assert.Null(result.Data.LastRebate);

        var query = GetDecodedQuery(server);
        Assert.Contains("apiKey=user-api-key", query);
        Assert.Contains("brokerType=api", query);
    }

    [Fact]
    public async Task GetDownloadLinksAsync_RequiresDatesForSpecifiedHistory()
    {
        using var server = CreateServer("/api/v5/broker/fd/rebate-per-orders", "fd-download-links.json");
        var client = CreateClient(server);

        await Assert.ThrowsAsync<ArgumentException>(() => client.Broker.FD.GetDownloadLinksAsync(allHistory: false));

        Assert.Empty(server.Requests);
    }

    [Fact]
    public async Task GetRebateInformationAsync_RejectsEmptyApiKey()
    {
        using var server = CreateServer("/api/v5/broker/fd/if-rebate", "fd-rebate-information.json");
        var client = CreateClient(server);

        await Assert.ThrowsAsync<ArgumentException>(() => client.Broker.FD.GetRebateInformationAsync(" "));

        Assert.Empty(server.Requests);
    }

    private static string GetDecodedQuery(LocalOkxRestServer server)
        => Uri.UnescapeDataString(Assert.Single(server.Requests).Query);

    private static LocalOkxRestServer CreateServer(string path, string fixtureName)
        => new(new Dictionary<string, string>
        {
            [$"GET {path}"] = FixtureReader.ReadManual("Broker", fixtureName),
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
