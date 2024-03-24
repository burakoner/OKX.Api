using OKX.Api.Account.Clients;
using OKX.Api.AlgoTrading.Clients;
using OKX.Api.Funding.Clients;
using OKX.Api.GridTrading.Clients;
using OKX.Api.Public.Clients;
using OKX.Api.Trade.Clients;

namespace OKX.Api;

/// <summary>
/// OKX WebSocket Client
/// </summary>
public class OkxSocketApiClient : OKXWebSocketApiBaseClient
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
    public OkxTradeSocketClient Trade { get; }

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
    public OKXWebSocketApiRecurringBuyClient RecurringBuy { get; }

    /// <summary>
    /// Copy Trading Client
    /// </summary>
    public OKXWebSocketApiCopyTradingClient CopyTrading { get; }

    /// <summary>
    /// Block Trading Client
    /// </summary>
    public OKXWebSocketApiBlockTradingClient BlockTrading { get; }

    /// <summary>
    /// Spread Trading Client
    /// </summary>
    public OKXWebSocketApiSpreadTradingClient SpreadTrading { get; }

    /// <summary>
    /// Trading Statistics Client
    /// </summary>
    public OKXWebSocketApiTradingStatisticsClient TradingStatistics { get; }

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
    public OkxSocketApiClient() : this(null, new OkxSocketApiOptions())
    {
    }

    /// <summary>
    /// OKXWebSocketApiClient Constructor
    /// </summary>
    /// <param name="options"></param>
    public OkxSocketApiClient(OkxSocketApiOptions options) : this(null, options)
    {
    }

    /// <summary>
    /// OKXWebSocketApiClient Constructor
    /// </summary>
    /// <param name="logger">Logger</param>
    /// <param name="options">Options</param>
    public OkxSocketApiClient(ILogger logger, OkxSocketApiOptions options) : base(logger, options)
    {
        this.Public = new OkxPublicSocketClient(this);
        this.Account = new OkxAccountSocketClient(this);
        this.Funding = new OkxFundingSocketClient(this);
        this.Trade = new OkxTradeSocketClient(this);
        this.AlgoTrading = new OkxAlgoTradingSocketClient(this);
        this.GridTrading = new OkxGridTradingSocketClient(this);



        this.RecurringBuy = new OKXWebSocketApiRecurringBuyClient(this);
        this.CopyTrading = new OKXWebSocketApiCopyTradingClient(this);
        this.BlockTrading = new OKXWebSocketApiBlockTradingClient(this);
        this.SpreadTrading = new OKXWebSocketApiSpreadTradingClient(this);
        this.TradingStatistics = new OKXWebSocketApiTradingStatisticsClient(this);
        this.FinancialProduct = new OKXWebSocketApiFinancialProductClient(this);
        this.Status = new OKXWebSocketApiSystemClient(this);
    }

    internal int RequestId() => base.NextId();

    internal Task<CallResult<WebSocketUpdateSubscription>> RootSubscribeAsync<T>(OkxSocketEndpoint endpoint, object request, string identifier, bool authenticated, Action<WebSocketDataEvent<T>> dataHandler, CancellationToken ct)
    {
        var url = ClientOptions.BaseAddress;
        var env = ((OkxSocketApiOptions)ClientOptions).DemoTradingService ? OkxAddress.Demo : OkxAddress.Default;
        if (endpoint == OkxSocketEndpoint.Public) url = env.WebSocketPublicAddress;
        else if (endpoint == OkxSocketEndpoint.Private) url = env.WebSocketPrivateAddress;
        else if (endpoint == OkxSocketEndpoint.Business) url = env.WebSocketBusinessAddress;

        return SubscribeAsync<T>(url, request, identifier, authenticated, dataHandler, ct);
    }

    internal Task<CallResult<T>> RootQueryAsync<T>(OkxSocketEndpoint endpoint, object request, bool authenticated)
    {
        var url = ClientOptions.BaseAddress;
        var env = ((OkxSocketApiOptions)ClientOptions).DemoTradingService ? OkxAddress.Demo : OkxAddress.Default;
        if (endpoint == OkxSocketEndpoint.Public) url = env.WebSocketPublicAddress;
        else if (endpoint == OkxSocketEndpoint.Private) url = env.WebSocketPrivateAddress;
        else if (endpoint == OkxSocketEndpoint.Business) url = env.WebSocketBusinessAddress;

        return QueryAsync<T>(url, request, authenticated);
    }

}
