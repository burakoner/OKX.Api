﻿using OKX.Api.Account.Clients;

namespace OKX.Api;

/// <summary>
/// OKX Rest API Client
/// </summary>
public sealed class OKXRestApiClient
{
    /// <summary>
    /// Logger
    /// </summary>
    internal ILogger Logger { get; }

    /// <summary>
    /// Client Options
    /// </summary>
    internal OKXRestApiOptions Options { get; }

    /// <summary>
    /// Trading Account Client
    /// </summary>
    public OkxAccountRestClient TradingAccount { get; }

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
    public OKXRestApiPublicClient PublicData { get; }

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
    public OKXRestApiAffiliateClient Affiliate { get; }

    /// <summary>
    /// Status Client
    /// </summary>
    public OKXRestApiStatusClient Status { get; }

    /// <summary>
    /// Broker Client
    /// </summary>
    public OKXRestApiBrokerClient Broker { get; }

    /// <summary>
    /// OKXRestApiClient Constructor
    /// </summary>
    public OKXRestApiClient() : this(null, new OKXRestApiOptions())
    {
    }

    /// <summary>
    /// OKXRestApiClient Constructor
    /// </summary>
    public OKXRestApiClient(OKXRestApiOptions options) : this(null, options)
    {
    }

    /// <summary>
    /// OKXRestApiClient Constructor
    /// </summary>
    /// <param name="logger">Logger</param>
    /// <param name="options">Options</param>
    public OKXRestApiClient(ILogger logger, OKXRestApiOptions options)
    {
        Options = options;
        Logger = logger ?? BaseClient.LoggerFactory.CreateLogger("OKX Api");

        PublicData = new OKXRestApiPublicClient(this);
        TradingAccount = new OkxAccountRestClient(this);
        OrderBookTrading = new OKXRestApiOrderBookTradingClient(this);
        BlockTrading = new OKXRestApiBlockTradingClient(this);
        SpreadTrading = new OKXRestApiSpreadTradingClient(this);
        TradingStatistics = new OKXRestApiTradingStatisticsClient(this);
        SubAccount = new OKXRestApiSubAccountClient(this);
        FundingAccount = new OKXRestApiFundingAccountClient(this);
        Affiliate = new OKXRestApiAffiliateClient(this);
        Status = new OKXRestApiStatusClient(this);
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
        PublicData.SetApiCredentials(credentials);
        TradingAccount.SetApiCredentials(credentials);
        FundingAccount.SetApiCredentials(credentials);
        OrderBookTrading.SetApiCredentials(credentials);
        BlockTrading.SetApiCredentials(credentials);
        SpreadTrading.SetApiCredentials(credentials);
        TradingStatistics.SetApiCredentials(credentials);
        SubAccount.SetApiCredentials(credentials);
        FundingAccount.SetApiCredentials(credentials);
        Affiliate.SetApiCredentials(credentials);
        Status.SetApiCredentials(credentials);
        Broker.SetApiCredentials(credentials);
    }
}
