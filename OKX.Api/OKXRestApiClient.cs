using OKX.Api.Account.Clients;
using OKX.Api.Affiliate.Clients;
using OKX.Api.AlgoTrading.Clients;
using OKX.Api.Authentication;
using OKX.Api.BlockTrading.Clients;
using OKX.Api.Broker.Clients;
using OKX.Api.CopyTrading.Clients;
using OKX.Api.Earn.Clients;
using OKX.Api.Funding.Clients;
using OKX.Api.GridTrading.Clients;
using OKX.Api.Public.Clients;
using OKX.Api.RecurringBuy.Clients;
using OKX.Api.Rubik.Clients;
using OKX.Api.Savings.Clients;
using OKX.Api.SignalTrading.Clients;
using OKX.Api.SpreadTrading.Clients;
using OKX.Api.Staking.Clients;
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
    public OkxSignalTradingRestClient SignalTrading { get; }

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
    public OkxBlockTradingRestClient BlockTrading { get; }

    /// <summary>
    /// Spread Trading Client
    /// </summary>
    public OkxSpreadTradingRestClient SpreadTrading { get; }

    /// <summary>
    /// Earn Client
    /// </summary>
    public OkxEarnRestClient Earn { get; }
    
    /// <summary>
    /// Savings Client
    /// </summary>
    public OkxSavingsRestClient Savings { get; }

    /// <summary>
    /// Staking Client
    /// </summary>
    public OkxStakingRestClient Staking { get; }

    /// <summary>
    /// Trading Statistics Client
    /// </summary>
    public OkxRubikRestClient Rubik { get; }

    /// <summary>
    /// Affiliate Client
    /// </summary>
    public OkxAffiliateRestClient Affiliate { get; }

    /// <summary>
    /// NonDisclosed Broker Client
    /// </summary>
    public OkxNonDisclosedBrokerRestClient NDBroker { get; }

    /// <summary>
    /// FullyDisclosed Broker Client
    /// Savings Client
    /// </summary>
    public OkxFullyDisclosedBrokerRestClient FDBroker { get; }

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
        Trading = new OkxTradingRestClient(this);
        AlgoTrading = new OkxAlgoTradingRestClient(this);
        GridTrading = new OkxGridTradingRestClient(this);
        SignalTrading = new OkxSignalTradingRestClient(this);
        RecurringBuy = new OkxRecurringBuyRestClient(this);
        CopyTrading = new OkxCopyTradingRestClient(this);
        BlockTrading = new OkxBlockTradingRestClient(this);
        SpreadTrading = new OkxSpreadTradingRestClient(this);
        Earn = new OkxEarnRestClient(this);
        Savings = new OkxSavingsRestClient(this);
        Staking = new OkxStakingRestClient(this);
        Rubik = new OkxRubikRestClient(this);
        Affiliate = new OkxAffiliateRestClient(this);
        NDBroker = new OkxNonDisclosedBrokerRestClient(this);
        FDBroker = new OkxFullyDisclosedBrokerRestClient(this);
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
        SignalTrading.SetApiCredentials(credentials);
        RecurringBuy.SetApiCredentials(credentials);
        GridTrading.SetApiCredentials(credentials);
        CopyTrading.SetApiCredentials(credentials);
        BlockTrading.SetApiCredentials(credentials);
        SpreadTrading.SetApiCredentials(credentials);
        Earn.SetApiCredentials(credentials);
        Savings.SetApiCredentials(credentials);
        Staking.SetApiCredentials(credentials);
        Rubik.SetApiCredentials(credentials);
        Affiliate.SetApiCredentials(credentials);
        NDBroker.SetApiCredentials(credentials);
        FDBroker.SetApiCredentials(credentials);
    }
}
