using System.Net;
using ApiSharp.Models;
using ApiSharp.Throttling;
using Newtonsoft.Json.Linq;
using OKX.Api.Account;
using OKX.Api.Tests.TestInfrastructure;

namespace OKX.Api.Tests.Account;

public class OkxAccountFeatureActivationTests
{
    private const string Endpoint = "/api/v5/account/activate-feature";

    [Fact]
    public async Task ActivateFeature_SendsSignedStringFeatureAndAcceptsEmptySuccess()
    {
        using var server = Server();
        var result = await Client(server).Account.ActivateFeatureAsync(OkxAccountFeature.UsdcOrderBookTrading);

        Assert.True(result.Success, result.Error?.ToString());
        Assert.IsType<RestCallResult>(result);
        Assert.Equal(HttpStatusCode.OK, result.Response?.StatusCode);
        Assert.Equal(HttpMethod.Post, result.Request?.Method);
        var request = Assert.Single(server.Requests);
        Assert.Equal("POST", request.Method);
        Assert.Equal(Endpoint, request.Path);
        Assert.Empty(request.Query);
        Assert.Equal(request.Body, result.Request?.Body);
        var payload = JObject.Parse(request.Body);
        Assert.Single(payload.Properties());
        Assert.Equal(JTokenType.String, payload["feature"]?.Type);
        Assert.Equal("1", payload["feature"]?.Value<string>());
        Assert.Equal("key", request.Headers["OK-ACCESS-KEY"]);
        Assert.Equal("pass", request.Headers["OK-ACCESS-PASSPHRASE"]);
        Assert.False(string.IsNullOrEmpty(request.Headers["OK-ACCESS-SIGN"]));
        Assert.False(string.IsNullOrEmpty(request.Headers["OK-ACCESS-TIMESTAMP"]));
    }

    [Theory]
    [InlineData(0)]
    [InlineData(2)]
    [InlineData(255)]
    public async Task ActivateFeature_RejectsUndocumentedValuesBeforeSending(byte feature)
    {
        using var server = Server();
        var error = await Assert.ThrowsAsync<ArgumentOutOfRangeException>(() =>
            Client(server).Account.ActivateFeatureAsync((OkxAccountFeature)feature));
        Assert.Equal("feature", error.ParamName);
        Assert.Empty(server.Requests);
    }

    [Theory]
    [InlineData(51773, "Feature not available in your region")]
    [InlineData(50011, "Requests too frequent")]
    public async Task ActivateFeature_PreservesNumericFailureAndDoesNotRetry(int code, string message)
    {
        using var server = Server(new JObject
        {
            ["code"] = code.ToString(), ["msg"] = message, ["data"] = new JArray(),
        }.ToString());

        var result = await Client(server).Account.ActivateFeatureAsync(OkxAccountFeature.UsdcOrderBookTrading);

        Assert.False(result.Success);
        Assert.IsType<ServerError>(result.Error);
        Assert.Equal(code, result.Error.Code);
        Assert.Equal(message, result.Error.Message);
        Assert.Equal(HttpStatusCode.OK, result.Response?.StatusCode);
        Assert.Single(server.Requests);
    }

    [Fact]
    public async Task ActivateFeature_RequiresCredentialsBeforeSending()
    {
        using var server = Server();
        var client = new OkxRestApiClient(new OkxRestApiOptions { BaseAddress = server.BaseAddress, AutoTimestamp = false });
        var error = await Assert.ThrowsAsync<ArgumentException>(() =>
            client.Account.ActivateFeatureAsync(OkxAccountFeature.UsdcOrderBookTrading));
        Assert.Contains("No valid API credentials", error.Message); // Existing root-client authentication convention.
        Assert.Empty(server.Requests);
    }

    [Fact]
    public async Task ActivateFeature_HonorsPreCanceledTokenWithoutSending()
    {
        using var server = Server();
        using var cancellation = new CancellationTokenSource();
        cancellation.Cancel();
        var result = await Client(server).Account.ActivateFeatureAsync(OkxAccountFeature.UsdcOrderBookTrading, cancellation.Token);
        Assert.False(result.Success);
        Assert.IsType<CancellationRequestedError>(result.Error);
        Assert.Empty(server.Requests);
    }

    [Fact]
    public async Task ActivateFeature_EnforcesFivePerTwoSecondsSeparatelyFromAccountQueries()
    {
        using var server = Server();
        var client = Client(server, RateLimitingBehavior.Fail);
        for (var i = 0; i < 5; i++)
            Assert.True((await client.Account.ActivateFeatureAsync(OkxAccountFeature.UsdcOrderBookTrading)).Success);

        var blocked = await client.Account.ActivateFeatureAsync(OkxAccountFeature.UsdcOrderBookTrading);
        Assert.False(blocked.Success);
        Assert.IsType<ClientRateLimitError>(blocked.Error);
        Assert.True((await client.Account.GetBalancesAsync()).Success);
        Assert.Equal(6, server.Requests.Count);
        Assert.Equal(5, server.Requests.Count(request => request.Path == Endpoint));
    }

    private static LocalOkxRestServer Server(string? response = null) => new(new Dictionary<string, string>
    {
        [$"POST {Endpoint}"] = response ?? FixtureReader.ReadManual("Account", "activate-feature.json"),
        ["GET /api/v5/account/balance"] = "{\"code\":\"0\",\"msg\":\"\",\"data\":[]}",
    });

    private static OkxRestApiClient Client(LocalOkxRestServer server, RateLimitingBehavior behavior = RateLimitingBehavior.Wait)
        => new(new OkxRestApiOptions(new OkxApiCredentials("key", "secret", "pass"))
        {
            BaseAddress = server.BaseAddress, AutoTimestamp = false, RateLimitingBehavior = behavior,
        });
}
