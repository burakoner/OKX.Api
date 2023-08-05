namespace OKX.Api.Clients.WebSocketApi;

/// <summary>
/// OKX OrderBook Trading Base WebSocket Api Client
/// </summary>
public class OKXWebSocketOrderBookTradingClient
{
    /// <summary>
    /// Trading Client
    /// </summary>
    public OKXWebSocketTradeClient Trade { get; }

    /// <summary>
    /// Algo Trading Client
    /// </summary>
    public OKXWebSocketAlgoTradingClient AlgoTrading { get; }

    /// <summary>
    /// Grid Trading Client
    /// </summary>
    public OKXWebSocketGridTradingClient GridTrading { get; }

    /// <summary>
    /// Recurring Buy Client
    /// </summary>
    public OKXWebSocketRecurringBuyClient RecurringBuy { get; }

    /// <summary>
    /// Copy Trading Client
    /// </summary>
    public OKXWebSocketCopyTradingClient CopyTrading { get; }

    /// <summary>
    /// Market Data Client
    /// </summary>
    public OKXWebSocketMarketDataClient MarketData { get; }

    // Root Client
    internal OKXWebSocketApiClient RootClient { get; }

    internal OKXWebSocketOrderBookTradingClient(OKXWebSocketApiClient root)
    {
        this.RootClient = root;

        Trade = new  OKXWebSocketTradeClient(root);
        AlgoTrading = new  OKXWebSocketAlgoTradingClient(root);
        GridTrading = new  OKXWebSocketGridTradingClient(root);
        RecurringBuy = new  OKXWebSocketRecurringBuyClient(root);
        CopyTrading = new  OKXWebSocketCopyTradingClient(root);
        MarketData = new  OKXWebSocketMarketDataClient(root);
    }

}