namespace OKX.Api.Tests.TestInfrastructure;

internal sealed record TestConfiguration(
    bool RunIntegrationTests,
    bool UseDemoTrading,
    string? ApiKey,
    string? ApiSecret,
    string? ApiPassphrase,
    string PublicBorrowCurrency)
{
    public bool HasApiCredentials =>
        !string.IsNullOrWhiteSpace(ApiKey) &&
        !string.IsNullOrWhiteSpace(ApiSecret) &&
        !string.IsNullOrWhiteSpace(ApiPassphrase);

    public static TestConfiguration Load()
    {
        DotEnvLoader.Load();

        return new TestConfiguration(
            ReadBool("OKX_RUN_INTEGRATION_TESTS"),
            ReadBool("OKX_DEMO_TRADING", defaultValue: true),
            Environment.GetEnvironmentVariable("OKX_API_KEY"),
            Environment.GetEnvironmentVariable("OKX_API_SECRET"),
            Environment.GetEnvironmentVariable("OKX_API_PASSPHRASE"),
            Environment.GetEnvironmentVariable("OKX_PUBLIC_BORROW_CURRENCY") ?? "BTC");
    }

    private static bool ReadBool(string variableName, bool defaultValue = false)
    {
        var value = Environment.GetEnvironmentVariable(variableName);
        if (string.IsNullOrWhiteSpace(value))
            return defaultValue;

        return value.Trim().ToLowerInvariant() switch
        {
            "1" or "true" or "yes" or "on" => true,
            "0" or "false" or "no" or "off" => false,
            _ => defaultValue,
        };
    }
}
