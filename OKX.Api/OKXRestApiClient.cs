namespace OKX.Api;

public sealed class OKXRestApiClient
{
    // Options
    internal OKXRestApiClientOptions ClientOptions { get; }
    internal CultureInfo CI { get; } = CultureInfo.InvariantCulture;

    // Clients
    public OKXTradeRestApiClient Trade { get; }
    public OKXFundingRestApiClient Funding { get; }
    // TODO: Convert
    public OKXAccountRestApiClient Account { get; }
    public OKXSubAccountRestApiClient SubAccount { get; }
    // TODO: Grid Trading
    // TODO: Recurring Buy
    // TODO: Savings
    // TODO: Earn
    // TODO: Copy Trading
    // TODO: Block Trading
    public OKXMarketDataRestApiClient MarketData { get; }
    public OKXPublicDataRestApiClient PublicData { get; }
    public OKXTradingDataRestApiClient TradingData { get; }

    public OKXRestApiClient() : this(new OKXRestApiClientOptions())
    {
    }

    public OKXRestApiClient(OKXRestApiClientOptions options)
    {
        ClientOptions = options;

        Trade = new OKXTradeRestApiClient(this);
        Funding = new OKXFundingRestApiClient(this);
        Account = new OKXAccountRestApiClient(this);
        SubAccount = new OKXSubAccountRestApiClient(this);
        MarketData = new OKXMarketDataRestApiClient(this);
        PublicData = new OKXPublicDataRestApiClient(this);
        TradingData = new OKXTradingDataRestApiClient(this);
    }

    public void SetApiCredentials(string apiKey, string apiSecret, string passPhrase)
        => SetApiCredentials(new OkxApiCredentials(apiKey, apiSecret, passPhrase));

    public void SetApiCredentials(OkxApiCredentials credentials)
    {
        Trade.SetApiCredentials(credentials);
        Funding.SetApiCredentials(credentials);
        Account.SetApiCredentials(credentials);
        SubAccount.SetApiCredentials(credentials);
        MarketData.SetApiCredentials(credentials);
        PublicData.SetApiCredentials(credentials);
        TradingData.SetApiCredentials(credentials);
    }
}
