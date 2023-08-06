namespace OKX.Api;

/// <summary>
/// OKX Rest API Client
/// </summary>
public sealed class OKXRestApiClient
{
    /// <summary>
    /// Client Options
    /// </summary>
    internal OKXRestApiClientOptions ClientOptions { get; }

    /// <summary>
    /// Trading Account Client
    /// </summary>
    public OKXRestApiTradingAccountClient TradingAccount { get; }

    /// <summary>
    /// OrderBook Trading Client
    /// </summary>
    public OKXRestApiOrderBookTradingClient OrderBookTrading { get; }

    /// <summary>
    /// Block Trading Client
    /// </summary>
    public OKXRestApiBlockTradingClient BlockTrading { get; }

    /// <summary>
    /// Spread Trading Client
    /// </summary>
    public OKXRestApiSpreadTradingClient SpreadTrading { get; }

    /// <summary>
    /// PublicData Client
    /// </summary>
    public OKXRestApiPublicDataClient PublicData { get; }

    /// <summary>
    /// Trading Statistics Client
    /// </summary>
    public OKXRestApiTradingStatisticsClient TradingStatistics { get; }

    /// <summary>
    /// Funding Account Client
    /// </summary>
    public OKXRestApiFundingAccountClient FundingAccount { get; }

    /// <summary>
    /// SubAccount Client
    /// </summary>
    public OKXRestApiSubAccountClient SubAccount { get; }

    /// <summary>
    /// Financial Product Client
    /// </summary>
    public OKXRestApiFinancialProductClient FinancialProduct { get; }

    /// <summary>
    /// Status Client
    /// </summary>
    public OKXRestApiSystemClient Status { get; }

    /// <summary>
    /// Broker Client
    /// </summary>
    public OKXRestApiBrokerClient Broker { get; }

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

        TradingAccount = new OKXRestApiTradingAccountClient(this);
        OrderBookTrading = new OKXRestApiOrderBookTradingClient(this);
        BlockTrading = new OKXRestApiBlockTradingClient(this);
        SpreadTrading = new OKXRestApiSpreadTradingClient(this);
        PublicData = new OKXRestApiPublicDataClient(this);
        TradingStatistics = new OKXRestApiTradingStatisticsClient(this);
        SubAccount = new OKXRestApiSubAccountClient(this);
        FundingAccount = new OKXRestApiFundingAccountClient(this);
        Status = new OKXRestApiSystemClient(this);
        Broker = new OKXRestApiBrokerClient(this);
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
        FundingAccount.SetApiCredentials(credentials);
        OrderBookTrading.SetApiCredentials(credentials);
        BlockTrading.SetApiCredentials(credentials);
        SpreadTrading.SetApiCredentials(credentials);
        PublicData.SetApiCredentials(credentials);
        TradingStatistics.SetApiCredentials(credentials);
        SubAccount.SetApiCredentials(credentials);
        FundingAccount.SetApiCredentials(credentials);
        Status.SetApiCredentials(credentials);
        Broker.SetApiCredentials(credentials);
        TradingAccount.SetApiCredentials(credentials);
    }
}
