using OKX.Api.Account.Clients;
using OKX.Api.AlgoTrading.Clients;
using OKX.Api.Funding.Clients;
using OKX.Api.SubAccount.Clients;
using OKX.Api.Trade.Clients;

namespace OKX.Api;

/// <summary>
/// OKX Rest API Client
/// </summary>
public sealed class OkxRestApiClient
{
    /// <summary>
    /// Logger
    /// </summary>
    internal ILogger Logger { get; }

    /// <summary>
    /// Client Options
    /// </summary>
    internal OkxRestApiOptions Options { get; }
    
    /// <summary>
    /// Public and Market Data Client
    /// </summary>
    public OkxPublicRestClient Public { get; }

    /// <summary>
    /// Trading Account Client
    /// </summary>
    public OkxAccountRestClient Account { get; }
    
    /// <summary>
    /// Funding Account Client
    /// </summary>
    public OkxFundingRestClient Funding { get; }
    
    /// <summary>
    /// SubAccount Client
    /// </summary>
    public OkxSubAccountRestClient SubAccount { get; }

    /// <summary>
    /// Trading Client
    /// </summary>
    public OkxTradeRestClient Trade { get; }

    /// <summary>
    /// Algo Trading Client
    /// </summary>
    public OkxAlgoTradingRestClient AlgoTrading { get; }
















    /// <summary>
    /// Grid Trading Client
    /// </summary>
    public OKXRestApiGridTradingClient GridTrading { get; }

    /// <summary>
    /// Recurring Buy Client
    /// </summary>
    public OKXRestApiRecurringBuyClient RecurringBuy { get; }

    /// <summary>
    /// Copy Trading Client
    /// </summary>
    public OKXRestApiCopyTradingClient CopyTrading { get; }

    /// <summary>
    /// Block Trading Client
    /// </summary>
    public OKXRestApiBlockTradingClient BlockTrading { get; }

    /// <summary>
    /// Spread Trading Client
    /// </summary>
    public OKXRestApiSpreadTradingClient SpreadTrading { get; }

    /// <summary>
    /// Trading Statistics Client
    /// </summary>
    public OKXRestApiTradingStatisticsClient TradingStatistics { get; }

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
    public OkxRestApiClient() : this(null, new OkxRestApiOptions())
    {
    }

    /// <summary>
    /// OKXRestApiClient Constructor
    /// </summary>
    public OkxRestApiClient(OkxRestApiOptions options) : this(null, options)
    {
    }

    /// <summary>
    /// OKXRestApiClient Constructor
    /// </summary>
    /// <param name="logger">Logger</param>
    /// <param name="options">Options</param>
    public OkxRestApiClient(ILogger logger, OkxRestApiOptions options)
    {
        Options = options;
        Logger = logger ?? BaseClient.LoggerFactory.CreateLogger("OKX Api");

        Public = new OkxPublicRestClient(this);
        Account = new OkxAccountRestClient(this);
        Funding = new OkxFundingRestClient(this);
        SubAccount = new OkxSubAccountRestClient(this);
        Trade = new OkxTradeRestClient(this);
        AlgoTrading = new OkxAlgoTradingRestClient(this);





        
        GridTrading = new OKXRestApiGridTradingClient(this);
        RecurringBuy = new OKXRestApiRecurringBuyClient(this);
        CopyTrading = new OKXRestApiCopyTradingClient(this);
        BlockTrading = new OKXRestApiBlockTradingClient(this);
        SpreadTrading = new OKXRestApiSpreadTradingClient(this);
        TradingStatistics = new OKXRestApiTradingStatisticsClient(this);
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
        Public.SetApiCredentials(credentials);
        Account.SetApiCredentials(credentials);
        Funding.SetApiCredentials(credentials);
        SubAccount.SetApiCredentials(credentials);
        Trade.SetApiCredentials(credentials);
        AlgoTrading.SetApiCredentials(credentials);



        
        GridTrading.SetApiCredentials(credentials);
        RecurringBuy.SetApiCredentials(credentials);
        GridTrading.SetApiCredentials(credentials);
        CopyTrading.SetApiCredentials(credentials);
        BlockTrading.SetApiCredentials(credentials);
        SpreadTrading.SetApiCredentials(credentials);
        TradingStatistics.SetApiCredentials(credentials);
        Funding.SetApiCredentials(credentials);
        Affiliate.SetApiCredentials(credentials);
        Status.SetApiCredentials(credentials);
        Broker.SetApiCredentials(credentials);
    }
}
