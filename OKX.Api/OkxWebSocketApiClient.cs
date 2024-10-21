namespace OKX.Api;

/// <summary>
/// OKX WebSocket Client
/// </summary>
public class OkxWebSocketApiClient : OkxBaseSocketClient
{
    /// <summary>
    /// Trading Account Client
    /// </summary>
    public OkxAccountSocketClient Account { get; }

    /// <summary>
    /// Trading Client
    /// </summary>
    public OkxTradeSocketClient Trade { get; }

    /// <summary>
    /// Algo Trading Client
    /// </summary>
    public OkxAlgoSocketClient Algo { get; }

    /// <summary>
    /// Grid Trading Client
    /// </summary>
    public OkxGridSocketClient Grid { get; }

    /// <summary>
    /// Recurring Buy Client
    /// </summary>
    public OkxRecurringBuySocketClient RecurringBuy { get; }
    
    /// <summary>
    /// Copy Trading Client
    /// </summary>
    public OkxCopyTradingSocketClient CopyTrading { get; }
    
    /// <summary>
    /// Block Trading Client
    /// </summary>
    public OkxBlockSocketClient Block { get; }

    /// <summary>
    /// Spread Trading Client
    /// </summary>
    public OkxSpreadSocketClient Spread { get; }

    /// <summary>
    /// Public and Market Data Client
    /// </summary>
    public OkxPublicSocketClient Public { get; }
    
    /// <summary>
    /// Funding Account Client
    /// </summary>
    public OkxFundingSocketClient Funding { get; }

    /// <summary>
    /// OKXWebSocketApiClient Constructor
    /// </summary>
    public OkxWebSocketApiClient() : this(null, new OkxWebSocketApiOptions())
    {
    }

    /// <summary>
    /// OKXWebSocketApiClient Constructor
    /// </summary>
    /// <param name="options"></param>
    public OkxWebSocketApiClient(OkxWebSocketApiOptions options) : this(null, options)
    {
    }

    /// <summary>
    /// OKXWebSocketApiClient Constructor
    /// </summary>
    /// <param name="logger">Logger</param>
    /// <param name="options">Options</param>
    public OkxWebSocketApiClient(ILogger? logger, OkxWebSocketApiOptions options) : base(logger, options)
    {
        this.Public = new OkxPublicSocketClient(this);
        this.Account = new OkxAccountSocketClient(this);
        this.Funding = new OkxFundingSocketClient(this);
        this.Trade = new OkxTradeSocketClient(this);
        this.Algo = new OkxAlgoSocketClient(this);
        this.Grid = new OkxGridSocketClient(this);
        this.RecurringBuy = new OkxRecurringBuySocketClient(this);
        this.CopyTrading = new OkxCopyTradingSocketClient(this);
        this.Block = new OkxBlockSocketClient(this);
        this.Spread = new OkxSpreadSocketClient(this);
    }

    internal int RequestId() => base.NextId();

    internal Task<CallResult<WebSocketUpdateSubscription>> RootSubscribeAsync<T>(OkxSocketEndpoint endpoint, object request, string? identifier, bool authenticated, Action<WebSocketDataEvent<T>> dataHandler, CancellationToken ct)
    {
        var url = ClientOptions.BaseAddress;
        var env = ((OkxWebSocketApiOptions)ClientOptions).DemoTradingService ? OkxAddress.Demo : OkxAddress.Default;
        if (endpoint == OkxSocketEndpoint.Public) url = env.WebSocketPublicAddress;
        else if (endpoint == OkxSocketEndpoint.Private) url = env.WebSocketPrivateAddress;
        else if (endpoint == OkxSocketEndpoint.Business) url = env.WebSocketBusinessAddress;

        return SubscribeAsync(url, request, identifier, authenticated, dataHandler, ct);
    }

    internal Task<CallResult<T>> RootQueryAsync<T>(OkxSocketEndpoint endpoint, object request, bool authenticated)
    {
        var url = ClientOptions.BaseAddress;
        var env = ((OkxWebSocketApiOptions)ClientOptions).DemoTradingService ? OkxAddress.Demo : OkxAddress.Default;
        if (endpoint == OkxSocketEndpoint.Public) url = env.WebSocketPublicAddress;
        else if (endpoint == OkxSocketEndpoint.Private) url = env.WebSocketPrivateAddress;
        else if (endpoint == OkxSocketEndpoint.Business) url = env.WebSocketBusinessAddress;

        return QueryAsync<T>(url, request, authenticated);
    }

}
