using OKX.Api.Common.Models;

namespace OKX.Api;

/// <summary>
/// OKX WebSocket Client
/// </summary>
public class OKXWebSocketApiClient : OkxBaseSocketClient
{
    /// <summary>
    /// Public and Market Data Client
    /// </summary>
    public OkxPublicSocketClient Public { get; }

    /// <summary>
    /// Trading Account Client
    /// </summary>
    public OkxAccountSocketClient Account { get; }

    /// <summary>
    /// Funding Account Client
    /// </summary>
    public OkxFundingSocketClient Funding { get; }

    /// <summary>
    /// Trading Client
    /// </summary>
    public OkxTradingSocketClient Trading { get; }

    /// <summary>
    /// Algo Trading Client
    /// </summary>
    public OkxAlgoTradingSocketClient AlgoTrading { get; }

    /// <summary>
    /// Grid Trading Client
    /// </summary>
    public OkxGridTradingSocketClient GridTrading { get; }

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
    public OkxBlockTradingSocketClient BlockTrading { get; }

    /// <summary>
    /// Spread Trading Client
    /// </summary>
    public OkxSpreadTradingSocketClient SpreadTrading { get; }

    /// <summary>
    /// OKXWebSocketApiClient Constructor
    /// </summary>
    public OKXWebSocketApiClient() : this(null, new OkxWebSocketApiOptions())
    {
    }

    /// <summary>
    /// OKXWebSocketApiClient Constructor
    /// </summary>
    /// <param name="options"></param>
    public OKXWebSocketApiClient(OkxWebSocketApiOptions options) : this(null, options)
    {
    }

    /// <summary>
    /// OKXWebSocketApiClient Constructor
    /// </summary>
    /// <param name="logger">Logger</param>
    /// <param name="options">Options</param>
    public OKXWebSocketApiClient(ILogger logger, OkxWebSocketApiOptions options) : base(logger, options)
    {
        this.Public = new OkxPublicSocketClient(this);
        this.Account = new OkxAccountSocketClient(this);
        this.Funding = new OkxFundingSocketClient(this);
        this.Trading = new OkxTradingSocketClient(this);
        this.AlgoTrading = new OkxAlgoTradingSocketClient(this);
        this.GridTrading = new OkxGridTradingSocketClient(this);
        this.RecurringBuy = new OkxRecurringBuySocketClient(this);
        this.CopyTrading = new OkxCopyTradingSocketClient(this);
        this.BlockTrading = new OkxBlockTradingSocketClient(this);
        this.SpreadTrading = new OkxSpreadTradingSocketClient(this);
    }

    internal int RequestId() => base.NextId();

    internal Task<CallResult<WebSocketUpdateSubscription>> RootSubscribeAsync<T>(OkxSocketEndpoint endpoint, object request, string identifier, bool authenticated, Action<WebSocketDataEvent<T>> dataHandler, CancellationToken ct)
    {
        var url = ClientOptions.BaseAddress;
        var env = ((OkxWebSocketApiOptions)ClientOptions).DemoTradingService ? OkxAddress.Demo : OkxAddress.Default;
        if (endpoint == OkxSocketEndpoint.Public) url = env.WebSocketPublicAddress;
        else if (endpoint == OkxSocketEndpoint.Private) url = env.WebSocketPrivateAddress;
        else if (endpoint == OkxSocketEndpoint.Business) url = env.WebSocketBusinessAddress;

        return SubscribeAsync<T>(url, request, identifier, authenticated, dataHandler, ct);
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
