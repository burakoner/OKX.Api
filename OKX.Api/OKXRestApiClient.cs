using OKX.Api.Account.Clients;
using OKX.Api.Affiliate.Clients;
using OKX.Api.Algo.Clients;
using OKX.Api.Block.Clients;
using OKX.Api.Broker.Clients;
using OKX.Api.CopyTrading.Clients;
using OKX.Api.Financial.Clients;
using OKX.Api.Funding.Clients;
using OKX.Api.Grid.Clients;
using OKX.Api.Public.Clients;
using OKX.Api.RecurringBuy.Clients;
using OKX.Api.Rubik.Clients;
using OKX.Api.SignalBot.Clients;
using OKX.Api.Spread.Clients;
using OKX.Api.SubAccount.Clients;
using OKX.Api.Trade.Clients;

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
    /// Alias for Public Client
    /// </summary>
    public OkxPublicRestClient Market { get => Public; }

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
    public OkxBrokerRestClient Broker { get; } // TODO: 3 + 27
    
    /// <summary>
    /// Affiliate Client
    /// </summary>
    public OkxAffiliateRestClient Affiliate { get; }

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
}
