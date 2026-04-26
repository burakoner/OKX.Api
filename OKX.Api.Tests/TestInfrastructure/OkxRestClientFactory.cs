namespace OKX.Api.Tests.TestInfrastructure;

internal static class OkxRestClientFactory
{
    public static OkxRestApiClient CreatePublic(TestConfiguration configuration)
    {
        var options = new OkxRestApiOptions
        {
            DemoTradingService = configuration.UseDemoTrading,
        };

        return new OkxRestApiClient(options);
    }

    public static OkxRestApiClient CreatePrivate(TestConfiguration configuration)
    {
        if (!configuration.HasApiCredentials)
            throw new InvalidOperationException("Private integration tests require API credentials.");

        var options = new OkxRestApiOptions
        {
            DemoTradingService = configuration.UseDemoTrading,
        };

        var client = new OkxRestApiClient(options);
        client.SetApiCredentials(configuration.ApiKey!, configuration.ApiSecret!, configuration.ApiPassphrase!);
        return client;
    }
}
