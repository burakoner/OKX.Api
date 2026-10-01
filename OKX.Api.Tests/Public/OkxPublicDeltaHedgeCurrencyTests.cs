using ApiSharp.Models;
using ApiSharp.Throttling;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OKX.Api.Base;
using OKX.Api.Public;
using OKX.Api.Tests.TestInfrastructure;

namespace OKX.Api.Tests.Public;

public class OkxPublicDeltaHedgeCurrencyTests
{
    private const string Endpoint = "/api/v5/public/delta-hedge-currencies";

    [Fact]
    public void OfficialExample_ParsesEveryMappingWithoutSynthesizingReverseEntries()
    {
        var response = JsonConvert.DeserializeObject<OkxRestApiResponse<List<OkxPublicDeltaHedgeCurrency>>>(
            FixtureReader.ReadManual("Public", "get-delta-hedge-currencies.json"));

        Assert.NotNull(response?.Data);
        Assert.Equal(0, response.ErrorCode);
        Assert.Equal(new[] { "ETH", "XAU", "AAPL" }, response.Data.Select(item => item.Currency));
        Assert.Equal(new[] { "BETH" }, response.Data[0].HedgeCurrencies);
        Assert.Equal(new[] { "XAUT" }, response.Data[1].HedgeCurrencies);
        Assert.Equal(new[] { "XAAPL" }, response.Data[2].HedgeCurrencies);
    }

    [Fact]
    public void Model_SerializesDocumentedStringArrayFields()
    {
        var item = new OkxPublicDeltaHedgeCurrency { Currency = "ETH", HedgeCurrencies = ["BETH", "NEW-ETH"] };

        var json = JObject.Parse(JsonConvert.SerializeObject(item));

        Assert.Equal(new[] { "ccy", "hedgeCcy" }, json.Properties().Select(property => property.Name));
        Assert.Equal("ETH", json["ccy"]?.Value<string>());
        Assert.Equal(new[] { "BETH", "NEW-ETH" }, json["hedgeCcy"]!.Values<string>());
    }

    [Theory]
    [InlineData(false)]
    [InlineData(true)]
    public async Task GetDeltaHedgeCurrencies_SendsUnsignedGetWithoutUnspecifiedFilter(bool withCredentials)
    {
        using var server = CreateServer();
        var client = CreateClient(server, withCredentials: withCredentials);

        var result = await client.Public.GetDeltaHedgeCurrenciesAsync();

        Assert.True(result.Success, result.Error?.ToString());
        Assert.Equal(3, result.Data.Count);
        var request = Assert.Single(server.Requests);
        Assert.Equal("GET", request.Method);
        Assert.Equal(Endpoint, request.Path);
        Assert.Equal(string.Empty, request.Query);
        Assert.Equal(string.Empty, request.Body);
        Assert.DoesNotContain(request.Headers.Keys, key => key.StartsWith("OK-ACCESS-", StringComparison.OrdinalIgnoreCase));
    }

    [Theory]
    [InlineData("ETH")]
    [InlineData("BETH")]
    [InlineData("AAPL")]
    [InlineData("NEW-TOKEN")]
    public async Task GetDeltaHedgeCurrencies_ForwardsCurrencyFilterWithoutHardcodedCurrencyCatalog(string currency)
    {
        // Synthetic response: the server determines the filtered entry, including reverse-side currencies.
        var payload = new JObject
        {
            ["code"] = "0",
            ["msg"] = "",
            ["data"] = new JArray(new JObject { ["ccy"] = currency, ["hedgeCcy"] = new JArray("OTHER-TOKEN") }),
        }.ToString();
        using var server = CreateServer(payload);

        var result = await CreateClient(server).Public.GetDeltaHedgeCurrenciesAsync(currency);

        Assert.True(result.Success, result.Error?.ToString());
        Assert.Equal($"?ccy={currency}", Assert.Single(server.Requests).Query);
        var mapping = Assert.Single(result.Data);
        Assert.Equal(currency, mapping.Currency);
        Assert.Equal(new[] { "OTHER-TOKEN" }, mapping.HedgeCurrencies);
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData("\t")]
    public async Task GetDeltaHedgeCurrencies_RejectsExplicitEmptyFilterBeforeSending(string currency)
    {
        using var server = CreateServer();
        var client = CreateClient(server);

        var error = await Assert.ThrowsAsync<ArgumentException>(() => client.Public.GetDeltaHedgeCurrenciesAsync(currency));

        Assert.Equal("currency", error.ParamName);
        Assert.Empty(server.Requests);
    }

    [Fact]
    public async Task GetDeltaHedgeCurrencies_PreservesEmptyMappingResponse()
    {
        using var server = CreateServer("{\"code\":\"0\",\"msg\":\"\",\"data\":[]}");

        var result = await CreateClient(server).Public.GetDeltaHedgeCurrenciesAsync("UNMAPPED-TOKEN");

        Assert.True(result.Success, result.Error?.ToString());
        Assert.Empty(result.Data);
        Assert.Single(server.Requests);
    }

    [Fact]
    public async Task GetDeltaHedgeCurrencies_PreservesMultipleAndEmptyHedgeArraysAndUnknownCurrencyNames()
    {
        // Synthetic forward-compatibility shape, not a claim about the current production mappings.
        using var server = CreateServer("""
            {"code":"0","msg":"","data":[
              {"ccy":"NEW","hedgeCcy":["TOKEN-B","TOKEN-A"],"futureField":"ignored"},
              {"ccy":"EMPTY","hedgeCcy":[]}
            ]}
            """);

        var result = await CreateClient(server).Public.GetDeltaHedgeCurrenciesAsync();

        Assert.True(result.Success, result.Error?.ToString());
        Assert.Equal(2, result.Data.Count);
        Assert.Equal("NEW", result.Data[0].Currency);
        Assert.Equal(new[] { "TOKEN-B", "TOKEN-A" }, result.Data[0].HedgeCurrencies);
        Assert.Equal("EMPTY", result.Data[1].Currency);
        Assert.Empty(result.Data[1].HedgeCurrencies);
    }

    [Fact]
    public async Task GetDeltaHedgeCurrencies_ReturnsNumericServerErrorWithoutRetrying()
    {
        using var server = CreateServer("{\"code\":\"50000\",\"msg\":\"Synthetic rejection\",\"data\":[]}");

        var result = await CreateClient(server).Public.GetDeltaHedgeCurrenciesAsync("ETH");

        Assert.False(result.Success);
        var error = Assert.IsType<ServerError>(result.Error);
        Assert.Equal(50000, error.Code);
        Assert.Equal("Synthetic rejection", error.Message);
        Assert.Null(result.Data);
        Assert.Single(server.Requests);
    }

    [Fact]
    public async Task GetDeltaHedgeCurrencies_ForwardsCancellationWithoutSending()
    {
        using var server = CreateServer();
        using var cancellation = new CancellationTokenSource();
        cancellation.Cancel();

        var result = await CreateClient(server).Public.GetDeltaHedgeCurrenciesAsync(ct: cancellation.Token);

        Assert.False(result.Success);
        Assert.IsType<CancellationRequestedError>(result.Error);
        Assert.Empty(server.Requests);
    }

    [Fact]
    public async Task GetDeltaHedgeCurrencies_UsesTwentyPerTwoSecondBudgetAcrossFiltersIndependentOfOtherEndpoints()
    {
        using var server = new LocalOkxRestServer(new Dictionary<string, string>
        {
            [$"GET {Endpoint}"] = FixtureReader.ReadManual("Public", "get-delta-hedge-currencies.json"),
            ["GET /api/v5/public/event-contract/series"] = FixtureReader.ReadManual("Public", "get-event-contract-series.json"),
        });
        var client = CreateClient(server, RateLimitingBehavior.Fail);
        Assert.True((await client.Public.GetEventContractSeriesAsync()).Success);

        for (var i = 0; i < 20; i++)
        {
            var result = await client.Public.GetDeltaHedgeCurrenciesAsync(i % 2 == 0 ? null : "ETH");
            Assert.True(result.Success, result.Error?.ToString());
        }

        var limited = await client.Public.GetDeltaHedgeCurrenciesAsync("AAPL");
        Assert.False(limited.Success);
        Assert.IsType<ClientRateLimitError>(limited.Error);
        Assert.Equal(20, server.Requests.Count(request => request.Path == Endpoint));
        Assert.True((await client.Public.GetEventContractSeriesAsync()).Success);
    }

    private static LocalOkxRestServer CreateServer(string? payload = null)
        => new(new Dictionary<string, string>
        {
            [$"GET {Endpoint}"] = payload ?? FixtureReader.ReadManual("Public", "get-delta-hedge-currencies.json"),
        });

    private static OkxRestApiClient CreateClient(LocalOkxRestServer server,
        RateLimitingBehavior behavior = RateLimitingBehavior.Wait, bool withCredentials = false)
        => new(new OkxRestApiOptions(withCredentials ? new OkxApiCredentials("key", "secret", "pass") : null)
        {
            AutoTimestamp = false,
            BaseAddress = server.BaseAddress,
            RateLimitingBehavior = behavior,
        });
}
