using ApiSharp.Throttling;

namespace OKX.Api.Tests.TestInfrastructure;

internal static class OkxRestClientFactory
{
    private const int MaxIntegrationRequestsPerSecond = 5;
    private static readonly TimeSpan IntegrationRateLimitWindow = TimeSpan.FromSeconds(1);

    public static OkxRestApiClient CreatePublic(TestConfiguration configuration)
    {
        var options = CreateOptions(configuration);
        return new OkxRestApiClient(options);
    }

    public static OkxRestApiClient CreatePrivate(TestConfiguration configuration)
    {
        if (!configuration.HasApiCredentials)
            throw new InvalidOperationException("Private integration tests require API credentials.");

        var options = CreateOptions(configuration);
        var client = new OkxRestApiClient(options);
        client.SetApiCredentials(configuration.ApiKey!, configuration.ApiSecret!, configuration.ApiPassphrase!);
        return client;
    }

    private static OkxRestApiOptions CreateOptions(TestConfiguration configuration)
    {
        var options = new OkxRestApiOptions
        {
            DemoTradingService = configuration.UseDemoTrading,
        };

        // Keep live integration traffic comfortably below OKX's public/private per-IP thresholds.
#pragma warning disable CS0612 // Type or member is obsolete
        options.RateLimiters.Add(new RateLimiter().AddTotalRateLimit(MaxIntegrationRequestsPerSecond, IntegrationRateLimitWindow, false));
#pragma warning restore CS0612 // Type or member is obsolete

        return options;
    }
}
