namespace OKX.Api;

/// <summary>
/// OKX WebSocket Client
/// </summary>
public class OKXWebSocketApiClient : OKXWebSocketApiBaseClient
{
    /// <summary>
    /// Trading Account Client
    /// </summary>
    public OKXWebSocketApiTradingAccountClient TradingAccount { get; }

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
    public OKXWebSocketApiPublicDataClient PublicData { get; }

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
    public OKXWebSocketApiClient() : this(new OKXWebSocketApiClientOptions())
    {
    }

    /// <summary>
    /// OKXWebSocketApiClient Constructor
    /// </summary>
    /// <param name="options">OKXStreamClientOptions</param>
    public OKXWebSocketApiClient(OKXWebSocketApiClientOptions options) : base(options)
    {
        this.TradingAccount = new OKXWebSocketApiTradingAccountClient(this);
        this.OrderBookTrading = new OKXWebSocketApiOrderBookTradingClient(this);
        this.BlockTrading = new OKXWebSocketApiBlockTradingClient(this);
        this.SpreadTrading = new OKXWebSocketApiSpreadTradingClient(this);
        this.PublicData = new OKXWebSocketApiPublicDataClient(this);
        this.TradingStatistics = new OKXWebSocketApiTradingStatisticsClient(this);
        this.FundingAccount = new OKXWebSocketApiFundingAccountClient(this);
        this.SubAccount = new OKXWebSocketApiSubAccountClient(this);
        this.FinancialProduct = new OKXWebSocketApiFinancialProductClient(this);
        this.Status = new OKXWebSocketApiSystemClient(this);
    }

    internal Task<CallResult<WebSocketUpdateSubscription>> RootSubscribeAsync<T>(OkxSocketEndpoint endpoint, object request, string identifier, bool authenticated, Action<WebSocketDataEvent<T>> dataHandler, CancellationToken ct)
    {
        var url = ClientOptions.BaseAddress;
        if (endpoint == OkxSocketEndpoint.Public)
            url = OKXApiAddresses.Default.WebSocketPublicAddress;
        else if (endpoint == OkxSocketEndpoint.Private)
            url = OKXApiAddresses.Default.WebSocketPrivateAddress;
        else if (endpoint == OkxSocketEndpoint.Business)
            url = OKXApiAddresses.Default.WebSocketBusinessAddress;

        return SubscribeAsync<T>(url, request, identifier, authenticated, dataHandler, ct);
    }

    internal Task<CallResult<T>> RootQueryAsync<T>(OkxSocketEndpoint endpoint, object request, bool authenticated)
    {
        var url = ClientOptions.BaseAddress;
        if (endpoint == OkxSocketEndpoint.Public)
            url = OKXApiAddresses.Default.WebSocketPublicAddress;
        else if (endpoint == OkxSocketEndpoint.Private)
            url = OKXApiAddresses.Default.WebSocketPrivateAddress;
        else if (endpoint == OkxSocketEndpoint.Business)
            url = OKXApiAddresses.Default.WebSocketBusinessAddress;

        return QueryAsync<T>(url, request, authenticated);
    }
}