using OKX.Api.Account.Clients;

namespace OKX.Api;

/// <summary>
/// OKX WebSocket Client
/// </summary>
public class OKXWebSocketApiClient : OKXWebSocketApiBaseClient
{
    /// <summary>
    /// Trading Account Client
    /// </summary>
    public OkxAccountSocketClient Account { get; }

    /// <summary>
    /// OrderBook Trading Client
    /// </summary>
    public OKXWebSocketApiOrderBookTradingClient OrderBookTrading { get; }

    /// <summary>
    /// Block Trading Client
    /// </summary>
    public OKXWebSocketApiBlockTradingClient BlockTrading { get; }

    /// <summary>
    /// Spread Trading Client
    /// </summary>
    public OKXWebSocketApiSpreadTradingClient SpreadTrading { get; }

    /// <summary>
    /// PublicData Client
    /// </summary>
    public OKXWebSocketApiPublicClient Public { get; }

    /// <summary>
    /// Trading Statistics Client
    /// </summary>
    public OKXWebSocketApiTradingStatisticsClient TradingStatistics { get; }

    /// <summary>
    /// Funding Account Client
    /// </summary>
    public OKXWebSocketApiFundingAccountClient FundingAccount { get; }

    /// <summary>
    /// SubAccount Client
    /// </summary>
    public OKXWebSocketApiSubAccountClient SubAccount { get; }

    /// <summary>
    /// Financial Product Client
    /// </summary>
    public OKXWebSocketApiFinancialProductClient FinancialProduct { get; }

    /// <summary>
    /// Status Client
    /// </summary>
    public OKXWebSocketApiSystemClient Status { get; }

    /// <summary>
    /// OKXWebSocketApiClient Constructor
    /// </summary>
    public OKXWebSocketApiClient() : this(null, new OKXWebSocketApiOptions())
    {
    }

    /// <summary>
    /// OKXWebSocketApiClient Constructor
    /// </summary>
    /// <param name="options"></param>
    public OKXWebSocketApiClient(OKXWebSocketApiOptions options) : this(null, options)
    {
    }

    /// <summary>
    /// OKXWebSocketApiClient Constructor
    /// </summary>
    /// <param name="logger">Logger</param>
    /// <param name="options">Options</param>
    public OKXWebSocketApiClient(ILogger logger, OKXWebSocketApiOptions options) : base(logger, options)
    {
        this.Public = new OKXWebSocketApiPublicClient(this);
        this.Account = new OkxAccountSocketClient(this);
        this.OrderBookTrading = new OKXWebSocketApiOrderBookTradingClient(this);
        this.BlockTrading = new OKXWebSocketApiBlockTradingClient(this);
        this.SpreadTrading = new OKXWebSocketApiSpreadTradingClient(this);
        this.TradingStatistics = new OKXWebSocketApiTradingStatisticsClient(this);
        this.FundingAccount = new OKXWebSocketApiFundingAccountClient(this);
        this.SubAccount = new OKXWebSocketApiSubAccountClient(this);
        this.FinancialProduct = new OKXWebSocketApiFinancialProductClient(this);
        this.Status = new OKXWebSocketApiSystemClient(this);
    }

    internal Task<CallResult<WebSocketUpdateSubscription>> RootSubscribeAsync<T>(OkxSocketEndpoint endpoint, object request, string identifier, bool authenticated, Action<WebSocketDataEvent<T>> dataHandler, CancellationToken ct)
    {
        var url = ClientOptions.BaseAddress;
        var env = ((OKXWebSocketApiOptions)ClientOptions).DemoTradingService ? OKXAddress.Demo : OKXAddress.Default;
        if (endpoint == OkxSocketEndpoint.Public)
            url = env.WebSocketPublicAddress;
        else if (endpoint == OkxSocketEndpoint.Private)
            url = env.WebSocketPrivateAddress;
        else if (endpoint == OkxSocketEndpoint.Business)
            url = env.WebSocketBusinessAddress;

        return SubscribeAsync<T>(url, request, identifier, authenticated, dataHandler, ct);
    }

    internal Task<CallResult<T>> RootQueryAsync<T>(OkxSocketEndpoint endpoint, object request, bool authenticated)
    {
        var url = ClientOptions.BaseAddress;
        var env = ((OKXWebSocketApiOptions)ClientOptions).DemoTradingService ? OKXAddress.Demo : OKXAddress.Default;
        if (endpoint == OkxSocketEndpoint.Public)
            url = env.WebSocketPublicAddress;
        else if (endpoint == OkxSocketEndpoint.Private)
            url = env.WebSocketPrivateAddress;
        else if (endpoint == OkxSocketEndpoint.Business)
            url = env.WebSocketBusinessAddress;

        return QueryAsync<T>(url, request, authenticated);
    }

    internal int RequestId()
    {
        return base.NextId();
    }
}
