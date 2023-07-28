namespace OKX.Api;

/// <summary>
/// OKX Rest API Client
/// </summary>
public sealed class OKXRestApiClient
{
    // Options
    internal OKXRestApiClientOptions ClientOptions { get; }

    /// <summary>
    /// Trade Client
    /// </summary>
    public OKXTradeRestApiClient Trade { get; }

    /// <summary>
    /// Funding Client
    /// </summary>
    public OKXFundingRestApiClient Funding { get; }

    /// <summary>
    /// Account Client
    /// </summary>
    public OKXAccountRestApiClient Account { get; }

    /// <summary>
    /// SubAccount Client
    /// </summary>
    public OKXSubAccountRestApiClient SubAccount { get; }

    /// <summary>
    /// GridTrading  Client
    /// </summary>
    public OKXGridTradingRestApiClient GridTrading { get; }

    /// <summary>
    /// MarketData Client
    /// </summary>
    public OKXMarketDataRestApiClient MarketData { get; }

    /// <summary>
    /// PublicData Client
    /// </summary>
    public OKXPublicDataRestApiClient PublicData { get; }

    /// <summary>
    /// TradingData Client
    /// </summary>
    public OKXTradingDataRestApiClient TradingData { get; }

    // TODO: Convert
    // TODO: Recurring Buy
    // TODO: Savings
    // TODO: Earn
    // TODO: Copy Trading
    // TODO: Block Trading
    // TODO: Spread Trading

    /// <summary>
    /// OKXRestApiClient Constructor
    /// </summary>
    public OKXRestApiClient() : this(new OKXRestApiClientOptions())
    {
    }

    /// <summary>
    /// OKXRestApiClient Constructor
    /// </summary>
    /// <param name="options">OKXRestApiClientOptions</param>
    public OKXRestApiClient(OKXRestApiClientOptions options)
    {
        ClientOptions = options;

        Trade = new OKXTradeRestApiClient(this);
        Funding = new OKXFundingRestApiClient(this);
        Account = new OKXAccountRestApiClient(this);
        SubAccount = new OKXSubAccountRestApiClient(this);
        GridTrading = new OKXGridTradingRestApiClient(this);
        MarketData = new OKXMarketDataRestApiClient(this);
        PublicData = new OKXPublicDataRestApiClient(this);
        TradingData = new OKXTradingDataRestApiClient(this);
    }

    /// <summary>
    /// Sets API Credentials
    /// </summary>
    /// <param name="apiKey">API Key</param>
    /// <param name="apiSecret">API Secret</param>
    /// <param name="passPhrase">API Pass Phrase</param>
    public void SetApiCredentials(string apiKey, string apiSecret, string passPhrase)
    {
        SetApiCredentials(new OkxApiCredentials(apiKey, apiSecret, passPhrase));
    }

    /// <summary>
    /// Sets API Credentials
    /// </summary>
    /// <param name="credentials">OkxApiCredentials Object</param>
    public void SetApiCredentials(OkxApiCredentials credentials)
    {
        Trade.SetApiCredentials(credentials);
        Funding.SetApiCredentials(credentials);
        Account.SetApiCredentials(credentials);
        SubAccount.SetApiCredentials(credentials);
        GridTrading.SetApiCredentials(credentials);
        MarketData.SetApiCredentials(credentials);
        PublicData.SetApiCredentials(credentials);
        TradingData.SetApiCredentials(credentials);
    }
}
