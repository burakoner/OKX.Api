namespace OKX.Api;

/// <summary>
/// OKX Rest API Client
/// </summary>
public class OkxRestApiClient
{
    /// <summary>
    /// Logger
    /// </summary>
    public ILogger Logger { get; }

    /// <summary>
    /// Client Options
    /// </summary>
    public OkxRestApiOptions Options { get; }

    /// <summary>
    /// Trading Account Client
    /// </summary>
    public OkxAccountRestClient Account { get; }

    /// <summary>
    /// Trading Client
    /// </summary>
    public OkxTradingRestClient Trading { get; }

    /// <summary>
    /// Algo Trading Client
    /// </summary>
    public OkxAlgoTradingRestClient AlgoTrading { get; }

    /// <summary>
    /// Grid Trading Client
    /// </summary>
    public OkxGridTradingRestClient GridTrading { get; }

    /// <summary>
    /// Signal Trading Client
    /// </summary>
    public OkxSignalBotTradingRestClient SignalBotTrading { get; }

    /// <summary>
    /// Recurring Buy Client
    /// </summary>
    public OkxRecurringBuyRestClient RecurringBuy { get; }

    /// <summary>
    /// Copy Trading Client
    /// </summary>
    public OkxCopyTradingRestClient CopyTrading { get; } // TODO

    /// <summary>
    /// Alias for Public Client
    /// </summary>
    public OkxPublicRestClient Market { get => Public; }

    /// <summary>
    /// Block Trading Client
    /// </summary>
    public OkxBlockTradingRestClient BlockTrading { get; } // TODO
    
    /// <summary>
    /// Spread Trading Client
    /// </summary>
    public OkxSpreadTradingRestClient SpreadTrading { get; } // TODO
    
    /// <summary>
    /// Public and Market Data Client
    /// </summary>
    public OkxPublicRestClient Public { get; }
    
    /// <summary>
    /// Trading Statistics Client
    /// </summary>
    public OkxRubikRestClient Rubik { get; } // TODO 2
    
    /// <summary>
    /// Funding Account Client
    /// </summary>
    public OkxFundingRestClient Funding { get; } // TODO 3
    
    /// <summary>
    /// SubAccount Client
    /// </summary>
    public OkxSubAccountRestClient SubAccount { get; }  // TODO 4

    /// <summary>
    /// Financial Products Client
    /// </summary>
    public OkxFinancialProductsRestClient FinancialProducts { get; } // TODO

    /// <summary>
    /// Broker Client
    /// </summary>
    public OkxBrokerRestClient Broker { get; } // TODO
    
    /// <summary>
    /// Affiliate Client
    /// </summary>
    public OkxAffiliateRestClient Affiliate { get; } // TODO 0

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
        Logger = logger ?? BaseClient.LoggerFactory.CreateLogger("OKX API");

        Public = new OkxPublicRestClient(this);
        Account = new OkxAccountRestClient(this);
        Funding = new OkxFundingRestClient(this);
        SubAccount = new OkxSubAccountRestClient(this);
        Trading = new OkxTradingRestClient(this);
        AlgoTrading = new OkxAlgoTradingRestClient(this);
        GridTrading = new OkxGridTradingRestClient(this);
        SignalBotTrading = new OkxSignalBotTradingRestClient(this);
        RecurringBuy = new OkxRecurringBuyRestClient(this);
        CopyTrading = new OkxCopyTradingRestClient(this);
        BlockTrading = new OkxBlockTradingRestClient(this);
        SpreadTrading = new OkxSpreadTradingRestClient(this);
        FinancialProducts = new OkxFinancialProductsRestClient(this);
        Rubik = new OkxRubikRestClient(this);
        Broker = new OkxBrokerRestClient(this);
        Affiliate = new OkxAffiliateRestClient(this);
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
        Trading.SetApiCredentials(credentials);
        AlgoTrading.SetApiCredentials(credentials);
        GridTrading.SetApiCredentials(credentials);
        SignalBotTrading.SetApiCredentials(credentials);
        RecurringBuy.SetApiCredentials(credentials);
        GridTrading.SetApiCredentials(credentials);
        CopyTrading.SetApiCredentials(credentials);
        BlockTrading.SetApiCredentials(credentials);
        SpreadTrading.SetApiCredentials(credentials);
        FinancialProducts.SetApiCredentials(credentials);
        Rubik.SetApiCredentials(credentials);
        Broker.SetApiCredentials(credentials);
        Affiliate.SetApiCredentials(credentials);
    }
}
