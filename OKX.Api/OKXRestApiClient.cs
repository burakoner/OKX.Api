namespace OKX.Api;

/// <summary>
/// OKX Rest API Client
/// </summary>
public class OkxRestApiClient
{
    #region Encapsulations
    /// <summary>
    /// Logger
    /// </summary>
    public ILogger Logger => this._logger;
    private readonly ILogger _logger;

    /// <summary>
    /// Client Options
    /// </summary>
    public OkxRestApiOptions Options => this._options;
    private readonly OkxRestApiOptions _options;
    #endregion

    #region Clients
    /// <summary>
    /// Trading Account Client
    /// </summary>
    public OkxAccountRestClient Account { get; }

    /// <summary>
    /// Trading Client
    /// </summary>
    public OkxTradeRestClient Trade { get; }

    /// <summary>
    /// Algo Trading Client
    /// </summary>
    public OkxAlgoRestClient Algo { get; }

    /// <summary>
    /// Grid Trading Client
    /// </summary>
    public OkxGridRestClient Grid { get; }

    /// <summary>
    /// Signal Trading Client
    /// </summary>
    public OkxSignalBotRestClient SignalBot { get; }

    /// <summary>
    /// Recurring Buy Client
    /// </summary>
    public OkxRecurringBuyRestClient RecurringBuy { get; }

    /// <summary>
    /// Copy Trading Client
    /// </summary>
    public OkxCopyTradingRestClient CopyTrading { get; }

    /// <summary>
    /// Block Trading Client
    /// </summary>
    public OkxBlockRestClient Block { get; }

    /// <summary>
    /// Spread Trading Client
    /// </summary>
    public OkxSpreadRestClient Spread { get; }

    /// <summary>
    /// Public and Market Data Client
    /// </summary>
    public OkxPublicRestClient Public { get; }

    /// <summary>
    /// Trading Statistics Client
    /// </summary>
    public OkxRubikRestClient Rubik { get; }

    /// <summary>
    /// Funding Account Client
    /// </summary>
    public OkxFundingRestClient Funding { get; }

    /// <summary>
    /// SubAccount Client
    /// </summary>
    public OkxSubAccountRestClient SubAccount { get; }

    /// <summary>
    /// Financial Products Client
    /// </summary>
    public OkxFinancialRestClient Financial { get; }

    /// <summary>
    /// Broker Client
    /// </summary>
    public OkxBrokerRestClient Broker { get; } // TODO

    /// <summary>
    /// Affiliate Client
    /// </summary>
    public OkxAffiliateRestClient Affiliate { get; }
    #endregion

    #region Constructors
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
    public OkxRestApiClient(ILogger? logger, OkxRestApiOptions options)
    {
        // Private Fields
        _logger = logger ?? BaseClient.LoggerFactory.CreateLogger("OKX.Api");
        _options = options;

        // Initialize Clients
        Public = new OkxPublicRestClient(this);
        Account = new OkxAccountRestClient(this);
        Funding = new OkxFundingRestClient(this);
        SubAccount = new OkxSubAccountRestClient(this);
        Trade = new OkxTradeRestClient(this);
        Algo = new OkxAlgoRestClient(this);
        Grid = new OkxGridRestClient(this);
        SignalBot = new OkxSignalBotRestClient(this);
        RecurringBuy = new OkxRecurringBuyRestClient(this);
        CopyTrading = new OkxCopyTradingRestClient(this);
        Block = new OkxBlockRestClient(this);
        Spread = new OkxSpreadRestClient(this);
        Financial = new OkxFinancialRestClient(this);
        Rubik = new OkxRubikRestClient(this);
        Broker = new OkxBrokerRestClient(this);
        Affiliate = new OkxAffiliateRestClient(this);
    }
    #endregion

    #region Public Methods
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
        Algo.SetApiCredentials(credentials);
        Grid.SetApiCredentials(credentials);
        SignalBot.SetApiCredentials(credentials);
        RecurringBuy.SetApiCredentials(credentials);
        Grid.SetApiCredentials(credentials);
        CopyTrading.SetApiCredentials(credentials);
        Block.SetApiCredentials(credentials);
        Spread.SetApiCredentials(credentials);
        Financial.SetApiCredentials(credentials);
        Rubik.SetApiCredentials(credentials);
        Broker.SetApiCredentials(credentials);
        Affiliate.SetApiCredentials(credentials);
    }
    #endregion
}
